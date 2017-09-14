namespace YZ.Common.Serialize
{
    using System;
    using System.Collections;
    using System.IO;
    using System.Runtime.Serialization;
    using System.Runtime.Serialization.Formatters.Binary;
    using System.Text;

    /// <summary>
    /// ���������л��������л�������
    /// </summary>
    public class BinSerializer : ISerializer
    {
        public string EncodName { get; set; }
        /// <summary>
        /// ���л�������
        /// </summary>
        private static Hashtable serializers = Hashtable.Synchronized(new Hashtable());

        /// <summary>
        /// ���ַ����з����л�һ��T���Ͷ���
        /// </summary>
        /// <typeparam name="T">���л��������л����������</typeparam>
        /// <param name="source">T���͵ĸ�ʽ���ַ���</param>
        /// <returns>T���Ͷ���</returns>
        public virtual T Deserialize<T>(string source)
            where T : class
        {
            if (string.IsNullOrEmpty(source))
                return default(T);

            byte[] buf = Encoding.UTF8.GetBytes(source);
            return Deserialize<T>(buf);
        }

        /// <summary>
        /// �ӵ�ǰ���з����л�һ��T���Ͷ���
        /// </summary>
        /// <typeparam name="T">���л��������л����������</typeparam>
        /// <param name="source">T���͵���</param>
        /// <returns>T���Ͷ���</returns>
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
        /// �ӵ�ǰ�ֽڴ��з����л�һ��T���Ͷ���
        /// </summary>
        /// <typeparam name="T">���л��������л����������</typeparam>
        /// <param name="source">T���͵��ֽڴ�</param>
        /// <returns>T���Ͷ���</returns>
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
        /// ���ļ��з����л�һ��T���Ͷ���
        /// </summary>
        /// <typeparam name="T">���л��������л����������</typeparam>
        /// <param name="fileName">T���͵ĸ�ʽ���ļ�</param>
        /// <returns>T���Ͷ���</returns>
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
        /// ��T�����л�Ϊһ���ַ���
        /// </summary>
        /// <typeparam name="T">���л��������л����������</typeparam>
        /// <param name="t">T���Ͷ���</param>
        /// <returns>T���͵ĸ�ʽ���ַ���</returns>
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
        /// ��T�����л���һ������
        /// </summary>
        /// <typeparam name="T">���л��������л����������</typeparam>
        /// <param name="t">T���Ͷ���</param>
        /// <param name="destination">��</param>
        public virtual void Serialize<T>(T t, System.IO.Stream destination)
            where T : class
        {
            IFormatter formatter = GetSerializer(typeof(T));
            formatter.Serialize(destination, t);
        }

        /// <summary>
        /// ��T�����л���һ���ֽ�������
        /// </summary>
        /// <typeparam name="T">���л��������л����������</typeparam>
        /// <param name="t">T���Ͷ���</param>
        /// <param name="buf">�ֽ�����</param>
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
        /// ��T�����л���һ���ļ���
        /// </summary>
        /// <typeparam name="T">���л��������л����������</typeparam>
        /// <param name="t">T���Ͷ���</param>
        /// <param name="fileName">������ļ���</param>
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
        /// �ӻ����л�ȡ���л�����,���û��,�򴴽�
        /// </summary>
        /// <returns>���л�����</returns>
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
        /// �������������л�����
        /// </summary>
        /// <returns>���л�����</returns>
        protected virtual IFormatter GetFormatter()
        {
            return new BinaryFormatter();
        }
    }
}