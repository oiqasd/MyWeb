using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YZ.Common
{
    public static class HtmlHelper
    {
        #region 去除html标签
        public static string ReplaceHtmlTag(string html, int length)
        {
            string strText = System.Text.RegularExpressions.Regex.Replace(html, "<[^>]+>", "");
            strText = System.Text.RegularExpressions.Regex.Replace(strText, "&[^;]+;", "");

            if (length > 0 && strText.Length > length)
                return strText.Substring(0, length-2)+"...";

            return strText;
        }
        #endregion
    }
}
