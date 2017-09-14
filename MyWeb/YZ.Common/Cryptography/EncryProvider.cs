using System;
using System.Collections.Generic;
using System.Text;
using System.Security.Cryptography;
using System.IO;

namespace YZ.Common.Cryptography
{
    /// <summary>   
    /// �ԳƼ����㷨��   
    /// </summary>   
    public class EncryProvider
    {

        private SymmetricAlgorithm mobjCryptoService;
        private static readonly string Key = "CaTct(%&hj7x89H$yuBI0456FtmaT5&fvHUFCy76*h%(HilJ$lhj!y6&(*jkP87j1p";
         
        /// <summary>   
        /// �ԳƼ�����Ĺ��캯��   
        /// </summary>   
            public EncryProvider()
        {
            mobjCryptoService = new RijndaelManaged();
        }
        /// <summary>   
        /// �����Կ   
        /// </summary>   
        /// <returns>��Կ</returns>   
        private byte[] GetLegalKey()
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
        /// ��ó�ʼ����IV   
        /// </summary>   
        /// <returns>��������IV</returns>   
        private byte[] GetLegalIV()
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
        /// ���ܷ���   
        /// </summary>   
        /// <param name="Source">�����ܵĴ�</param>   
        /// <returns>�������ܵĴ�</returns>   
        public string Encrypto<T>(T Source)
        {
            byte[] bytIn = UTF8Encoding.UTF8.GetBytes(Source.ToString());
            MemoryStream ms = new MemoryStream();
            ICryptoTransform encrypto = null;
            CryptoStream cs = null;
            byte[] bytOut=null;
            try
            {
                mobjCryptoService.Key = GetLegalKey();
                mobjCryptoService.IV = GetLegalIV();
                encrypto = mobjCryptoService.CreateEncryptor();
                cs = new CryptoStream(ms, encrypto, CryptoStreamMode.Write);
                cs.Write(bytIn, 0, bytIn.Length);
                cs.FlushFinalBlock();
                bytOut = ms.ToArray();
            }            
            finally
            {
                if(encrypto!=null)encrypto.Dispose();
                if (ms != null) ms.Close();
                if (cs != null) cs.Close();
            }
            return Convert.ToBase64String(bytOut);
        }
        /// <summary>   
        /// ���ܷ���   
        /// </summary>   
        /// <param name="Source">�����ܵĴ�</param>   
        /// <returns>�������ܵĴ�</returns>   
        public string Decrypto<T>(T Source)
        {
            string rtnStr = string.Empty;
            MemoryStream ms = null;
            ICryptoTransform encrypto = null;
            CryptoStream cs = null;
            StreamReader sr = null;
            try
            {
                byte[] bytIn = Convert.FromBase64String(Source.ToString().Replace(" ", "+"));
                ms = new MemoryStream(bytIn, 0, bytIn.Length);
                mobjCryptoService.Key = GetLegalKey();
                mobjCryptoService.IV = GetLegalIV();
                encrypto = mobjCryptoService.CreateDecryptor();
                cs = new CryptoStream(ms, encrypto, CryptoStreamMode.Read);
                sr = new StreamReader(cs);
                rtnStr = sr.ReadToEnd();
            }
            finally {
                if (encrypto != null) encrypto.Dispose();
                if (ms != null) ms.Close();
                if (cs != null) cs.Close();
                if (sr != null) cs.Close();
            }
            return rtnStr;
        }
    }
}
