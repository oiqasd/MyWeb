namespace YZ.Common.Serialize
{
    using System;
    using System.Collections;
    using System.Collections.Specialized;
    using System.Reflection;

    /// <summary>
    /// NameValue���л��������л�������
    /// </summary>
    public class NameValueSerializer : ISerializer
    {
        public string EncodName { get; set; }
        /// <summary>
        /// ���Լ��ϻ���
        /// </summary>
        private static Hashtable hashTypes = Hashtable.Synchronized(new Hashtable());

        /// <summary>
        /// ���������л������л�����
        /// </summary>
        private BinSerializer nvcSerializer = new BinSerializer();

        /// <summary>
        /// ���ַ����з����л�һ��T���Ͷ���
        /// </summary>
        /// <typeparam name="T">���л��������л����������</typeparam>
        /// <param name="source">T���͵ĸ�ʽ���ַ���</param>
        /// <returns>T���Ͷ���</returns>
        public T Deserialize<T>(string source)
            where T : class
        {
            NameValueCollection nvc = nvcSerializer.Deserialize<NameValueCollection>(source);
            T t = FromNameValues<T>(nvc);
            return t;
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
            NameValueCollection nvc = nvcSerializer.Deserialize<NameValueCollection>(source);
            T t = FromNameValues<T>(nvc);
            return t;
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
            NameValueCollection nvc = nvcSerializer.Deserialize<NameValueCollection>(source);
            T t = FromNameValues<T>(nvc);
            return t;
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
            NameValueCollection nvc = nvcSerializer.DeserializeFromFile<NameValueCollection>(fileName);
            T t = FromNameValues<T>(nvc);
            return t;
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
            NameValueCollection nvc = ToNameValues(t);
            return nvcSerializer.Serialize(nvc);
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
            NameValueCollection nvc = ToNameValues(t);
            nvcSerializer.Serialize(nvc, destination);
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
            NameValueCollection nvc = ToNameValues(t);
            nvcSerializer.Serialize(nvc, ref buf);
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
            NameValueCollection nvc = ToNameValues(t);
            nvcSerializer.Serialize(nvc, fileName);
        }

        /// <summary>
        /// ��ƽ��ʵ�����ת����NameValueCollection����
        /// </summary>
        /// <typeparam name="T">���л��������л����������</typeparam>
        /// <param name="t">ʵ�����</param>
        /// <returns>NameValueCollection���� </returns>        
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
        /// ��NameValueCollection����ת����ƽ��ʵ�����  
        /// </summary>
        /// <typeparam name="T">���л��������л����������</typeparam>
        /// <param name="data">NameValueCollection����</param>
        /// <returns>ʵ�����</returns>
        private static T FromNameValues<T>(NameValueCollection data)
            where T : class
        {
            if (data == null)
                return default(T);
            Type objType = typeof(T);
            ConstructorInfo cif = objType.GetConstructor(null);
            if (cif == null)
                throw new ArgumentException(string.Format("{0}������Ĭ�Ϲ��캯��.", objType));
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
        /// �ӻ����л�ȡһ�����͵����Լ��ϣ����û�У��򴴽�������ӵ������У��������Ӧ�ó�������
        /// </summary>
        /// <returns>���漯��</returns>
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
