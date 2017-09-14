using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Web.Util;
using YZ.Biz;
using YZ.Common;

namespace YZ.Web.Asp
{
    /// <summary>
    /// 我+好友动态 
    /// </summary>
    public partial class userDefault : BasePage
    {
        protected PageList<Article> ArticleList { get; set; } 
        protected void Page_Load(object sender, EventArgs e)
        {
            base.Page_Load(sender, e);
            if (!IsPostBack)
            {
                Page.Title = "动态中心";
                DataBind(0);
            }
        }
        protected void Pager_PageChanging(object src, Wuqi.Webdiyer.PageChangingEventArgs e)
        {
            Pager.CurrentPageIndex = e.NewPageIndex;

            DataBind(Pager.CurrentPageIndex - 1);
        }

        protected void DataBind(int index)
        {
            ArticleList = ArticleRepository.Instance.GetNewArticlePageList(index, Pager.PageSize);
            Pager.RecordCount = ArticleList.TotalItemCount;
        }
    }
}