namespace YZ.Common.Serialize
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// 序列化方式
    /// </summary>
    public enum SerializeMethod
    {
        /// <summary>
        /// 二进制序列化
        /// </summary>
        Bin,
        /// <summary>
        /// Base64序列化
        /// </summary>
        Base64,
        /// <summary>
        /// NameValue序列化
        /// </summary>
        NameValue,
        /// <summary>
        /// XML序列化
        /// </summary>
        Xml
    }
}
