using System;
using System.Web;
using System.IO;
using System.Text;
using System.Configuration;

namespace YZ.Common
{
    public class AspxToHtml
    {
        #region 变量
        /// <summary>
        /// 模板文件中要替代的参数个数
        /// </summary>
        private int _templateParamCount = 0;
        /// <summary>
        ///  模板文件所在的路径
        /// </summary>
        private string _templateFilePath = ConfigurationManager.AppSettings["TemplateFilePath"];
        /// <summary>
        /// 转换后的HTML文件所存放的路径
        /// </summary>
        private string _HTMLFilePath = ConfigurationManager.AppSettings["HtmlFilePath"];
        private string _coding = ConfigurationManager.AppSettings["Encoding"];
        /// <summary>
        /// 模板页页面编码
        /// </summary>
        private Encoding _templateHTMLCode;// = Encoding.GetEncoding("gb2312");
        /// <summary>
        /// 转换后的文件编码
        /// </summary>
        private Encoding _code;// = Encoding.GetEncoding("gb2312");
        /// <summary>
        /// 转换后的HTML文件名
        /// </summary>
        private string _convertedFilename = "";
        /// <summary>
        /// 模板文件中的参数
        /// </summary>
        private string[] _templateFileparameter;
        /// <summary>
        /// ASPx文件中的要代替HTML文件中的参数实际值
        /// </summary>
        private string[] _ASPxFileparameter;
        /// <summary>
        /// 错误日志路径
        /// </summary>
        private string _errlogPath = ConfigurationManager.AppSettings["ToHtmlErrLogPath"];

        #endregion
        
        #region 属性
        /// <summary>
        /// 模板文件中要替代的参数个数
        /// </summary>
        public int TemplateParamCount
        {
            get
            {
                return this._templateParamCount;
            }
            set//分配参数个数时，同时为模板文件中的参数和ASPx文件中的要代替HTML文件中的参数实际值这两个分配实际数组
            {
                if (value < 0)
                    throw new ArgumentException();

                if (value > 0)
                {
                    this._templateParamCount = value;
                    //模板文件中的参数                    
                    _templateFileparameter = new string[value];
                    //ASPx文件中的要代替HTML文件中的参数实际值
                    _ASPxFileparameter = new string[value];
                }
                else
                    this._templateParamCount = 0;
            }
        }

        /// <summary>
        /// 模板文件所在的路径
        /// </summary>
        public string TemplateFilePath
        {
            get { return this._templateFilePath; }
            set { this._templateFilePath = value; }
        }

        /// <summary>
        /// 转换后的HTML文件所存放的路径
        /// </summary>
        public string HTMLFilePath
        {
            get { return this._HTMLFilePath; }
            set { this._HTMLFilePath = value; }
        }

        /// <summary>
        /// HTML模板文件编码
        /// </summary>
        public Encoding TemplateHTMLCode
        {
            get { return this._templateHTMLCode; }
            set
            {
                this._templateHTMLCode = Encoding.GetEncoding(_coding);
                //this._templateHTMLCode = Encoding.GetEncoding(value.ToString());
            }
        }

        /// <summary>
        /// 编码
        /// </summary>
        public Encoding Code
        {
            get { return this._code; }
            set { this._code = Encoding.GetEncoding(_coding); }
        }

        /// <summary>
        /// 错误文件所在路径
        /// </summary>
        public string ErrLogPath
        {
            get
            {
                if (!(this._errlogPath == null))
                    return this._errlogPath;
                else
                    return "AspxToHtml_log.txt";
            }
            set { this._errlogPath = value; }
        }
        #endregion

        #region 构造

        public AspxToHtml()
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //            
        }

        private void settemplateParamCount(int templateParamCount)
        {
            if (templateParamCount > 0)
                this.TemplateParamCount = templateParamCount;
        }
        /**/
        ///

        /// 提供欲代替的参数个数
        ///

        /// 
        public AspxToHtml(int templateParamCount)
        {
            settemplateParamCount(templateParamCount);

        }
        /**/
        ///

        /// 
        ///

        /// HTML模板页中的参数个数
        /// HTMLFilePath">生成的HTML文件所存放的文件夹路径
        /// HTML模板页路径
        public AspxToHtml(int templateParamCount, string HTMLFilePath, string templateFilePath)
        {
            settemplateParamCount(templateParamCount);
            this.HTMLFilePath = HTMLFilePath;
            this.TemplateFilePath = templateFilePath;

        }
        #endregion

        #region 操作

        /// 获取转换后的HTML文件所在相对文件路径
        /// 如：如果HTMLFilePath="/news/"
        /// 转换后的HTML文件名为200505050505.HTML
        /// 则返回的值为/news/200505050505.HTML
        ///

        /// 如果在未调用StartConvert方法之前调用此属性则返回null
        public string HTMLFileVirtualPath
        {
            get
            {
                if (!string.IsNullOrEmpty(this._convertedFilename))
                    return this.HTMLFilePath + this._convertedFilename;
                else
                    return null;
            }
        }

        ///
        /// 为HTML页面参数数组付值
        ///
        /// 
        public void setTemplateFileparameter(string[] param)
        {
            try
            {
                if (param.Length == this.TemplateParamCount)
                    this._templateFileparameter = param;
                //else//与原定义的个数不等
                //
            }
            catch (System.Exception ex)
            {
                WriteErrFile(ex);
            }
        }
      
