using System;
using System.Runtime.Caching;
using System.Web;
using System.Configuration;

namespace YZ.Common
{
    public class CacheHelper
    {
        private static ObjectCache CurrentCache = MemoryCache.Default;

        private CacheItemPolicy pilicy = null;

        #region Instance
        private static CacheHelper _instance;
        private static object _syncObject = new object();
        public static CacheHelper Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (_syncObject)
                    {
                        if (_instance == null)
                        {
                            _instance = new CacheHelper();
                        }
                    }
                }
                return _instance;
            }
        }
        #endregion

        public void addItem(string cacheKey, object cacheValue, Expiration exp = Expiration.FiveMin)
        {
            pilicy = new CacheItemPolicy();

            switch (exp)
            {
                case Expiration.OneDay:
                    pilicy.AbsoluteExpiration = DateTimeOffset.Now.AddDays(1);
                    break;
                case Expiration.TwentyMinites:
                    pilicy.AbsoluteExpiration = DateTimeOffset.Now.AddMinutes(20);
                    break;
                case Expiration.FiveMin:
                    pilicy.AbsoluteExpiration = DateTimeOffset.Now.AddMinutes(5);
                    break;
                case Expiration.TwoMin:
                    pilicy.AbsoluteExpiration = DateTimeOffset.Now.AddMinutes(2);
                    break;
                case Expiration.OneMin:
                    pilicy.AbsoluteExpiration = DateTimeOffset.Now.AddMinutes(1);
                    break;
                default:
                    pilicy.AbsoluteExpiration = DateTimeOffset.Now.AddMinutes(5);
                    break;
            }
            CurrentCache.Set(cacheKey, cacheValue, pilicy);
        }

        public void RemoveItem(string cacheKey)
        {
            if (CurrentCache.Contains(cacheKey))
            {
                CurrentCache.Remove(cacheKey);
            }
        }

        private object GetItem(string cacheKey)
        {
            return CurrentCache[cacheKey] as object;
        }

        public T GetItem<T>(string cacheKey)
        {
            object obj = GetItem(cacheKey);
            return obj == null ? default(T) : (T)obj;
        }
        /// <summary>
        /// 缓存时长
        /// </summary>
        public enum Expiration
        {
            TwentyMinites,

            OneDay,

            TwoMin,

            FiveMin,

            OneMin,

            FiveSecond,//5秒
        }

        #region 缓存依赖项
        //缓存依赖项使缓存依赖于其他资源，当依赖项更改时，缓存条目项将自动从缓存中移除。缓存依赖项可以是应用程序的 Cache 中的文件、目录或与其他对象的键。如果文件或目录更改，缓存就会过期。
        //string str = "";
        //   if (Cache["key"] == null)
        //   {
        //       str = System.IO.File.ReadAllText(Server.MapPath("TextFile1.txt")); //读取TextFile1.txt文件中的数据
        //       CacheDependency dp = new CacheDependency(Server.MapPath("TextFile1.txt"));//建立缓存依赖项dp
        //       Cache.Insert("key", str, dp);
        //       Response.Write(Cache["key"]);   //如果TextFile1.txt这个文件的内容不变就一直读取缓存中的数据，一旦TextFile1.txt文件中的数据改变里面重新读取TextFile1.txt文件中的数据
        //   }
        //   else
        //   {
        //       Response.Write(Cache["key"]);
        //   }

        //public void CacheItemRemovedCallback(string key, object value, CacheItemRemovedReason reason) //这个为缓存移除时的回调函数，一定要保持与 Cache.Insert（）方法中的最后一个参数名字一致，
        ////这里使用了委托，你可以在 Cache.Insert（）这个函数中转定义看到的，所以这里的格式就只能按我写的这种方法签名写。
        //{
        //    System.IO.File.WriteAllText(Server.MapPath("log.txt"), "缓存移除原因为:" + reason.ToString());
        //}
        #endregion
    }
}
