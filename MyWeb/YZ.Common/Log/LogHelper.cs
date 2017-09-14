using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YZ.Common.Log
{
    public class LogHelper
    {
        static string BaseDic = AppDomain.CurrentDomain.BaseDirectory + "Log\\";
        static LogHelper()
        {
            if (!Directory.Exists(BaseDic))
                Directory.CreateDirectory(BaseDic);
        }
        public static void Debug(string title, string message)
        {
            using (StreamWriter sw = new StreamWriter(BaseDic + "Debug.txt", true))
            {
                sw.WriteLine();
                sw.WriteLine("Time:" + System.DateTime.Now.ToLongTimeString());
                sw.WriteLine("Title:" + title);
                sw.WriteLine("Message:" + message);
            }
        }
        public static void Info(string title, string message)
        {
            using (StreamWriter sw = new StreamWriter(BaseDic + "Info.txt", true))
            {
                sw.WriteLine();
                sw.WriteLine("Time:" + System.DateTime.Now.ToLongTimeString());
                sw.WriteLine("Title:" + title);
                sw.WriteLine("Message:" + message);
            }
        }
        public static void Error(string title, string message)
        {
            using (StreamWriter sw = new StreamWriter(BaseDic + "Error.txt", true))
            {
                sw.WriteLine();
                sw.WriteLine("Time:" + System.DateTime.Now.ToLongTimeString());
                sw.WriteLine("Title:" + title);
                sw.WriteLine("Message:" + message);
            }
        }
        public static void Error(string title, string message, Exception ex)
        {
            using (StreamWriter sw = new StreamWriter(BaseDic + "Error.txt", true))
            {
                sw.WriteLine();
                sw.WriteLine("Time:" + System.DateTime.Now.ToLongTimeString());
                sw.WriteLine("Title:" + title);
                sw.WriteLine("Message:" + message);
                sw.WriteLine("Exception:" + ex);
            }
        }

        public static void Fatal(string title, string message)
        {
            using (StreamWriter sw = new StreamWriter(BaseDic + "Fatal.txt", true))
            {
                sw.WriteLine();
                sw.WriteLine("Time:" + System.DateTime.Now.ToLongTimeString());
                sw.WriteLine("Title:" + title);
                sw.WriteLine("Message:" + message);
            }
        }
        public static void Fatal(string title, string message, Exception ex)
        {
            using (StreamWriter sw = new StreamWriter(BaseDic + "Fatal.txt", true))
            {
                sw.WriteLine();
                sw.WriteLine("Time:" + System.DateTime.Now.ToLongTimeString());
                sw.WriteLine("Exception:" + ex);
                sw.WriteLine("Message:" + ex.Message);
            }
        }

    }
}
