using System;

namespace YZ.Common.Serialize
{
    /// <summary>
    /// Base64序列化、反序列化对象
    /// </summary>
    public class Base64Serializer : BinSerializer, ISerializer
    {
        //public string EncodName { get; set; }
        /// <summary>
        /// 从Base64编码字符串中反序列化一个T类型对象
        /// </summary>
        /// <typeparam name="T">序列化、反序列化对象的类型</typeparam>
        /// <param name="source">T类型的格式化字符串</param>
        /// <returns>T类型对象</returns>
        public override T Deserialize<T>(string source)
        {
            if (string.IsNullOrEmpty(source))
                return default(T);

            byte[] buf = Convert.FromBase64String(source);
            return Deserialize<T>(buf);
        }

        /// <summary>
        /// 将T类序列化为一个Base64编码字符串
        /// </summary>
        /// <typeparam name="T">序列化、反序列化对象的类型</typeparam>
        /// <param name="t">T类型对象</param>
        /// <returns>T类型的格式化字符串</returns>
        public override string Serialize<T>(T t)
        {
            if (t == null)
                return string.Empty;

            byte[] buf = null;
            Serialize(t, ref buf);
            string base64String = Convert.ToBase64String(buf);
            return base64String;
        }
    }
}
