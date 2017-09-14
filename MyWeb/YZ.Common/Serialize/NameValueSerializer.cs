namespace YZ.Common.Serialize
{
    using System;
    using System.Collections;
    using System.Collections.Specialized;
    using System.Reflection;

    /// <summary>
    /// NameValue序列化、反序列化工具类
    /// </summary>
    public class NameValueSerializer : ISerializer
    {
        public string EncodName { get; set; }
        /// <summary>
        /// 属性集合缓存
        /// </summary>
        private static Hashtable hashTypes = Hashtable.Synchronized(new Hashtable());

        /// <summary>
        /// 二进制序列化反序列化对象
        /// </summary>
        private BinSerializer nvcSerializer = new BinSerializer();

        /// <summary>
        /// 从字符串中反序列化一个T类型对象
        /// </summary>
        /// <typeparam name="T">序列化、反序列化对象的类型</typeparam>
        /// <param name="source">T类型的格式化字符串</param>
        /// <returns>T类型对象</returns>
        public T Deserialize<T>(string source)
            where T : class
        {
            NameValueCollection nvc = nvcSerializer.Deserialize<NameValueCollection>(source);
            T t = FromNameValues<T>(nvc);
            return t;
        }

        /// <summary>
        /// 从当前流中反序列化一个T类型对象
        /// </summary>
        /// <typeparam name="T">序列化、反序列化对象的类型</typeparam>
        /// <param name="source">T类型的流</param>
        /// <returns>T类型对象</returns>
        public T Deserialize<T>(System.IO.Stream source)
            where T : class
        {
            NameValueCollection nvc = nvcSerializer.Deserialize<NameValueCollection>(source);
            T t = FromNameValues<T>(nvc);
            return t;
        }

        /// <summary>
        /// 从当前字节串中反序列化一个T类型对象
        /// </summary>
        /// <typeparam name="T">序列化、反序列化对象的类型</typeparam>
        /// <param name="source">T类型的字节串</param>
        /// <returns>T类型对象</returns>
        public T Deserialize<T>(byte[] source)
            where T : class
        {
            NameValueCollection nvc = nvcSerializer.Deserialize<NameValueCollection>(source);
            T t = FromNameValues<T>(nvc);
            return t;
        }

        /// <summary>
        /// 从文件中反序列化一个T类型对象
        /// </summary>
        /// <typeparam name="T">序列化、反序列化对象的类型</typeparam>
        /// <param name="fileName">T类型的格式化文件</param>
        /// <returns>T类型对象</returns>
        public T DeserializeFromFile<T>(string fileName)
            where T : class
        {
            NameValueCollection nvc = nvcSerializer.DeserializeFromFile<NameValueCollection>(fileName);
            T t = FromNameValues<T>(nvc);
            return t;
        }

        /// <summary>
        /// 将T类序列化为一个字符串
        /// </summary>
        /// <typeparam name="T">序列化、反序列化对象的类型</typeparam>
        /// <param name="t">T类型对象</param>
        /// <returns>T类型的格式化字符串</returns>
        public string Serialize<T>(T t)
            where T : class
        {
            NameValueCollection nvc = ToNameValues(t);
            return nvcSerializer.Serialize(nvc);
        }

        /// <summary>
        /// 将T类序列化到一个流中
        /// </summary>
        /// <typeparam name="T">序列化、反序列化对象的类型</typeparam>
        /// <param name="t">T类型对象</param>
        /// <param name="destination">流</param>
        public void Serialize<T>(T t, System.IO.Stream destination)
            where T : class
        {
            NameValueCollection nvc = ToNameValues(t);
            nvcSerializer.Serialize(nvc, destination);
        }

        /// <summary>
        /// 将T类序列化到一个字节序列中
        /// </summary>
        /// <typeparam name="T">序列化、反序列化对象的类型</typeparam>
        /// <param name="t">T类型对象</param>
        /// <param name="buf">字节序列</param>
        public void Serialize<T>(T t, ref byte[] buf)
            where T : class
        {
            NameValueCollection nvc = ToNameValues(t);
            nvcSerializer.Serialize(nvc, ref buf);
        }

        /// <summary>
        /// 将T类序列化到一个文件中
        /// </summary>
        /// <typeparam name="T">序列化、反序列化对象的类型</typeparam>
        /// <param name="t">T类型对象</param>
        /// <param name="fileName">输出的文件名</param>
        public void Serialize<T>(T t, string fileName)
            where T : class
        {
            NameValueCollection nvc = ToNameValues(t);
            nvcSerializer.Serialize(nvc, fileName);
        }

        /// <summary>
        /// 将平面实体对象转化成NameValueCollection对象
        /// </summary>
        /// <typeparam name="T">序列化、反序列化对象的类型</typeparam>
        /// <param name="t">实体对象</param>
        /// <returns>NameValueCollection对象 </returns>        
        private static NameValueCollection ToNameValues<T>(T t)
            where T : class
        {
            if (t == null)
                return null;
            PropertyInfo[] pis = GetProperties(typeof(T));
            NameValueCollection data = new NameValueCollection();
            foreach (PropertyInfo pi in pis)
                data.Add(pi.Name, pi.GetValue(t, null).ToString());
            return data;
        }

        /// <summary>
        /// 将NameValueCollection对象转换成平面实体对象  
        /// </summary>
        /// <typeparam name="T">序列化、反序列化对象的类型</typeparam>
        /// <param name="data">NameValueCollection对象</param>
        /// <returns>实体对象</returns>
        private static T FromNameValues<T>(NameValueCollection data)
            where T : class
        {
            if (data == null)
                return default(T);
            Type objType = typeof(T);
            ConstructorInfo cif = objType.GetConstructor(null);
            if (cif == null)
                throw new ArgumentException(string.Format("{0}必须有默认构造函数.", objType));
            T t = (T)cif.Invoke(null);
            PropertyInfo[] pis = GetProperties(typeof(T));
            string oneValue = string.Empty;
            foreach (PropertyInfo pi in pis)
            {
                oneValue = data[pi.Name];
                if (string.IsNullOrEmpty(oneValue))
                    continue;

                if (pi.PropertyType.IsEnum)
                    pi.SetValue(t, Enum.Parse(pi.PropertyType, oneValue, true), null);
                else if (pi.PropertyType.IsGenericType)
                    continue;
                else
                    pi.SetValue(t, Convert.ChangeType(oneValue, pi.PropertyType), null);
            }
            return t;
        }

        /// <summary>
        /// 从缓存中获取一个类型的属性集合，如果没有，则创建，并添加到缓存中，用于提高应用程序性能
        /// </summary>
        /// <returns>缓存集合</returns>
        private static PropertyInfo[] GetProperties(Type objType)
        {
            if (objType == null)
                return new PropertyInfo[0];
            PropertyInfo[] pis = null;
            if (hashTypes.ContainsKey(objType.FullName))
                pis = (PropertyInfo[])hashTypes[objType];
            else
            {
                if (hashTypes.Count >= 100)
                    hashTypes.Clear();
                pis = objType.GetProperties();
                hashTypes.Add(objType.FullName, pis);
            }
            return pis;
        }
    }
}
