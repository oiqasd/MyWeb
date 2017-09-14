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
    public partial class userCenter : BasePage
    {
        //protected PageList<Article> ArticleList { get; set; }
        //const int pagesize = 10;
        protected void Page_Load(object sender, EventArgs e)
        {
            base.Page_Load(sender, e);
            //if (!IsPostBack)
            //{
            //    ArticleList = new CacheRepository().CachedArticleList(0, pagesize);
            //}
        }
    }
}