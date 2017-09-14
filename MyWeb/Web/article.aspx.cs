using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using YZ.Biz;
using Web.Server;
using Web.Helper;
using Web.Util.Expand;
using YZ.Biz.Entity;
using Web.Util;

/// <summary>
///文章详情页
///2015-4-1
/// </summary>
namespace YZ.Web.Asp
{
    public partial class article : BasePage
    {

        protected E_Article e_Article;
        protected string prevArticle;
        protected string nextArticle;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                e_Article = new E_Article();
                var articleId = Request["artid"];
                long id;
                if (!string.IsNullOrWhiteSpace(articleId) && long.TryParse(articleId, out id) && id > 0)
                {
                    UserControl uc = (UserControl)Page.LoadControl("control/UcComment.ascx");
                    uc.Attributes["aid"] = id.ToString();
                    pholder.Controls.Add(uc);
                    getArticle(id);
                    setStatistic(id); 
                }
                else
                {
                    Response.Redirect("articlelist.aspx");
                    //details.InnerHtml = "<a style=\"font-size:20pt;color:red; margin-top:30px;\">资源已过期，或该内容已删除。</a>";
                }

            }
        }

        /// <summary>
        /// 获取文章信息
        /// </summary>
        /// <param name="id"></param>
        private void getArticle(long id)
        {
            ArticleRepository biz = new ArticleRepository();

            var result = biz.GetArticleDetail(id);
            if (result != null)
            {
                //内容
                details.InnerHtml = result.a_Content;
                e_Article.articleId = result.id;
                e_Article.articleContent = result.a_Content;
                //类别
                e_Article.articleTypeId = result.ArticleType.id;
                e_Article.articleType = result.ArticleType.a_t_ArticleCategory;
                //日期
                e_Article.articleCreateDate = result.a_CreateDate.ToString(1);
                //标题 
                e_Article.articleTitle = result.a_Title;

                if (!(Session[SessionHelper.SessionKey_User_UserID] == null || (long)Session[SessionHelper.SessionKey_User_UserID] == 0))
                    if (result.a_CreateBy == (long)Session[SessionHelper.SessionKey_User_UserID]) e_Article.self = true;

                //来源、作者
                if (!string.IsNullOrWhiteSpace(result.a_From))
                {
                    e_Article.articleFrom = "来源：" + result.a_From + "　";
                }
                e_Article.articleFrom = e_Article.articleFrom + "作者：" + "<a href='userArticle.aspx?userid=" + result.a_CreateBy + "'>" + result.UserInfo.U_NickName + "</a>";

                e_Article.articleUserId = result.UserInfo.U_Id;
                e_Article.articleNickName = result.UserInfo.U_NickName;

                Page.Title = result.a_Title + "[" + result.UserInfo.U_NickName + "]";
                //获取上下篇
                var prev = YZ.Biz.SqlRepository.Instance.GetPrevArticle(id);
                if (prev == null)
                    prevArticle = "没有了";
                else
                {
                    prevArticle = string.Format("<a href='article.aspx?artid={0}' class='link'>{1}</a>", prev.id, prev.a_Title);
                }
                var next = YZ.Biz.SqlRepository.Instance.GetNextArticle(id);
                if (next == null)
                    nextArticle = "没有了";
                else
                {
                    nextArticle = string.Format("<a href='article.aspx?artid={0}' class='link'>{1}</a>", next.id, next.a_Title);
                }
            }
            else
            {
                Response.Redirect("error/NoAccess.html");
                //details.InnerHtml = "<a style=\"font-size:20pt;color:red; margin-top:30px;\">检索失败请重试，或该内容已删除</a>";
            }
        }

        /// <summary>
        /// 流量统计（未考虑并发）
        /// </summary>
        /// <param name="id"></param>
        private void setStatistic(long id)
        {
            //CommonServer
            Thread th = new Thread(CommonServer.Statistic);
            th.Start(id);

        }
    }
}