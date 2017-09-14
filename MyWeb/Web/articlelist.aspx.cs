using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using YZ.Biz;
using YZ.Common;

namespace YZ.Web.Asp
{
    public partial class articlelist : System.Web.UI.Page
    {
        protected PageList<Article> ArticleList { get; set; }
        private string order; 
        private string type;
        protected void Page_Load(object sender, EventArgs e)
        {
            order = Request["order"];
            type = Request["type"];
            if (!IsPostBack)
            {
                Page.Title = "文章列表";
                DataBind(0, order);
            }
        }
        protected void Pager_PageChanging(object src, Wuqi.Webdiyer.PageChangingEventArgs e)
        {
            Pager.CurrentPageIndex = e.NewPageIndex;

            DataBind(Pager.CurrentPageIndex - 1, order);
        }

        protected void DataBind(int index, string order)
        {
            ArticleList = ArticleRepository.Instance.GetNewArticlePageList(index, Pager.PageSize, order);
            Pager.RecordCount = ArticleList.TotalItemCount;
        }
       
    }
}