using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using YZ.Common;

namespace YZ.Biz
{
    public class ArticleRepository : BaseRepository
    {
        #region Instance
        private static ArticleRepository _instance;
        private static object _syncObject = new object();
        public static ArticleRepository Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (_syncObject)
                    {
                        if (_instance == null)
                        {
                            _instance = new ArticleRepository();
                        }
                    }
                }
                return _instance;
            }
        }
        #endregion

        /// <summary>
        /// 添加文章
        /// </summary> 
        /// <returns></returns>
        public bool AddArticle(Article _article)
        {
            _Context.Articles.Add(_article);
            return _Context.SaveChanges() > 0;
        }

        /// <summary>
        /// 更新文章
        /// </summary> 
        /// <returns></returns>
        public bool UpdateArticle(Article _article)
        {
            var ainfo = _Context.Articles.Where(m => m.id == _article.id).FirstOrDefault();
            ainfo.a_Title = _article.a_Title;
            ainfo.a_TypeId = _article.a_TypeId;
            ainfo.a_Content = _article.a_Content;
            ainfo.a_From = _article.a_From;
            ainfo.a_IsTop = _article.a_IsTop;
            ainfo.a_KeyWord = _article.a_KeyWord;
            ainfo.a_ModifiedBy = _article.a_ModifiedBy;
            ainfo.a_ModifiedDate = DateTime.Now;
            return _Context.SaveChanges() > 0;
        }

        /// <summary>
        /// 根据ID查找文章
        /// </summary> 
        /// <returns></returns>
        public Article GetArticleById(long id)
        {
            Article article = _Context.Articles.Where(m => m.id == id).FirstOrDefault();
            return article;
        }

        /// <summary>
        /// 按热度获取文章分页
        /// </summary>
        /// <param name="i"></param>
        /// <param name="n"></param>
        /// <returns></returns>
        public PageList<Article> GetArticlePageList(int index, int size)
        {
            return _Context.Articles.OrderByDescending(m => m.a_IsHot).ThenByDescending(m => m.id).ToPageList(index, size);
        }
        /// <summary>
        /// 按发布时间获取文章分页
        /// </summary>
        /// <param name="i"></param>
        /// <param name="n"></param>
        /// <param name="order">1（默认）：时间；2：按点击率；3：用户</param>
        /// <returns></returns>
        public PageList<Article> GetNewArticlePageList(int index, int size, string order = "", int type = -10)
        {
            if (string.IsNullOrEmpty(order)) order = "";

            if (order == "2") return _Context.Articles.Where(m => type == -10 || m.a_TypeId == type).OrderByDescending(m => m.a_Statistics).ToPageList(index, size);
            else if (order == "3") return _Context.Articles.Where(m => type == -10 || m.a_TypeId == type).OrderByDescending(m => m.a_CreateBy).ToPageList(index, size);
            else return _Context.Articles.Where(m => type == -10 || m.a_TypeId == type).OrderByDescending(m => m.a_CreateDate).ToPageList(index, size);
        }
        /// <summary>
        /// 根据用户Id获取文章分页
        /// </summary>
        /// <param name="i"></param>
        /// <param name="n"></param>
        /// <returns></returns>
        public PageList<Article> GetArticlePageListByUid(long uid, int index, int size)
        {
            return _Context.Articles.Where(m => m.a_CreateBy == uid || m.a_ModifiedBy == uid).OrderByDescending(m => m.a_IsHot).ThenByDescending(m => m.id).ToPageList(index, size);
        }

        /// <summary>
        /// 检索文章分页
        /// </summary>
        /// <param name="keyword"></param>
        /// <param name="index"></param>
        /// <param name="size"></param>
        /// <returns></returns>
        public PageList<Article> GetSearchResult(string keyword, int index, int size)
        {
            return _Context.Articles.Where(m => m.a_Title.Contains(keyword) || m.a_KeyWord.Contains(keyword)).OrderBy(m => m.a_IsHot).ThenByDescending(m => m.a_ModifiedDate).ToPageList(index, size);
        }

        /// <summary>
        /// 根据类别筛选文章列表
        /// </summary>
        /// <param name="typeid"></param>
        /// <param name="index"></param>
        /// <param name="size"></param>
        /// <returns></returns>
        public PageList<Article> GetArticleByType(int typeid, int index, int size)
        {
            return _Context.Articles.Where(m => m.a_TypeId == typeid).OrderByDescending(m => m.id).ToPageList(index, size);
        }
        /// <summary>
        /// 获取文章详情
        /// </summary>
        /// <param name="id"></param> 
        /// <returns>Article</returns>
        public Article GetArticleDetail(long id)
        {
            return _Context.Articles.FirstOrDefault(m => m.id == id);
        }

        /// <summary>
        /// 统计点击数
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool SetStatistic(long id)
        {
            var list = _Context.Articles.Where(m => m.id == id).FirstOrDefault();
            if (list != null)
            {
                list.a_Statistics = list.a_Statistics ?? 0;
                list.a_Statistics += 1;
                return _Context.SaveChanges() > 0;
            }
            return false;
        }

        /// <summary>
        /// 侧边导航-我的文章
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        //public List<Article> GetArticleById(int id)
        //{
        //    var list = _Context.Articles.Where(m => m.a_CreateBy == id).FirstOrDefault();

        //    return list;
        //}

        /// <summary>
        /// 获取点赞数
        /// </summary>
        /// <param name="aid"></param>
        /// <returns></returns>
        public List<LikeCount> GetLikeCount(int aid)
        {
            return _Context.LikeCounts.Where(m => m.a_id == aid && m.stats == 0).ToList();
        }

        /// <summary>
        /// 新闻类别
        /// </summary>
        /// <returns></returns>
        public List<ArticleType> ArticleType()
        {
            return _Context.ArticleTypes.ToList();
        }

        /// <summary>
        /// 添加新闻类别
        /// </summary>
        /// <returns></returns>
        public bool AddArticleType(ArticleType type)
        {
            _Context.ArticleTypes.Add(type);
            return _Context.SaveChanges() > 0;
        }
        /// <summary>
        /// 删除新闻类别
        /// </summary>
        /// <returns></returns>
        public bool DeleteArticleType(ArticleType type)
        {
            var data = _Context.ArticleTypes.Where(m => m.id == type.id).FirstOrDefault();
            if (data != null)
            {
                data.a_t_Flag = 0;
            }
            return _Context.SaveChanges() > 0;
        }
        /// <summary>
        /// 物理删除新闻列表
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool PhysicsDeleteArticleType(int id)
        {
            var data = _Context.ArticleTypes.Where(m => m.id == id).FirstOrDefault();
            _Context.ArticleTypes.Remove(data);
            return _Context.SaveChanges() > 0;
        }
    }
}
