using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using YZ.Biz;

namespace YZ.Web.Asp
{
    public partial class searchResult : System.Web.UI.Page
    {
        protected YZ.Common.PageList<Article> resultList { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {
            var value = "";

            if (!IsPostBack)
            {
                value = Request["keyword"];
                txtKeyWord.Value = value; ;
            }
            dataBind();
        }
        protected void Pager_PageChanging(object src, Wuqi.Webdiyer.PageChangingEventArgs e)
        {
            Pager.CurrentPageIndex = e.NewPageIndex;

            dataBind();
        }

        private void dataBind()
        {
            ArticleRepository biz = new ArticleRepository();
            var value = txtKeyWord.Value;
            if (string.IsNullOrWhiteSpace(value))
            {
                resultList = biz.GetArticlePageList(Pager.CurrentPageIndex - 1, Pager.PageSize);
            }
            else
            {
                resultList = biz.GetSearchResult(value, Pager.CurrentPageIndex - 1, Pager.PageSize);
            }
            Pager.RecordCount = resultList.TotalItemCount;
        }
    }
}