using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YZ.Biz.Entity
{
    public class  ResponseMsgWraper<T>
    {
        public ResponseMsgWraper()
        {
            IsSuccess = false;
            ErrorCode = "0000";
        }
        /// <summary>
        /// 是否成功
        /// </summary>
        public bool IsSuccess { get; set; }
        /// <summary>
        /// 错误码
        /// </summary>
        public string ErrorCode { get; set; }
        /// <summary>
        /// 返回消息
        /// </summary>
        public string Message { get; set; }
        /// <summary>
        /// 响应主体
        /// </summary>
        public T Body { get; set; }
    }
    public class ResponseMsg : ResponseMsgWraper<object>
    { }
}
