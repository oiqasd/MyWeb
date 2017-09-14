using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Web.Util;
using YZ.Biz;
using YZ.Common;
using Web.Util.Expand;

namespace YZ.Web.Asp
{
    /// <summary>
    /// 本人发布的
    /// </summary>
    public partial class userArticle : BasePage
    {
        public PageList<Article> MyArticleList { get; set; }
        long userid;
        public bool IsSelf = false;//当前是否是本人
        protected void Page_Load(object sender, EventArgs e)
        {
            //base.Page_Load(sender, e);
            try { userid = int.Parse(Request.QueryString["userid"]); }
            catch { userid = 0; }
            if (!IsPostBack)
            {
                if (userid <= 0) userid = UserID;
                if (userid <= 0)
                {
                    //Page.ClientScript.RegisterStartupScript(Page.GetType(), "", "<script> $.MsgBox.Alert('提示','未找到数据，点击确定返回上一页。',function(){window.history.go(-1);});</script>");
                    return;
                }
                //绑定数据
                DataBind(0);
                if (userid == UserID)
                {
                    IsSelf = true;
                    Page.Title = "我的文章列表";
                }
                else Page.Title = (MyArticleList != null && MyArticleList.Count > 0) ? (MyArticleList[0].UserInfo.U_NickName + "的文章中心") : "";// Server.UrlDecode(Request.QueryString["um"]) + "的文章中心";

            }
        }

        protected void Pager_PageChanging(object src, Wuqi.Webdiyer.PageChangingEventArgs e)
        {
            Pager.CurrentPageIndex = e.NewPageIndex;

            DataBind(Pager.CurrentPageIndex - 1);
        }

        private void DataBind(int index)
        {

            MyArticleList = ArticleRepository.Instance.GetArticlePageListByUid(userid, index, Pager.PageSize);
            // new CacheRepository().CachedArcitleListByUserId(UserID, Pager.CurrentPageIndex - 1, pagesize);

            Pager.RecordCount = MyArticleList.TotalItemCount;
        }

    }
}