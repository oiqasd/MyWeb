namespace YZ.Common.Serialize
{
    using System;
    using System.Collections;
    using System.IO;
    using System.Text;
    using System.Xml.Serialization;

    /// <summary>
    /// XML���л��������л�������
    /// </summary>
    public class XMLSerializer : ISerializer
    {
        public string EncodName { get; set; }
        /// <summary>
        /// ���л�������
        /// </summary>
        private static Hashtable serializers = Hashtable.Synchronized(new Hashtable());

        /// <summary>
        /// һ���յ������ռ�
        /// </summary>
        private static XmlSerializerNamespaces xmlNs = GetXmlNameSpace();

        /// <summary>
        /// ���ַ����з����л�һ��T���Ͷ���
        /// </summary>
        /// <typeparam name="T">���л��������л����������</typeparam>
        /// <param name="source">T���͵ĸ�ʽ���ַ���</param>
        /// <returns>T���Ͷ���</returns>
        public T Deserialize<T>(string source)
            where T : class
        {
            MemoryStream stream = new MemoryStream(Encoding.GetEncoding(EncodName).GetBytes(source));
            return Deserialize<T>(stream);
        }

        /// <summary>
        /// �ӵ�ǰ���з����л�һ��T���Ͷ���
        /// </summary>
        /// <typeparam name="T">���л��������л����������</typeparam>
        /// <param name="source">T���͵���</param>
        /// <returns>T���Ͷ���</returns>
        public T Deserialize<T>(System.IO.Stream source)
            where T : class
        {
            XmlSerializer serializer = GetXmlSerializer(typeof(T));
            T objRet = (T)serializer.Deserialize(source);
            return objRet;
        }

        /// <summary>
        /// �ӵ�ǰ�ֽڴ��з����л�һ��T���Ͷ���
        /// </summary>
        /// <typeparam name="T">���л��������л����������</typeparam>
        /// <param name="source">T���͵��ֽڴ�</param>
        /// <returns>T���Ͷ���</returns>
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
        /// ���ļ��з����л�һ��T���Ͷ���
        /// </summary>
        /// <typeparam name="T">���л��������л����������</typeparam>
        /// <param name="fileName">T���͵ĸ�ʽ���ļ�</param>
        /// <returns>T���Ͷ���</returns>
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
        /// ��T�����л�Ϊһ���ַ���
        /// </summary>
        /// <typeparam name="T">���л��������л����������</typeparam>
        /// <param name="t">T���Ͷ���</param>
        /// <returns>T���͵ĸ�ʽ���ַ���</returns>
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
        /// ��T�����л���һ������
        /// </summary>
        /// <typeparam name="T">���л��������л����������</typeparam>
        /// <param name="t">T���Ͷ���</param>
        /// <param name="destination">��</param>
        public void Serialize<T>(T t, System.IO.Stream destination)
            where T : class
        {
            XmlSerializer serializer = GetXmlSerializer(typeof(T));
            serializer.Serialize(destination, t, xmlNs);
        }

        /// <summary>
        /// ��T�����л���һ���ֽ�������
        /// </summary>
        /// <typeparam name="T">���л��������л����������</typeparam>
        /// <param name="t">T���Ͷ���</param>
        /// <param name="buf">�ֽ�����</param>
        public void Serialize<T>(T t, ref byte[] buf)
            where T : class
        {
            MemoryStream stream = new MemoryStream();
            XmlSerializer serializer = GetXmlSerializer(typeof(T));
            serializer.Serialize(stream, t, xmlNs);
            buf = stream.GetBuffer();
        }

        /// <summary>
        /// ��T�����л���һ���ļ���
        /// </summary>
        /// <typeparam name="T">���л��������л����������</typeparam>
        /// <param name="t">T���Ͷ���</param>
        /// <param name="fileName">������ļ���</param>
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
        /// �ӻ����л�ȡһ��XML���л�����,���û��,�򴴽�,����ӵ�������
        /// </summary>
        /// <returns>XML���л�����</returns>
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
        /// ��ȡһ���յ������ռ�
        /// </summary>
        /// <returns>�������ռ�</returns>
        private static XmlSerializerNamespaces GetXmlNameSpace()
        {
            XmlSerializerNamespaces ns = new XmlSerializerNamespaces();
            ns.Add("", "");
            return ns;
        }
    }
}
