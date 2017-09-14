using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YZ.Service.Model.DTO
{
   public class SensitiveMessageRequest:BaseRequest
    {
        /// <summary>
        /// 使用AES加密的Json字符串
        /// </summary>
        public override string Data { get; set; }
        /// <summary>
        /// AES Key 经过RSA加密的，用来解密消息体的秘钥，必填
        /// </summary>
        public string Key { get; set; }
        /// <summary>
        /// 签名 对传输的字符串信息的md5签名和Hash签名，保证提交的字符串不被篡改
        /// </summary>
        public string Sign { get; set; }
    }
}
