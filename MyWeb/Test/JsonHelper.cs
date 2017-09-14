using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test
{

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public static class JsonHelper<T>
    {
        /// <summary>
        /// 把基于json格式的字符串序列化成对象
        /// </summary>
        /// <param name="jsonText">基于json格式的字符串</param>
        /// <returns>对象</returns>
        public static T ConvertToObject(string jsonText)
        {
            JsonTextReader reader = new JsonTextReader(new StringReader(jsonText));
            JsonSerializer json = new JsonSerializer();
            T t = (T)json.Deserialize(reader, typeof(T));
            return t;
        }

        /// <summary>
        /// 把对象转成基于json格式的字符串
        /// </summary>
        /// <param name="t">对象</param>
        /// <returns>基于json格式的字符串</returns>
        public static string ConvertToJsonText(T t)
        {
            return JsonConvert.SerializeObject(t);
        }
    }
}
