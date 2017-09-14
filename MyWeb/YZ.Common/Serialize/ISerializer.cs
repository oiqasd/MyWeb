namespace YZ.Common.Serialize
{
    using System.IO;

    /// <summary>
    /// ͨ�����л��������л��ӿ�
    /// </summary>
    public interface ISerializer
    {
        string EncodName { get; set; }
        /// <summary>
        /// ���ַ����з����л�һ��T���Ͷ���
        /// </summary>
        /// <typeparam name="T">���л��������л����������</typeparam>
        /// <param name="source">T���͵ĸ�ʽ���ַ���</param>
        /// <returns>T���Ͷ���</returns>
        T Deserialize<T>(string source) where T : class;

        /// <summary>
        /// �ӵ�ǰ���з����л�һ��T���Ͷ���
        /// </summary>
        /// <typeparam name="T">���л��������л����������</typeparam>
        /// <param name="source">T���͵���</param>
        /// <returns>T���Ͷ���</returns>
        T Deserialize<T>(Stream source) where T : class;

        /// <summary>
        /// �ӵ�ǰ�ֽڴ��з����л�һ��T���Ͷ���
        /// </summary>
        /// <typeparam name="T">���л��������л����������</typeparam>
        /// <param name="source">T���͵��ֽڴ�</param>
        /// <returns>T���Ͷ���</returns>
        T Deserialize<T>(byte[] source) where T : class;

        /// <summary>
        /// ���ļ��з����л�һ��T���Ͷ���
        /// </summary>
        /// <typeparam name="T">���л��������л����������</typeparam>
        /// <param name="fileName">T���͵ĸ�ʽ���ļ�</param>
        /// <returns>T���Ͷ���</returns>
        T DeserializeFromFile<T>(string fileName) where T : class;

        /// <summary>
        /// ��T�����л�Ϊһ���ַ���
        /// </summary>
        /// <typeparam name="T">���л��������л����������</typeparam>
        /// <param name="t">T���Ͷ���</param>
        /// <returns>T���͵ĸ�ʽ���ַ���</returns>        
        string Serialize<T>(T t) where T : class;

        /// <summary>
        /// ��T�����л���һ������
        /// </summary>
        /// <typeparam name="T">���л��������л����������</typeparam>
        /// <param name="t">T���Ͷ���</param>
        /// <param name="destination">��</param>
        void Serialize<T>(T t, System.IO.Stream destination) where T : class;

        /// <summary>
        /// ��T�����л���һ���ֽ�������
        /// </summary>
        /// <typeparam name="T">���л��������л����������</typeparam>
        /// <param name="t">T���Ͷ���</param>
        /// <param name="buf">�ֽ�����</param>
        void Serialize<T>(T t, ref byte[] buf) where T : class;

        /// <summary>
        /// ��T�����л���һ���ļ���
        /// </summary>
        /// <typeparam name="T">���л��������л����������</typeparam>
        /// <param name="t">T���Ͷ���</param>
        /// <param name="fileName">������ļ���</param>
        void Serialize<T>(T t, string fileName) where T : class;
    }
}