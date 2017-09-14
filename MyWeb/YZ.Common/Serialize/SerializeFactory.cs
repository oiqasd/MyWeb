namespace YZ.Common.Serialize 
{
    using System;
    using System.Configuration;
    using System.Reflection;

    /// <summary>
    /// ���л����󹤳�,�����û����������������л�����
    /// </summary>
    public static class SerializeFactory
    {
        /// <summary>
        /// ����AppSettings.config��Serializer_Mode����������л�����.��ָ������ʱ,��ָ����ʽ������ʱ,Ĭ��ʹ�ö��������л���ʽ.
        /// </summary>
        /// <returns>���л�����</returns>
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
                    //ָ�����л���������nl[0]�����ڳ���nl[1]
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
                    //ָ��Ĭ�ϵ����л���ʽ,��bin,base64,namevalue,soap,xml��
                    return GetSerializer(mode);
                }
            }
            catch
            {
                return new BinSerializer();
            }
        }

        /// <summary>
        /// �����û�ָ���ķ�ʽ�������л�����
        /// </summary>
        /// <param name="mode">���л���ʽ</param>
        /// <returns>���л�����</returns>
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
        /// �����û�ָ���ķ�ʽ�������л�����
        /// </summary>
        /// <param name="mode">���л���ʽ</param>
        /// <returns>���л�����</returns>
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
            ///Ĭ�����ñ���Ϊutf-8
            Serializer.EncodName = "utf-8";

            return Serializer;
        }
    }
}