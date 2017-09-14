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
    public partial class UCRight : System.Web.UI.UserControl
    {
        protected PageList<Article> ArticleList { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {
            //热门文章
            ArticleList = new CacheRepository().CachedHotArticleList(0, 10);
        }
    }
}