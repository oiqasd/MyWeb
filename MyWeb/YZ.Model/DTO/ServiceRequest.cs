using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YZ.Service.Model.DTO
{
    /// <summary>
    /// 对外公开的消息请求传输对象，提供服务器端调用
    /// </summary>
    public class ServiceRequest : BaseRequest
    {
        /// <summary>
        /// 签名 对传输的字符串信息的md5签名和Hash签名，保证提交的字符串不被篡改
        /// </summary>
        public string Sign { get; set; }
    }
}
