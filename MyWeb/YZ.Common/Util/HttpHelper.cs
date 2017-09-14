using System;
using System.IO;
using System.Net;
using System.Text; 

namespace YZ.Common.Util
{
    public class HttpHelper
    {
        /// <summary>
        /// 发生post请求
        /// </summary>
        /// <param name="Url"></param>
        /// <param name="postDataStr"></param>
        /// <returns></returns>
        public static string DoHttpPost(string Url, string postDataStr)
        {
            //ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3;
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(Url);
            request.Method = "POST";
            request.ContentType = "application/json";
            byte[] bData = (Encoding.UTF8.GetBytes(postDataStr));
            request.ContentLength = bData.Length;
            Stream writeStream = request.GetRequestStream();
            writeStream.Write(bData, 0, bData.Length);
            writeStream.Close();

            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            {

                Stream myResponseStream = response.GetResponseStream();
                StreamReader myStreamReader = new StreamReader(myResponseStream, Encoding.GetEncoding("utf-8"));
                string retString = myStreamReader.ReadToEnd();
                myStreamReader.Close();
                myResponseStream.Close();

                return retString;
            }
        }
    }
}
