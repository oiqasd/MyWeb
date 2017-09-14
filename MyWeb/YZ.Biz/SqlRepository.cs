using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YZ.Biz.Entity;

namespace YZ.Biz
{
    public class SqlRepository : BaseRepository
    {
        #region Instance
        private static SqlRepository _instance;
        private static object _syncObject = new object();
        public static SqlRepository Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (_syncObject)
                    {
                        if (_instance == null)
                        {
                            _instance = new SqlRepository();
                        }
                    }
                }
                return _instance;
            }
        }
        #endregion

        #region Linq
        //PageList<Article> list = null; 
        //list = (from da in _Context.Articles
        //        join dt in _Context.ArticleTypes on da.a_TypeId equals dt.id
        //        where dt.id == typeid
        //        select da).ToPageList(index, size);//select new { da.id, da.a_Title }).ToPageList(index,size);
        //return list;
        #endregion 

        /// <summary>
        /// 上一篇
        /// </summary>
        /// <param name="aid"></param>
        /// <returns></returns>
        public ExArticle GetPrevArticle(long aid)
        {
            string strSql = " select top 1 id,a_title from article where id<{0} order by id desc ;";
            //strSql += " select top 1 id nid,a_Title na_Title from Articles where id>{0};";
            return RunSql<ExArticle>(string.Format(strSql, aid));
        }

        /// <summary>
        /// 下一篇
        /// </summary>
        /// <param name="aid"></param>
        /// <returns></returns>
        public ExArticle GetNextArticle(long aid)
        {
            string strSql = " select top 1 id,a_Title from Article where id>{0};"; 
            return RunSql<ExArticle>(string.Format(strSql, aid));
        }
    }
}