        ///
        /// 为ASPx文件中将要替换HTML文件中的参数数组付值
        ///
        /// 
        public void setASPxFileparameter(string[] param)
        {
            try
            {
                if (param.Length == this.TemplateParamCount)
                    this._ASPxFileparameter = param;
                //else//与原定义的个数不等
                //
            }
            catch (System.Exception ex)
            {
                WriteErrFile(ex);
            }
        }
        /**/
        ///

        /// 开始进行ASPxToHTML转换
        ///

        /// 返回值为成功创建后的文件名称
        /// 在调用此方法之前必需确定已调用setTemplateFileparameter 和setASPxFileparameter方法进行相应的付值操作
        public string StartConvert()
        {
            if (this._templateFileparameter.Length == this._ASPxFileparameter.Length)
            {
                return writeFile();
            }
            else
            {
                return null;
            }
        }
        ///

        /// 开始进行ASPxToHTML转换
        ///

        /// HTMLparam">HTML模板页中的所有参数数组
        /// ASPxparam">ASPx页面中要代替HTML模板页中参数值数组
        /// 返回值为成功创建后的文件名称
        public string StartConvert(string[] HTMLparam, string[] ASPxparam)
        {
            //先调用setTemplateFileparameter 和setASPxFileparameter方法，进行付值操作
            setTemplateFileparameter(HTMLparam);
            setASPxFileparameter(ASPxparam);
            //返回文件名
            string fn = this.StartConvert();
            //
            _convertedFilename = fn;
            //
            return fn;
        }

        /**/
        ///
        /// 用时间加随机数生成一个文件名
        ///
        /// 
        private string getfilename()
        {
            //用时间加随机数生成一个文件名
            System.Threading.Thread.Sleep(50);
            string yearStr = System.DateTime.Now.Year.ToString();
            string monthStr = string.Format("{0:0#}", System.DateTime.Now.Month);
            string dayStr = string.Format("{0:0#}", System.DateTime.Now.Day);
            string hourStr = string.Format("{0:0#}", System.DateTime.Now.Hour);
            string minuteStr = string.Format("{0:0#}", System.DateTime.Now.Minute);
            string secondStr = string.Format("{0:0#}", System.DateTime.Now.Second);
            string millisecondStr = string.Format("{0:000#}", System.DateTime.Now.Millisecond);
            System.Random rd = new System.Random();
            return yearStr + monthStr + dayStr + hourStr + minuteStr + secondStr + millisecondStr + string.Format("{0:0000#}", rd.Next(100)) + ".HTML";
            //return DateTime.Now.ToString("yyyyMMddHHmmss")+".HTML";
        }
        /**/
        ///

        /// 进行转换处理
        ///

        /// 返回以时间命名的文件名
        private string writeFile()
        {

            // 读取模板文件
            string temp = HttpContext.Current.Server.MapPath(this.TemplateFilePath);
            StreamReader sr = null;
            string str = "";
            try
            {
                sr = new StreamReader(temp, this.TemplateHTMLCode);
                str = sr.ReadToEnd(); // 读取文件
            }
            catch (Exception ex)
            {
                //HttpContext.Current.Response.Write(exp.Message);
                //HttpContext.Current.Response.End();        
                WriteErrFile(ex);
            }
            finally
            {
                sr.Close();
            }
            // 替换内容
            // 这时,模板文件已经读入到名称为str的变量中了
            for (int i = 0; i < this._templateParamCount; i++)
            {
                str = str.Replace(this._templateFileparameter[i], this._ASPxFileparameter[i]);
            }

            return savefile(str);
        }


        private string savefile(string str)
        {
            // 写文件
            StreamWriter sw = null;
            try
            {

                string path = HttpContext.Current.Server.MapPath(this.HTMLFilePath);
                //HTML文件名称    
                string HTMLfilename = getfilename();
                sw = new StreamWriter(path + HTMLfilename, false, this.Code);
                sw.Write(str);
                sw.Flush();
                return HTMLfilename;
            }
            catch (Exception ex)
            {
                WriteErrFile(ex);
            }
            finally
            {
                sw.Close();
            }
            return "";
        }

        /**/
        /// 
        /// 传入URL返回网页的HTML代码
        /// 
        /// URL
        /// 
        public string getUrltoHTML(string Url)
        {
            try
            {
                System.Net.WebRequest wReq = System.Net.WebRequest.Create(Url);
                System.Net.WebResponse wResp = wReq.GetResponse();
                System.IO.Stream respStream = wResp.GetResponseStream();
                System.IO.StreamReader reader = new System.IO.StreamReader(respStream, Code);
                return savefile(reader.ReadToEnd());

            }
            catch (System.Exception ex)
            {
                WriteErrFile(ex);
            }
            return "";
        }
        #endregion
         
        #region 错误日志

        /**/
        ///
        /// 把错误写入文件方法#region 把错误写入文件方法
        ///
        /// 
        private void WriteErrFile(Exception ee)
        {

            FileStream fs1 = new FileStream(HttpContext.Current.Server.MapPath(ErrLogPath), System.IO.FileMode.Append);
            StreamWriter sw1 = new StreamWriter(fs1);
            sw1.WriteLine("**************************************************");
            sw1.WriteLine("错误日期:" + System.DateTime.Now);
            sw1.WriteLine("错误描述:" + ee.Message);
            sw1.WriteLine("错误名称:" + ee.Source);
            sw1.WriteLine("详细:" + ee.ToString());
            sw1.WriteLine("*************************************************");
            sw1.Close();
        }

        #endregion

    }
}
