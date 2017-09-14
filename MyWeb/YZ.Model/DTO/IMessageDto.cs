using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YZ.Service.Model.DTO
{
    public interface IMessageDto
    {

        /// <summary>
        /// 消息类型
        /// </summary>
        string MessageType { get; set; }
        /// <summary>
        /// 执行方法体名称
        /// </summary>
        string ActionName { get; set; }
        /// <summary>
        /// 当前应用程序ID
        /// </summary>
        string AppId { get; set; }
        /// <summary>
        /// 具体参数的Json字符串的Base64编码
        /// </summary>
        string Data { get; set; }
        /// <summary>
        /// 机器码  每个设备唯一的设备码
        /// </summary>
        string MachineCode { get; set; }
        /// <summary>
        /// 时间戳
        /// </summary>
        Int64 TimeStamp { get; set; }
        /// <summary>
        /// 用户状态  如果不需要状态的就传空字符串就行了“”
        /// </summary>
        string Token { get; set; }
        /// <summary>
        /// 版本号
        /// </summary>
        string Version { get; set; }
        /// <summary>
        /// 签名 对传输的字符串信息的md5签名和Hash签名，保证提交的字符串不被篡改
        /// </summary>
        //string Sign { get; set; }
    }
}
