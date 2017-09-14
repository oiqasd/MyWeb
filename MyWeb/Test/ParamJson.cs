using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YZ.Common.Util;

namespace Test
{
    public class ServiceRequest
    {
        /// <summary>
        /// 消息类型
        /// </summary>
        public string MessageType { get; set; }
        /// <summary>
        /// 执行方法体名称
        /// </summary>
        public string ActionName { get; set; }
        /// <summary>
        /// 当前应用程序ID
        /// </summary>
        public string AppId { get; set; }
        /// <summary>
        /// 具体参数的Json字符串的Base64编码
        /// </summary>
        public virtual string Data { get; set; }
        /// <summary>
        /// 机器码  每个设备唯一的设备码，和钱包、配货一致
        /// </summary>
        public string MachineCode { get; set; }
        /// <summary>
        /// 时间戳
        /// </summary>
        public Int64 TimeStamp { get; set; }
        /// <summary>
        /// 用户状态  如果不需要状态的就传空字符串就行了""
        /// </summary>
        public string Token { get; set; }
        /// <summary>
        /// 版本号
        /// </summary>
        public string Version { get; set; }
        public string Sign { get; set; }
    }

    public class NEasyServiceResponse
    {
        public int Code { get; set; }
        public string Msg { get; set; }
        public string Body { get; set; }

        public void Fill(int code, string msg = null, string data = null)
        {
            this.Code = code;
            this.Msg = msg;
            this.Body = data;
        }
    }

    public class SDKApiInfo
    {
        public string Type;
        public string ActionName;
        public string ActionDesc;
        public List<List<string>> NeedParams;
    }

    public class ParamJson
    {
        public static string jsonText = "";
        static ParamJson()
        {
            StreamReader sr = File.OpenText("sdk.json");
            StringBuilder jsonArrayText_tmp = new StringBuilder();
            string input = null;
            while ((input = sr.ReadLine()) != null)
            {
                jsonArrayText_tmp.Append(input);
            }
            sr.Close();
            jsonText = jsonArrayText_tmp.ToString();
        }

        public static List<SDKApiInfo> GetApiInfo()
        { 
            return JsonHelper.Deserialize<List<SDKApiInfo>>(jsonText);
        }
    }
}
