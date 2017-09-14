using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YZ.Common.Cryptography
{
    public class CryptHash
    {
        /// <summary>
        /// 默认种子序号使用1
        /// </summary>
        /// <param name="pData"></param>
        /// <returns></returns>
        public static string HashData(string pData)
        {
            Byte[] FromData = System.Text.Encoding.GetEncoding("utf-8").GetBytes(pData);
            if (FromData.Length == 0)
                return "";
            string ret = HashData(FromData, FromData.Length, 1).ToString("x16");
            if (ret.Length > 8)
                ret = ret.Substring(ret.Length - 8, 8);
            if (ret.Length < 8)
            {
                for (int i = 0; i < 8 - ret.Length; i++)
                {
                    ret = "0" + ret;
                }
            }
            return ret;
        }
        //字节数组的哈希算法
        //pData:需要进行哈希运算的数据
        //dwDataLen:需要参与哈希运算的数据长度，单位是字节
        //dwHashType:哈希种子类型序号，取值范围是0到3 。
        //返回值：一个32位整型的哈希值。
        //为了降低碰撞概率，可以使用不同的dwHashType，计算得到2个哈希值，之后将两个结果组合为一个64位整型值用于判断
        public static uint HashData(byte[] pData, int dwDataLen, uint dwHashType)
        {
            if (dwHashType >= 4) return 0;
            byte[] key = pData;
            uint seed1 = 0x7FED7FED, seed2 = 0xEEEEEEEE;
            uint ch = 0;
            uint[] cryptTable = new uint[4] { 0x12356487, 0x24d85e23, 0xfa5e1d3c, 0xcd830957 };
            int i = 0;
            while (i < dwDataLen)
            {
                ch = (uint)((key[i]) + 32);

                seed1 = cryptTable[dwHashType] ^ (seed1 + seed2) * ch;
                seed2 = ch + seed1 + seed2 + (seed2 << 5) + 3;
                i++;
            }
            return seed1;
        }
    }
}
