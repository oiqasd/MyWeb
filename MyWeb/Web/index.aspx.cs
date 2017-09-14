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
    public partial class index : BasePage
    {
        public PageList<Article> HotArticleList { get; set; }
        public PageList<Article> NewArticleList { get; set; }
        const int pagesize = 6;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                S_DataBind();
            }
        }

        protected void S_DataBind()
        {
            HotArticleList = CacheRepository.Instance.CachedHomeHotArticleList(0, pagesize);
            NewArticleList = CacheRepository.Instance.CachedHomeNewArticleList(0, pagesize);
        }

        public int LikeCount(int aid)
        {
            if (aid <= 0)
                return 0;
            return ArticleRepository.Instance.GetLikeCount(aid).Count;
        }
    }
}