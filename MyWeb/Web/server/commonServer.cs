using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using YZ.Biz;

namespace Web.Server
{
    public class CommonServer
    {

        /// <summary>
        /// 统计功能
        /// </summary>
        /// <param name="id">传入ID</param>
        public static void Statistic(object id)
        {
            //使用方式
            //thred th = new thred(Statistic);
            //th.start(id);
            int sid = 0;
            if (int.TryParse(id.ToString(), out sid))
            {
                Stat_Article(sid);
            }

        }
        protected static void Stat_Article(int id)
        {
            ArticleRepository biz = new ArticleRepository();
            biz.SetStatistic(id);

        }
    }
}