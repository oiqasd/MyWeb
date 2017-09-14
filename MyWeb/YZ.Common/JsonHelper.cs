using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.IO;

namespace YZ.Common
{
    /// <summary>
    /// JSON序列化和反序列化辅助类
    /// </summary>
   public class JsonHelper
    {
        /// <summary>
        /// 将指定的对象序列化成 JSON 数据。
        /// </summary>
        /// <param name="obj">要序列化的对象。</param>
        /// <returns></returns>
       public static string Serialize(object obj)
       {
           try {
               if (obj == null) return null;
               else {
                   using (MemoryStream ms = new MemoryStream())
                   {
                       DataContractJsonSerializer serializer = new DataContractJsonSerializer(obj.GetType());
                       serializer.WriteObject(ms, obj);
                       ms.Position = 0;

                       using (StreamReader sr = new StreamReader(ms))
                       {
                           return sr.ReadToEnd();
                       }
                   }
               }
           }
           catch (Exception ex) {
               throw ex;
           }
       }

       /// <summary>
       /// 反序列化
       /// </summary>
       /// <typeparam name="T"></typeparam>
       /// <param name="jsonString"></param>
       /// <param name="encoding"></param>
       /// <returns></returns>
       public static T Deserialize<T>(string jsonString,string encoding="")
       {
           using (MemoryStream ms = new MemoryStream(Encoding.UTF8.GetBytes(jsonString)))
           {
               DataContractJsonSerializer serizlizer = new DataContractJsonSerializer(typeof(T));
               return (T)serizlizer.ReadObject(ms);
           }
       }
    }
}
