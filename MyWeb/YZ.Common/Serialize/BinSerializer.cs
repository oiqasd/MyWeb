namespace YZ.Common.Serialize
{
    using System;
    using System.Collections;
    using System.IO;
    using System.Runtime.Serialization;
    using System.Runtime.Serialization.Formatters.Binary;
    using System.Text;

    /// <summary>
    /// 二进制序列化、反序列化工具类
    /// </summary>
    public class BinSerializer : ISerializer
    {
        public string EncodName { get; set; }
        /// <summary>
        /// 序列化器缓存
        /// </summary>
        private static Hashtable serializers = Hashtable.Synchronized(new Hashtable());

        /// <summary>
        /// 从字符串中反序列化一个T类型对象
        /// </summary>
        /// <typeparam name="T">序列化、反序列化对象的类型</typeparam>
        /// <param name="source">T类型的格式化字符串</param>
        /// <returns>T类型对象</returns>
        public virtual T Deserialize<T>(string source)
            where T : class
        {
            if (string.IsNullOrEmpty(source))
                return default(T);

            byte[] buf = Encoding.UTF8.GetBytes(source);
            return Deserialize<T>(buf);
        }

        /// <summary>
        /// 从当前流中反序列化一个T类型对象
        /// </summary>
        /// <typeparam name="T">序列化、反序列化对象的类型</typeparam>
        /// <param name="source">T类型的流</param>
        /// <returns>T类型对象</returns>
        public virtual T Deserialize<T>(System.IO.Stream source)
            where T : class
        {
            try
            {
                IFormatter formatter = GetSerializer(typeof(T));
                T t = (T)formatter.Deserialize(source);
                return t;
            }
            catch
            {
            }
            return default(T);
        }

        /// <summary>
        /// 从当前字节串中反序列化一个T类型对象
        /// </summary>
        /// <typeparam name="T">序列化、反序列化对象的类型</typeparam>
        /// <param name="source">T类型的字节串</param>
        /// <returns>T类型对象</returns>
        public virtual T Deserialize<T>(byte[] source)
            where T : class
        {
            if (source == null || source.Length == 0)
                return default(T);

            using (MemoryStream ms = new MemoryStream())
            {
                ms.Write(source, 0, source.Length);
                ms.Position = 0;
                return Deserialize<T>(ms);
            }
        }

        /// <summary>
        /// 从文件中反序列化一个T类型对象
        /// </summary>
        /// <typeparam name="T">序列化、反序列化对象的类型</typeparam>
        /// <param name="fileName">T类型的格式化文件</param>
        /// <returns>T类型对象</returns>
        public virtual T DeserializeFromFile<T>(string fileName)
            where T : class
        {
            if (!File.Exists(fileName))
                return null;
            T t = null;
            using (FileStream fs = new FileStream(fileName, FileMode.Open))
            {
                IFormatter formatter = GetSerializer(typeof(T));
                t = (T)formatter.Deserialize(fs);
            }
            return t;
        }

        /// <summary>
        /// 将T类序列化为一个字符串
        /// </summary>
        /// <typeparam name="T">序列化、反序列化对象的类型</typeparam>
        /// <param name="t">T类型对象</param>
        /// <returns>T类型的格式化字符串</returns>
        public virtual string Serialize<T>(T t)
            where T : class
        {
            if (t == null)
                return string.Empty;

            byte[] buf = null;
            Serialize(t, ref buf);
            string str = Encoding.UTF8.GetString(buf);
            return str.TrimEnd('\0');
        }

        /// <summary>
        /// 将T类序列化到一个流中
        /// </summary>
        /// <typeparam name="T">序列化、反序列化对象的类型</typeparam>
        /// <param name="t">T类型对象</param>
        /// <param name="destination">流</param>
        public virtual void Serialize<T>(T t, System.IO.Stream destination)
            where T : class
        {
            IFormatter formatter = GetSerializer(typeof(T));
            formatter.Serialize(destination, t);
        }

        /// <summary>
        /// 将T类序列化到一个字节序列中
        /// </summary>
        /// <typeparam name="T">序列化、反序列化对象的类型</typeparam>
        /// <param name="t">T类型对象</param>
        /// <param name="buf">字节序列</param>
        public virtual void Serialize<T>(T t, ref byte[] buf)
            where T : class
        {
            if (t == null)
                return;

            using (MemoryStream ms = new MemoryStream())
            {
                Serialize(t, ms);
                buf = ms.ToArray();
            }
        }

        /// <summary>
        /// 将T类序列化到一个文件中
        /// </summary>
        /// <typeparam name="T">序列化、反序列化对象的类型</typeparam>
        /// <param name="t">T类型对象</param>
        /// <param name="fileName">输出的文件名</param>
        public virtual void Serialize<T>(T t, string fileName)
            where T : class
        {
            using (FileStream fs = new FileStream(fileName, FileMode.Create))
            {
                IFormatter formatter = GetSerializer(typeof(T));
                formatter.Serialize(fs, t);
            }
        }

        /// <summary>
        /// 从缓存中获取序列化对象,如果没有,则创建
        /// </summary>
        /// <returns>序列化对象</returns>
        protected IFormatter GetSerializer(Type objType)
        {
            if (serializers.ContainsKey(objType.FullName))
                return (IFormatter)serializers[objType.FullName];
            else
            {
                if (serializers.Count >= 100)
                    serializers.Clear();
                IFormatter serializer = GetFormatter();
                serializers.Add(objType.FullName, serializer);
                return serializer;
            }
        }

        /// <summary>
        /// 创建二进制序列化对象
        /// </summary>
        /// <returns>序列化对象</returns>
        protected virtual IFormatter GetFormatter()
        {
            return new BinaryFormatter();
        }
    }
}