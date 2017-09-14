namespace YZ.Common.Serialize
{
    using System.IO;

    /// <summary>
    /// 通用序列化、反序列化接口
    /// </summary>
    public interface ISerializer
    {
        string EncodName { get; set; }
        /// <summary>
        /// 从字符串中反序列化一个T类型对象
        /// </summary>
        /// <typeparam name="T">序列化、反序列化对象的类型</typeparam>
        /// <param name="source">T类型的格式化字符串</param>
        /// <returns>T类型对象</returns>
        T Deserialize<T>(string source) where T : class;

        /// <summary>
        /// 从当前流中反序列化一个T类型对象
        /// </summary>
        /// <typeparam name="T">序列化、反序列化对象的类型</typeparam>
        /// <param name="source">T类型的流</param>
        /// <returns>T类型对象</returns>
        T Deserialize<T>(Stream source) where T : class;

        /// <summary>
        /// 从当前字节串中反序列化一个T类型对象
        /// </summary>
        /// <typeparam name="T">序列化、反序列化对象的类型</typeparam>
        /// <param name="source">T类型的字节串</param>
        /// <returns>T类型对象</returns>
        T Deserialize<T>(byte[] source) where T : class;

        /// <summary>
        /// 从文件中反序列化一个T类型对象
        /// </summary>
        /// <typeparam name="T">序列化、反序列化对象的类型</typeparam>
        /// <param name="fileName">T类型的格式化文件</param>
        /// <returns>T类型对象</returns>
        T DeserializeFromFile<T>(string fileName) where T : class;

        /// <summary>
        /// 将T类序列化为一个字符串
        /// </summary>
        /// <typeparam name="T">序列化、反序列化对象的类型</typeparam>
        /// <param name="t">T类型对象</param>
        /// <returns>T类型的格式化字符串</returns>        
        string Serialize<T>(T t) where T : class;

        /// <summary>
        /// 将T类序列化到一个流中
        /// </summary>
        /// <typeparam name="T">序列化、反序列化对象的类型</typeparam>
        /// <param name="t">T类型对象</param>
        /// <param name="destination">流</param>
        void Serialize<T>(T t, System.IO.Stream destination) where T : class;

        /// <summary>
        /// 将T类序列化到一个字节序列中
        /// </summary>
        /// <typeparam name="T">序列化、反序列化对象的类型</typeparam>
        /// <param name="t">T类型对象</param>
        /// <param name="buf">字节序列</param>
        void Serialize<T>(T t, ref byte[] buf) where T : class;

        /// <summary>
        /// 将T类序列化到一个文件中
        /// </summary>
        /// <typeparam name="T">序列化、反序列化对象的类型</typeparam>
        /// <param name="t">T类型对象</param>
        /// <param name="fileName">输出的文件名</param>
        void Serialize<T>(T t, string fileName) where T : class;
    }
}