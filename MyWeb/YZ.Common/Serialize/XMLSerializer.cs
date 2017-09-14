namespace YZ.Common.Serialize
{
    using System;
    using System.Collections;
    using System.IO;
    using System.Text;
    using System.Xml.Serialization;

    /// <summary>
    /// XML序列化、反序列化工具类
    /// </summary>
    public class XMLSerializer : ISerializer
    {
        public string EncodName { get; set; }
        /// <summary>
        /// 序列化器缓存
        /// </summary>
        private static Hashtable serializers = Hashtable.Synchronized(new Hashtable());

        /// <summary>
        /// 一个空的命名空间
        /// </summary>
        private static XmlSerializerNamespaces xmlNs = GetXmlNameSpace();

        /// <summary>
        /// 从字符串中反序列化一个T类型对象
        /// </summary>
        /// <typeparam name="T">序列化、反序列化对象的类型</typeparam>
        /// <param name="source">T类型的格式化字符串</param>
        /// <returns>T类型对象</returns>
        public T Deserialize<T>(string source)
            where T : class
        {
            MemoryStream stream = new MemoryStream(Encoding.GetEncoding(EncodName).GetBytes(source));
            return Deserialize<T>(stream);
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
            XmlSerializer serializer = GetXmlSerializer(typeof(T));
            T objRet = (T)serializer.Deserialize(source);
            return objRet;
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
        public T DeserializeFromFile<T>(string fileName)
            where T : class
        {
            using (TextReader tr = new StreamReader(fileName, Encoding.UTF8))
            {
                XmlSerializer serializer = GetXmlSerializer(typeof(T));
                T objRet = (T)serializer.Deserialize(tr);
                return objRet;
            }
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
            string retSerialXml = "";
            MemoryStream stream = new MemoryStream();
            XmlSerializer serializer = GetXmlSerializer(typeof(T));
            serializer.Serialize(stream, t, xmlNs);
            retSerialXml = Encoding.GetEncoding(EncodName).GetString(stream.GetBuffer());
            return retSerialXml.Trim('\0');
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
            XmlSerializer serializer = GetXmlSerializer(typeof(T));
            serializer.Serialize(destination, t, xmlNs);
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
            MemoryStream stream = new MemoryStream();
            XmlSerializer serializer = GetXmlSerializer(typeof(T));
            serializer.Serialize(stream, t, xmlNs);
            buf = stream.GetBuffer();
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
            using (TextWriter writer = new StreamWriter(fileName))
            {
                XmlSerializer serializer = GetXmlSerializer(typeof(T));
                serializer.Serialize(writer, t, xmlNs);
            }
        }

        /// <summary>
        /// 从缓存中获取一个XML序列化对象,如果没有,则创建,并添加到缓存中
        /// </summary>
        /// <returns>XML序列化对象</returns>
        private XmlSerializer GetXmlSerializer(Type objType)
        {
            if (objType == null)
                return null;
            if (serializers.ContainsKey(objType.FullName))
                return (XmlSerializer)serializers[objType.FullName];
            else
            {
                if (serializers.Count >= 100)
                    serializers.Clear();
                XmlSerializer serializer = new XmlSerializer(objType);
                serializers.Add(objType.FullName, serializer);
                return serializer;
            }
        }

        /// <summary>
        /// 获取一个空的命名空间
        /// </summary>
        /// <returns>空命名空间</returns>
        private static XmlSerializerNamespaces GetXmlNameSpace()
        {
            XmlSerializerNamespaces ns = new XmlSerializerNamespaces();
            ns.Add("", "");
            return ns;
        }
    }
}
