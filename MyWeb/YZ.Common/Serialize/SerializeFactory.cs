namespace YZ.Common.Serialize 
{
    using System;
    using System.Configuration;
    using System.Reflection;

    /// <summary>
    /// 序列化对象工厂,根据用户输入或配置项创建序列化对象
    /// </summary>
    public static class SerializeFactory
    {
        /// <summary>
        /// 根据AppSettings.config中Serializer_Mode配置项创建序列化对象.不指定该项时,或指定方式不存在时,默认使用二进制序列化方式.
        /// </summary>
        /// <returns>序列化对象</returns>
        public static ISerializer GetSerializer()
        {
            string mode = ConfigurationManager.AppSettings["Serializer_Mode"];
            if (string.IsNullOrEmpty(mode))
                return new BinSerializer();
            try
            {
                mode = mode.Trim();
                string[] nl = mode.Split(';');
                if (nl.Length >= 2)
                {
                    //指定序列化对象类型nl[0]和所在程序集nl[1]
                    Assembly asb = Assembly.LoadFrom(nl[1]);
                    if (asb == null)
                        return new BinSerializer();
                    Type tp = asb.GetType(nl[0]);
                    if (tp == null)
                        return new BinSerializer();
                    ConstructorInfo ctorInfo = tp.GetConstructor(null);
                    if (ctorInfo == null)
                        return new BinSerializer();
                    ISerializer sizer = ctorInfo.Invoke(null) as ISerializer;
                    if (sizer == null)
                        return new BinSerializer();
                    else
                        return sizer;
                }
                else
                {
                    //指定默认的序列化方式,如bin,base64,namevalue,soap,xml等
                    return GetSerializer(mode);
                }
            }
            catch
            {
                return new BinSerializer();
            }
        }

        /// <summary>
        /// 根据用户指定的方式构造序列化对象
        /// </summary>
        /// <param name="mode">序列化方式</param>
        /// <returns>序列化对象</returns>
        public static ISerializer GetSerializer(string mode)
        {
            if (mode == null)
            {
                mode = "bin";
            }
            mode = mode.Trim().ToLower();
            if (string.IsNullOrEmpty(mode))
            {
                mode = "bin";
            }
            switch (mode)
            {
                case "bin":
                    return new BinSerializer();
                case "base64":
                    return new Base64Serializer();
                case "namevalue":
                    return new NameValueSerializer();
                case "xml":
                    return new XMLSerializer();
            }
            return new BinSerializer();
        }

        /// <summary>
        /// 根据用户指定的方式构造序列化对象
        /// </summary>
        /// <param name="mode">序列化方式</param>
        /// <returns>序列化对象</returns>
        public static ISerializer GetSerializer(SerializeMethod mode)
        {
            ISerializer Serializer;
            switch (mode)
            {
                case SerializeMethod.Bin:
                    Serializer = new BinSerializer();
                    break;
                case SerializeMethod.Base64:
                    Serializer = new Base64Serializer();
                    break;
                case SerializeMethod.NameValue:
                    Serializer = new NameValueSerializer();
                    break;
                case SerializeMethod.Xml:
                    Serializer = new XMLSerializer();
                    break;
                default:
                    Serializer = new BinSerializer();
                    break;

            }
            ///默认设置编码为utf-8
            Serializer.EncodName = "utf-8";

            return Serializer;
        }
    }
}