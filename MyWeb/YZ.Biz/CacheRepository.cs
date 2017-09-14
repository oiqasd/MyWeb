using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YZ.Common;

namespace YZ.Biz
{
    public class CacheRepository : BaseRepository
    {
        #region
        /// <summary>
        /// 缓存常量：用户信息
        /// </summary>
        const string CKey_CZZ_UserInfo = "CKey_CZZ_UserInfo";

        ///// <summary>
        ///// 站点投资信息统计
        ///// </summary>
        //const string CKey_HJS_SiteStatistics = "CKey_HJS_SiteStatistics";

        /// <summary>
        ///我的文章列表
        /// </summary>
        const string CKey_CZZ_MyArticleList = "CKey_CZZ_MyArticle";

        /// <summary>
        ///首页最新文章列表
        /// </summary>
        const string CKey_CZZ_PubHomeArticleList = "CKey_CZZ_PubHomeArticleList";

        /// <summary>
        ///首页文章列表
        /// </summary>
        const string CKey_CZZ_HotHomeArticleList = "CKey_CZZ_HotHomeArticleList";

        /// <summary>
        ///最新文章列表
        /// </summary>
        const string CKey_CZZ_PubArticleList = "CKey_CZZ_PubArticleList";

        /// <summary>
        ///热门文章列表
        /// </summary>
        const string CKey_CZZ_HotArticleList = "CKey_CZZ_HotArticleList";

        /// <summary>
        ///热门文章类别
        /// </summary>
        const string CKey_CZZ_ArticleType = "CKey_CZZ_ArticleType";

        #endregion
        CacheHelper _CacheHelper;

        public CacheRepository()
        {
            _CacheHelper = new CacheHelper();
        }

        #region Instance
        private static CacheRepository _instance;
        private static object _syncObject = new object();
        public static CacheRepository Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (_syncObject)
                    {
                        if (_instance == null)
                        {
                            _instance = new CacheRepository();
                        }
                    }
                }
                return _instance;
            }
        }
        #endregion

        #region Dictionary

        #region 个人中心
        /// <summary>
        /// 缓存用户信息
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public UserInfo CachedUserInfoByName(string username)
        {
            //get{
            var _CachedUserInfo = _CacheHelper.GetItem<UserInfo>(CKey_CZZ_UserInfo);
            if (_CachedUserInfo == null || _CachedUserInfo.U_Id <= 0)
            {
                _CachedUserInfo = _Context.UserInfoes.Where(m => m.U_UserName == username).FirstOrDefault();
                _CacheHelper.addItem(CKey_CZZ_UserInfo, _CachedUserInfo, CacheHelper.Expiration.TwoMin);
            }
            return _CachedUserInfo;
            //}
        }

        /// <summary>
        /// 缓存我的文章列表
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public PageList<Article> CachedArcitleListByUserId(long userID, int index, int size)
        {

            var _CachedArcitleListByUserId = _CacheHelper.GetItem<PageList<Article>>(CKey_CZZ_MyArticleList);
            if (_CachedArcitleListByUserId == null || _CachedArcitleListByUserId.Count <= 0)
            {
                _CachedArcitleListByUserId = _Context.Articles.Where(m => m.a_CreateBy == userID).OrderByDescending(m => m.a_CreateDate).ToPageList(index, size);
                _CacheHelper.addItem(CKey_CZZ_MyArticleList, _CachedArcitleListByUserId, CacheHelper.Expiration.FiveSecond);
            }
            return _CachedArcitleListByUserId;
        }
        #endregion

        #region 公共

        #region 首页
        /// <summary>
        /// 最近文章
        /// </summary>
        /// <param name="index"></param>
        /// <param name="size"></param>
        /// <returns></returns>
        public PageList<Article> CachedHomeNewArticleList(int index, int size)
        {
            var _CachedArticleList = _CacheHelper.GetItem<PageList<Article>>(CKey_CZZ_PubHomeArticleList);
            if (_CachedArticleList == null || _CachedArticleList.Count<=0)
            {
                _CachedArticleList = _Context.Articles.OrderByDescending(m => m.a_CreateDate).ToPageList(index, size);
                _CacheHelper.addItem(CKey_CZZ_PubHomeArticleList, _CachedArticleList, CacheHelper.Expiration.OneMin);
            }
            return _CachedArticleList;
        }


        /// <summary>
        /// 热门文章
        /// </summary>
        /// <param name="index"></param>
        /// <param name="size"></param>
        /// <returns></returns>
        public PageList<Article> CachedHomeHotArticleList(int index, int size)
        {
            var _CachedArticleList = _CacheHelper.GetItem<PageList<Article>>(CKey_CZZ_HotHomeArticleList);
            if (_CachedArticleList == null || _CachedArticleList.Count <= 0)
            {
                _CachedArticleList = _Context.Articles.OrderByDescending(m => m.a_Statistics).ToPageList(index, size);
                _CacheHelper.addItem(CKey_CZZ_HotHomeArticleList, _CachedArticleList, CacheHelper.Expiration.TwoMin);
            }
            return _CachedArticleList;
        }
        #endregion
        /// <summary>
        /// 最近文章
        /// </summary>
        /// <param name="index"></param>
        /// <param name="size"></param>
        /// <returns></returns>
        public PageList<Article> CachedNewArticleList(int index, int size)
        {
            var _CachedArticleList = _CacheHelper.GetItem<PageList<Article>>(CKey_CZZ_PubArticleList);
            if (_CachedArticleList == null || _CachedArticleList.Count<=0)
            {
                _CachedArticleList = _Context.Articles.OrderByDescending(m => m.a_CreateDate).ToPageList(index, size);
                _CacheHelper.addItem(CKey_CZZ_PubArticleList, _CachedArticleList, CacheHelper.Expiration.OneMin);
            }
            return _CachedArticleList;
        }


        /// <summary>
        /// 热门文章
        /// </summary>
        /// <param name="index"></param>
        /// <param name="size"></param>
        /// <returns></returns>
        public PageList<Article> CachedHotArticleList(int index, int size)
        {
            var _CachedArticleList = _CacheHelper.GetItem<PageList<Article>>(CKey_CZZ_HotArticleList);
            if (_CachedArticleList == null || _CachedArticleList.Count <= 0)
            {
                _CachedArticleList = _Context.Articles.OrderByDescending(m => m.a_Statistics).ToPageList(index, size);
                _CacheHelper.addItem(CKey_CZZ_HotArticleList, _CachedArticleList, CacheHelper.Expiration.TwoMin);
            }
            return _CachedArticleList;
        }

        //↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑以上2项不能这样缓存,或者翻页时动态获取(首页例外)↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑
        /// <summary>
        /// 新闻类别缓存
        /// </summary>
        /// <returns></returns>
        public List<ArticleType> CachedArticleType()
        {
            var _CachedArticleType = _CacheHelper.GetItem<List<ArticleType>>(CKey_CZZ_ArticleType);
            if (_CachedArticleType == null || _CachedArticleType.Count <= 0)
            {
                _CachedArticleType = _Context.ArticleTypes.Where(m => m.a_t_Flag == (int)YZ.Biz.DataBaseEnum.EN_ArticleTypeUsed.Enabled).OrderBy(m => m.id).ToList();
                _CacheHelper.addItem(CKey_CZZ_ArticleType, _CachedArticleType, CacheHelper.Expiration.FiveMin);
            }

            return _CachedArticleType;
        }

        #endregion

        #endregion
    }

}
