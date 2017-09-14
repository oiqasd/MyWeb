using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI.WebControls;
using System.Net;
using System.IO;

namespace YZ.Common.Util
{
    public class Utils
    {
        /// <summary>
        /// 随机密码 前三数字+后三字母
        /// </summary>
        /// <returns></returns>
        public static string AutoPassword()
        {
            string[] s1 = { "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", 
                              "s", "t", "u", "v", "w", "x", "y", "z", "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", 
                              "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z" };
            var p = new Random().Next(999).ToString("000");
            p += s1[new Random().Next(0, s1.Length)];
            p += s1[new Random().Next(0, s1.Length)];
            p += s1[new Random().Next(0, s1.Length)];
            return p;
        }

        /// <summary>
        /// 将对象属性转换为key-value对
        /// </summary>
        /// <returns></returns>
        public static SortedDictionary<String, Object> EntityToMap(Object o)
        {
            SortedDictionary<String, Object> map = new SortedDictionary<string, object>();
            Type t = o.GetType();

            PropertyInfo[] pi = t.GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (PropertyInfo p in pi)
            {
                MethodInfo mi = p.GetGetMethod();
                if (mi != null && mi.IsPublic)
                {
                    map.Add(p.Name, mi.Invoke(o, new Object[] { }));
                }
            }
            return map;
        }

        /// <summary>
        /// 生成6位随机数
        /// </summary>
        /// <returns></returns>
        public static string GetSixRandom()
        {
            Random rand = new Random(Guid.NewGuid().GetHashCode());
            string iRand = rand.Next(100000, 999999).ToString();
            return iRand;
        }

        /// <summary>
        /// 生成4位随机数
        /// </summary>
        /// <returns></returns>
        public static string GetFourRandom()
        {
            Random rand = new Random(Guid.NewGuid().GetHashCode());
            string iRand = rand.Next(1000, 9999).ToString();
            return iRand;
        }

        /// <summary>
        /// 生成随机数
        /// </summary>
        /// <param name="CodeFormat"></param>
        /// <returns></returns>
        public static string GetRandom(int CodeFormat)
        {
            string strRand = string.Empty;
            switch (CodeFormat)
            {
                case 1:
                    strRand = GetSixRandom();
                    break;
                case 2:
                    strRand = GetFourRandom();
                    break;
            }
            return strRand;
        }

        /// <summary>
        /// 生成订单号，19位
        /// </summary>
        public static string CreateOrderId()
        {
            StringBuilder orderId = new StringBuilder();
            //生成4位随机数
            Random rand = new Random(Guid.NewGuid().GetHashCode());
            string iRand = rand.Next(1000, 9999).ToString();
            string strTime = DateTime.Now.ToString("yyMMddHHmmssfff");

            return orderId.Append(strTime).Append(iRand).ToString();
        }

        /// <summary>
        /// 生成订单号，19位
        /// </summary>
        public static string CreateOrderId(string str)
        {
            StringBuilder orderId = new StringBuilder();
            //生成4位随机数
            Random rand = new Random(Guid.NewGuid().GetHashCode());
            string iRand = rand.Next(1000, 9999).ToString();
            string strTime = DateTime.Now.ToString("yyMMddHHmmssfff");
            if (string.IsNullOrEmpty(str))
                return orderId.Append(strTime).Append(iRand).ToString();
            else
                return orderId.Append(str).Append(strTime).Append(iRand).ToString();
        }

        /// <summary>
        /// 随机排序数组
        /// </summary>
        /// <returns></returns>
        public static int[] GetRandomArray(int[] arr)
        {
            int[] arr2 = new int[arr.Length];

            for (int i = 0; i <= arr.Length - 1; i++)
            {
                Random rd = new Random();
                int r = arr.Length - i;
                int pos = rd.Next(r);
                arr2[i] = arr[pos];
                arr[pos] = arr[r-1 ];// 将最后一位数值赋值给已经被使用的cbPosition  
            }

            return arr2;
        }


        #region 将数据存到文本文件
        //保存到文件
        /// <param name="dt">要保存的DataTable</param>
        /// <param name="FileName">文件名,注意用英文字符或者数字</param>
        public static void SaveDTToText(DataTable dt, string FileName, string encode = "utf-8")
        {
            FileName = HttpContext.Current.Server.UrlPathEncode(FileName);
            HttpResponse resp;
            resp = HttpContext.Current.Response;
            if (dt == null || dt.Rows.Count == 0)
            {
                //Alert("所下载的东西没有内容，请先查询再下载!");
                return;
            }
            resp.ContentEncoding = System.Text.Encoding.GetEncoding(encode);
            resp.AppendHeader("Content-Disposition", "attachment;filename=" + FileName);
            string colHeaders = "", ls_item = "";
            int i = 0;

            DataRow[] myRow = dt.Select("");

            //取得数据表各列标题，各标题之间以\t分割，最后一个列标题后加回车符 
            for (i = 0; i < dt.Columns.Count - 1; i++)
                colHeaders += dt.Columns[i].Caption.ToString() + "\t";
            colHeaders += dt.Columns[i].Caption.ToString() + "\r\n";
            //向HTTP输出流中写入取得的数据信息 
            resp.Write(colHeaders);
            //逐行处理数据   
            foreach (DataRow row in myRow)
            {
                //在当前行中，逐列获得数据，数据之间以\t分割，结束时加回车符\n 
                for (i = 0; i < dt.Columns.Count - 1; i++)
                    ls_item += row[i].ToString() + "\t";
                ls_item += row[i].ToString() + "\r\n";
                //当前行数据写入HTTP输出流，并且置空ls_item以便下行数据     
                resp.Write(ls_item);
                ls_item = "";
            }

            //写缓冲区中的数据到HTTP头文件中 
            resp.End();
        }

        /// <summary>
        /// 将Gridview导入到文件中 DataGrid也是一样的
        /// </summary>
        /// <param name="fileType">作为附件输出,filename=FileFlow.xls 
        /// 指定输出文件的名称,注意其扩展名和指定文件类型相符，
        /// 可以为：.doc 　　 .xls 　　 .txt 　　.htm　
        /// ContentType指定文件类型 
        /// 可以为application/ms-excel 　　 
        /// application/ms-word 　　 
        /// application/ms-txt 　　 
        /// application/ms-html 　　 
        /// 或其他浏览器可直接支持文档　 
        /// </param>
        /// <param name="fileName"></param>
        public static void SaveToFile(string fileType, string fileName, GridView GridView1)
        {
            HttpResponse resp;
            resp = HttpContext.Current.Response;
            fileName = HttpContext.Current.Server.UrlPathEncode(fileName);
            resp.Clear();
            resp.Buffer = true;
            resp.Charset = "GB2312";
            resp.AppendHeader("Content-Disposition", "attachment;filename=" + fileName);
            // 如果设置为 GetEncoding("GB2312");导出的文件将会出现乱码！！！
            resp.ContentEncoding = System.Text.Encoding.UTF7;
            resp.ContentType = fileType;//设置输出文件类型为对应文件。 
            System.IO.StringWriter oStringWriter = new System.IO.StringWriter();
            System.Web.UI.HtmlTextWriter oHtmlTextWriter = new System.Web.UI.HtmlTextWriter(oStringWriter);
            GridView1.RenderControl(oHtmlTextWriter);
            resp.Output.Write(oStringWriter.ToString());
            resp.Flush();
            resp.End();
        }

        #endregion
    }
}
