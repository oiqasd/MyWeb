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
    public partial class UCLeft : System.Web.UI.UserControl
    {
        protected PageList<Article> ArticleList { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {
            //新
            ArticleList = new CacheRepository().CachedNewArticleList(0, 10);
        }
    }
}