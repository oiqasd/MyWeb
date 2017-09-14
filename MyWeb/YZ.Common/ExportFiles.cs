using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YZ.Common
{
    public class ExportFiles
    {

        public void ExportDsToXls(System.Web.UI.Page page, DataSet ds, string fileName)
        {
            string data = ExportTable(ds);

            if (string.IsNullOrEmpty(data)) return;

            page.Response.Clear();
            page.Response.Buffer = true;
            page.Response.Charset = ConfigHelper.AppSetting<string>("Encoding");// "GB2312";
            page.Response.AddHeader("Content-Disposition", "attachment;filename=" + fileName + ".xls");
            page.Response.ContentEncoding = Encoding.GetEncoding(ConfigHelper.AppSetting<string>("Encoding"));//设置输出流为简体中文
            page.Response.ContentType = "application/ms-excel";//设置输出文件类型为excel文件。 
            page.EnableViewState = false;
            page.Response.Write(data);
            page.Response.End();
        }

        public string ExportTable(DataSet ds)
        {
            if (ds == null || ds.Tables.Count <= 0)
            {
                return string.Empty;
            }

            if (ds.Tables[0] == null || ds.Tables[0].Rows.Count <= 0)
            {
                return string.Empty;
            }

            StringBuilder sb = new StringBuilder();
            try
            {

                foreach (DataTable dt in ds.Tables)
                {
                    sb.AppendLine("<meta http-equiv=\"Content-Type\" content=\"text/html; charset=" + ConfigHelper.AppSetting<string>("Encoding") + "\">");
                    sb.AppendLine("<table cellspacing=\"0\" cellpadding=\"5\" rules=\"all\" border=\"1\">");

                    //写出列名
                    sb.Append("<tr style=\"font-weight: bold; white-space: nowrap;\">");
                    foreach (DataColumn column in dt.Columns)
                    {
                        sb.Append("<td>");
                        sb.Append(column.ColumnName);
                        sb.Append("</td>");
                    }

                    sb.Append("</tr>");

                    //show data

                    foreach (DataRow dr in dt.Rows)
                    {
                        sb.Append("<tr>");
                        foreach (DataColumn column in dt.Columns)
                        {
                            if (column.ColumnName.Trim().Equals("编号"))
                                sb.Append("<td style=\"vnd.ms-excel.numberformat:@\">");// 避免显示科学计数法
                            else
                                sb.Append("<td>");

                            if (dr[column] != null)
                                sb.Append(dr[column].ToString());

                            sb.Append("</td>");
                        }
                        sb.Append("</tr>");
                    }
                    sb.Append("</table>");
                }

                return sb.ToString();
            }
            catch
            {
                return string.Empty;
            }
            finally
            {
            }
        }
    }
}
