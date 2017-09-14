using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace YZ.Common.Cryptography
{
    public class SymmetricMethod
    {
        private static SymmetricAlgorithm mobjCryptoService;
        private static string Key;
        /// <summary>   
        /// 对称加密类的构造函数   
        /// </summary>   
        public SymmetricMethod()
        {
            mobjCryptoService = new RijndaelManaged();
            Key = "Guz(%&hj7x89H$yuBI0456FtmaT5&fvHUFCy76*h%(HilJ$lhj!y6&(*jkP87jH7";
        }

        /// <summary>   
        /// 获得密钥   
        /// </summary>   
        /// <returns>密钥</returns>   
        private static byte[] GetLegalKey()
        {
            string sTemp = Key;
            mobjCryptoService.GenerateKey();
            byte[] bytTemp = mobjCryptoService.Key;
            int KeyLength = bytTemp.Length;
            if (sTemp.Length > KeyLength)
                sTemp = sTemp.Substring(0, KeyLength);
            else if (sTemp.Length < KeyLength)
                sTemp = sTemp.PadRight(KeyLength, ' ');
            return ASCIIEncoding.ASCII.GetBytes(sTemp);
        }

        /// <summary>   
        /// 获得初始向量IV   
        /// </summary>   
        /// <returns>初试向量IV</returns>   
        private static byte[] GetLegalIV()
        {
            string sTemp = "E4ghj*Ghg7!rNIfb&95GUY86GfghUb#er57HBh(u%g6HJ($jhWk7&!hg4ui%$hjk";
            mobjCryptoService.GenerateIV();
            byte[] bytTemp = mobjCryptoService.IV;
            int IVLength = bytTemp.Length;
            if (sTemp.Length > IVLength)
                sTemp = sTemp.Substring(0, IVLength);
            else if (sTemp.Length < IVLength)
                sTemp = sTemp.PadRight(IVLength, ' ');
            return ASCIIEncoding.ASCII.GetBytes(sTemp);
        }

        /// <summary>   
        /// 加密方法   
        /// </summary>   
        /// <param name="Source">待加密的串</param>   
        /// <returns>经过加密的串</returns>   
        public static string Encrypt(string Source)
        {
            byte[] bytIn = UTF8Encoding.UTF8.GetBytes(Source);
            MemoryStream ms = new MemoryStream();
            mobjCryptoService.Key = GetLegalKey();
            mobjCryptoService.IV = GetLegalIV();
            ICryptoTransform encrypto = mobjCryptoService.CreateEncryptor();
            CryptoStream cs = new CryptoStream(ms, encrypto, CryptoStreamMode.Write);
            cs.Write(bytIn, 0, bytIn.Length);
            cs.FlushFinalBlock();
            ms.Close();
            byte[] bytOut = ms.ToArray();
            return Convert.ToBase64String(bytOut);
        }

        /// <summary>
        /// 可以设置密钥的加密方式
        /// </summary>
        /// <param name="sourceData">原文</param>
        /// <param name="key">密钥，8位数字，字符串方式</param>
        /// <returns></returns>
        public static string Encrypt(string sourceData, string key)
        {
            //set key and initialization vector values
            //Byte[] key = new byte[] {0x21, 2, 0x88, 4, 5, 0x56, 7, 0x99};
            //Byte[] iv = new byte[] {0x21, 2, 0x88, 4, 5, 0x56, 7, 0x99};

            #region 检查密钥是否符合规定
            if (key.Length > 8)
            {
                key = key.Substring(0, 8);
            }
            #endregion

            char[] tmp = key.ToCharArray();
            Byte[] keys = new byte[8];
            //Byte[] keys = new byte[] { 1, 2, 3, 4, 5, 6, 7, 8 };
            Byte[] iv = new byte[] { 1, 2, 3, 4, 5, 6, 7, 8 };

            //设置密钥
            for (int i = 0; i < 8; i++)
            {
                if (tmp.Length > i)
                {
                    keys[i] = (byte)tmp[i];
                }
                else
                {
                    keys[i] = (byte)i;
                }
            }

            try
            {
                //convert data to byte array
                Byte[] sourceDataBytes = System.Text.ASCIIEncoding.UTF8.GetBytes(sourceData);
                //get target memory stream
                MemoryStream tempStream = new MemoryStream();
                //get encryptor and encryption stream
                DESCryptoServiceProvider encryptor = new DESCryptoServiceProvider();
                CryptoStream encryptionStream = new CryptoStream(tempStream, encryptor.CreateEncryptor(keys, iv), CryptoStreamMode.Write);

                //encrypt data
                encryptionStream.Write(sourceDataBytes, 0, sourceDataBytes.Length);
                encryptionStream.FlushFinalBlock();

                //put data into byte array
                Byte[] encryptedDataBytes = tempStream.GetBuffer();
                //convert encrypted data into string
                return System.Convert.ToBase64String(encryptedDataBytes, 0, (int)tempStream.Length);
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        /// <summary>   
        /// 解密方法   
        /// </summary>   
        /// <param name="Source">待解密的串</param>   
        /// <returns>经过解密的串</returns>   
        public static string Decrypt(string Source)
        {
            byte[] bytIn = Convert.FromBase64String(Source);
            MemoryStream ms = new MemoryStream(bytIn, 0, bytIn.Length);
            mobjCryptoService.Key = GetLegalKey();
            mobjCryptoService.IV = GetLegalIV();
            ICryptoTransform encrypto = mobjCryptoService.CreateDecryptor();
            CryptoStream cs = new CryptoStream(ms, encrypto, CryptoStreamMode.Read);
            StreamReader sr = new StreamReader(cs);
            return sr.ReadToEnd();
        }

        /// <summary>
        /// 解密
        /// </summary>
        /// <param name="sourceData">密文</param>
        /// <param name="key">密钥，8位数字，字符串方式</param>
        /// <returns></returns>
        public static string Decrypt(string sourceData, string key)
        {
            #region 检查密钥是否符合规定
            if (key.Length > 8)
            {
                key = key.Substring(0, 8);
            }
            #endregion

            char[] tmp = key.ToCharArray();
            Byte[] keys = new byte[8];
            Byte[] iv = new byte[] { 1, 2, 3, 4, 5, 6, 7, 8 };

            //设置密钥
            for (int i = 0; i < 8; i++)
            {
                if (tmp.Length > i)
                {
                    keys[i] = (byte)tmp[i];
                }
                else
                {
                    keys[i] = (byte)i;
                }
            }

            try
            {
                //convert data to byte array
                Byte[] encryptedDataBytes = System.Convert.FromBase64String(sourceData);
                //get source memory stream and fill it 
                MemoryStream tempStream = new MemoryStream(encryptedDataBytes, 0, encryptedDataBytes.Length);
                //get decryptor and decryption stream 
                DESCryptoServiceProvider decryptor = new DESCryptoServiceProvider();
                CryptoStream decryptionStream = new CryptoStream(tempStream, decryptor.CreateDecryptor(keys, iv), CryptoStreamMode.Read);

                //decrypt data 
                StreamReader allDataReader = new StreamReader(decryptionStream);

                return allDataReader.ReadToEnd();
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }
    }
}
