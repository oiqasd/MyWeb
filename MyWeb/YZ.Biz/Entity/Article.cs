using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YZ.Biz.Entity
{
    public class E_Article
    {
        public E_Article()
        {
            articleId = -10;
            articleTitle = null;
            articleTypeId = -10;
            articleType = null;
            articleContent = null;
            articleUserId = -10;
            articleNickName = null;
            articleCreateDate = null;
            articleFrom = null;
            self = false;
        }
        public long articleId { get; set; }//ID
        public string articleTitle { get; set; }//标题
        public int articleTypeId { get; set; } //文章类别ID 
        public string articleType { get; set; } //文章类别
        public string articleContent { get; set; } //文章类别Content
        public long articleUserId { get; set; } //用户Id
        public string articleNickName { get; set; } //用户显示名称
        public string articleCreateDate { get; set; } //创建时间
        public string articleFrom { get; set; }//来源、作者 
        public bool self { get; set; }//是否本人可编辑
    }

    /// <summary>
    /// Article页面拓展
    /// 上一篇下一篇功能
    /// </summary>
    //public class PieceArticle
    //{
    //    public List<PrevArticle> prev { get; set; }
    //    public List<NextArticle> next { get; set; }
    //}
    ////上一篇
    //public class PrevArticle
    //{
    //    public long pid { get; set; }
    //    public string pa_Title { get; set; }
    //}
    ////下一篇
    //public class NextArticle
    //{
    //    public long id { get; set; }
    //    public string a_Title { get; set; }
    //}
    /// <summary>
    /// Article页面拓展
    /// 上一篇下一篇功能
    /// </summary>
    public class ExArticle
    {
        public long id { get; set; }
        public string a_Title { get; set; }
    }
}
