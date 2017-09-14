using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using YZ.Biz;
using YZ.Common;

namespace Web
{
    public partial class food : System.Web.UI.Page
    {
        //public string HtmlList { get; set; }
        //public string HtmlMore { get; set; }

        const int size = 24;
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {

                    if (Request["IsLoad"] == "true")
                    {
                        int i = 0;
                        int.TryParse(Request["index"], out i);
                        Response.Clear();
                        Response.Write(GetList(i));
                        Response.End();
                    }
                }
            }
            catch (Exception ex)
            {
                //throw ex;
            }
        }

        string GetList(int index)
        {
            string htmlList="";
            var data = YZ.Biz.ArticleRepository.Instance.GetArticleByType((int)YZ.Biz.DataBaseEnum.em_articleTypeList.FOOD, index, size);
            if (data != null && data.Count > 0)
            {
                foreach (var m in data)
                {
                    htmlList += "<a href='article.aspx?artid=" + m.id + "' target='_blank'><h1>" + YZ.Common.HtmlHelper.ReplaceHtmlTag(m.a_Title, 24) + "</h1>" +
                        "<div>" + YZ.Common.HtmlHelper.ReplaceHtmlTag(m.a_Content, 112) + "</div></a>";
                }
                index++;
                if (data.TotalPageCount > index)
                {
                    //HtmlMore = "<a data-index='" + index + "' data-all='" + data.TotalPageCount + "' class='btn btn-group-vertical'>加载更多</a>";
                }
                else
                    htmlList += "<div class=\" width_100per clearfix\"><div id=\"h-more\" class=\"mid\" style=\"width: 120px;font-size:22px;<!-- color:#000;--> \"><br /><p>҉没҉有҉更҉多҉了҉</p></div></div>";
                //HtmlMore = "没҉有҉更҉多҉了";
            }

            return htmlList;
        }
    }
}