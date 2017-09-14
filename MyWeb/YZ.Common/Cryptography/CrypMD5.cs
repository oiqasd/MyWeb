using System;
using System.Security.Cryptography;
using System.Text;

namespace YZ.Common.Cryptography
{
    public class CrypMD5
    {
        //���������ַ���������32
        //����������ݿ���Salt�ֶγ���32
        private static string GetSalt()
        {
            Random rnd = new Random();
            Byte[] b = new Byte[32];
            rnd.NextBytes(b);
            return MD5ToHexString(b);
        }

        /// <summary>
        /// ��������
        /// </summary>
        /// <param name="strPassword">�û����������,���ܿ�</param>
        /// <param name="salt">saltֵ</param>
        /// <returns>����MD5���ܺ������</returns>
        /// <remarks>
        /// ������Ҫ�����˴�saltֵ��ʲô��ʽʲô�����������
        /// </remarks>
        public static string Encrypt(string strPassword, string salt)
        {
            if (strPassword == null) strPassword = "";
            if (salt == null) salt = "";

            return MD5ToHexString(strPassword + salt);
        }

        /// <summary>
        /// ����
        /// </summary>
        /// <param name="strPassword"></param>
        /// <returns></returns>
        public static string Encrypt(string strPassword)
        {
            return Encrypt(strPassword, null);
        }

        /// <summary>
        /// MD5 ���ܣ�byte[]��
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static byte[] MD5_Byte(byte[] data)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            return md5.ComputeHash(data);
        }

        /// <summary>
        /// MD5 ���ܣ�byte[]�ͼ���Ϊstring
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static string MD5ToHexString(byte[] data)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] result = md5.ComputeHash(data);
            string t = "";
            string tTemp = "";
            for (int i = 0; i < result.Length; i++)
            {
                tTemp = Convert.ToString(result[i], 16);
                if (tTemp.Length != 2)
                {
                    switch (tTemp.Length)
                    {
                        case 0: tTemp = "00"; break;
                        case 1: tTemp = "0" + tTemp; break;
                        default: tTemp = tTemp.Substring(0, 2); break;
                    }
                }
                t += tTemp;
            }
            return t;
        }

        /// <summary>
        /// ����ʵ��
        /// </summary>
        /// <param name="strText"></param>
        /// <returns></returns>
        private static string MD5ToHexString(string strText)
        {
            byte[] data = System.Text.ASCIIEncoding.Unicode.GetBytes(strText);
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] result = md5.ComputeHash(data);
            string t = "";
            for (int i = 0; i < result.Length; i++)
            {
                t += Convert.ToString(result[i], 16).PadLeft(2, '0');
            }
            return t;
        }

        /// <summary>
        /// MD5���ܣ�utf8��
        /// update 2017.1.17
        /// </summary>
        /// <param name="str"></param>
        /// <param name="L32">true:32λ���ȣ�false:16λ����</param>
        /// <returns></returns>
        public static string MD5(string str, bool L32 = true)
        {
            byte[] b = Encoding.UTF8.GetBytes(str);
            b = new MD5CryptoServiceProvider().ComputeHash(b);
            StringBuilder ret = new StringBuilder();
            if (L32)
                for (int i = 0; i < b.Length; i++)
                {
                    ret.Append(Convert.ToString(b[i], 16).PadLeft(2, '0'));
                    //ret += b[i].ToString("x2");
                    // ret += b[i].ToString("x").PadLeft(2, '0');
                }
            else
                return BitConverter.ToString(b, 4, 8).Replace("-", "").ToLower();
            return ret.ToString();
        }
      
        /// <summary>
        /// ���ַ�������󾭹�md5���ܣ����ؼ��ܺ���ַ����Ĵ�д��ʾ
        /// </summary>
        /// <param name="str"></param>
        /// <param name="encodingName"></param>
        /// <returns></returns>
        public static string MD5_Enode(string str, string encodingName = "utf-8")
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            Byte[] FromData = System.Text.Encoding.GetEncoding(encodingName).GetBytes(str);
            Byte[] TargetData = md5.ComputeHash(FromData);
            string Byte2String = "";
            for (int i = 0; i < TargetData.Length; i++)
            {
                Byte2String += TargetData[i].ToString("X2");
            }
            return Byte2String;
        }
    }
}
