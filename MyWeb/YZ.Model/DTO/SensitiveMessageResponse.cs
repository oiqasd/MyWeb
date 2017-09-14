using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YZ.Service.Model.DTO
{
    public class SensitiveMessageResponse
    {
        /// <summary>
        /// 返回参数的Json字符串的Base64编码
        /// </summary>
        public string Data { get; set; }
        /// <summary>
        /// AES Key 经过RSA加密的，用来解密消息体的秘钥，必填
        /// </summary>
        public string Key { get; set; }
        /// <summary>
        /// 结果码 0 表示成功，其他表示操作有问题
        /// </summary>
        public int ResultCode { get; set; }
        /// <summary>
        /// 结果描述
        /// </summary>
        public string ResultMsg { get; set; }
        /// <summary>
        /// 签名 对传输的字符串信息的签名，保证提交的字符串不被篡改
        /// </summary>
        public string Sign { get; set; }
    }
}
