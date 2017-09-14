using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.Util.Expand
{
    public static class ExpandMethod
    {
        #region 时间格式转换
        public static string ToString(this DateTime? time , int option)
        {

            string dateStr = "";
            if (time==null || time == default(DateTime))
                return dateStr;
            DateTime dt = Convert.ToDateTime(time);
            switch (option)
            {
                case 1:
                    dateStr = dt.ToString("yyyy-MM-dd HH:mm:ss");
                    break;
                case 2:
                    dateStr = dt.ToString("yyyy-MM-dd");
                    break;
                case 3:
                    dateStr = dt.ToString("yyyy-MM-dd HH:mm:ss.ffff");
                    break;
                case 4:
                    dateStr = dt.ToString("yyyyMMddHHmmss");
                    break;
                case 5:
                    dateStr = dt.ToString("yyyyMMddHHmmssffff");
                    break;
                case 6:
                    dateStr = dt.ToString("HH:mm:ss");
                    break;
                default:
                    dateStr = dt.ToString("yyyy-MM-dd HH:mm:ss");
                    break;
            }
            return dateStr;
        }
        #endregion
    }
}