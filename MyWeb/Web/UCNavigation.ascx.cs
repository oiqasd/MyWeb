using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Web.Util;
using YZ.Biz;
using YZ.Common;

namespace Web
{
    public partial class UCNavigation : BaseUserControl
    {
        /// <summary>
        /// 用户信息
        /// </summary>
        protected UserInfo userinfo { get; set; }

       /// <summary>
       /// 热门文章
       /// </summary>
        protected PageList<Article> ArticleList { get; set; }

        /// <summary>
        /// 我的文章
        /// </summary>
        protected PageList<Article> MyArticleList { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {
            base.Page_Load(sender, e);

            //我的文章
            userinfo = new CacheRepository().CachedUserInfoByName(UserName);

            //热门文章
            ArticleList = new CacheRepository().CachedHotArticleList(0,6);

            //我的文章
            MyArticleList = new CacheRepository().CachedArcitleListByUserId(UserID,0, 6);
        }
    }
}