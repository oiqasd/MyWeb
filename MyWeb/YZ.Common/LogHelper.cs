using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YZ.Service.Common
{
    public class LogHelper
    {
        public static void Debug(string title, string message)
        {
            using (StreamWriter sw = new StreamWriter(AppDomain.CurrentDomain.BaseDirectory + "Log\\Debug.txt", true))
            {
                sw.WriteLine();
                sw.WriteLine("Time:" + System.DateTime.Now.ToLongTimeString());
                sw.WriteLine("Title:" + title);
                sw.WriteLine("Message:" + message);
            }
        }
        public static void Info(string title, string message)
        {
            using (StreamWriter sw = new StreamWriter(AppDomain.CurrentDomain.BaseDirectory + "Log\\Error.txt", true))
            {
                sw.WriteLine();
                sw.WriteLine("Time:" + System.DateTime.Now.ToLongTimeString());
                sw.WriteLine("Title:" + title);
                sw.WriteLine("Message:" + message);
            }
        }
        public static void Error(string title, string message)
        {
            using (StreamWriter sw = new StreamWriter(AppDomain.CurrentDomain.BaseDirectory + "Log\\Error.txt", true))
            {
                sw.WriteLine();
                sw.WriteLine("Time:" + System.DateTime.Now.ToLongTimeString());
                sw.WriteLine("Title:" + title);
                sw.WriteLine("Message:" + message);
            }
        }
        public static void Error(string title, string message, Exception ex)
        {
            using (StreamWriter sw = new StreamWriter(AppDomain.CurrentDomain.BaseDirectory + "Log\\Error.txt", true))
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
            using (StreamWriter sw = new StreamWriter(AppDomain.CurrentDomain.BaseDirectory + "Log\\Fatal.txt", true))
            {
                sw.WriteLine();
                sw.WriteLine("Time:" + System.DateTime.Now.ToLongTimeString());
                sw.WriteLine("Title:" + title);
                sw.WriteLine("Message:" + message);
            }
        }
        public static void Fatal(string title, string message, Exception ex)
        {
            using (StreamWriter sw = new StreamWriter(AppDomain.CurrentDomain.BaseDirectory + "Log\\Fatal.txt", true))
            {
                sw.WriteLine();
                sw.WriteLine("Time:" + System.DateTime.Now.ToLongTimeString());
                sw.WriteLine("Exception:" + ex);
                sw.WriteLine("Message:" + ex.Message);
            }
        }

    }
}
