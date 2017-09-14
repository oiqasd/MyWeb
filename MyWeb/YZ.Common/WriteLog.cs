using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YZ.Common
{
   public class WriteLog
    {
       public static void ErrorLog(string outmsg)
       { 
           using (StreamWriter sw = new StreamWriter(AppDomain.CurrentDomain.BaseDirectory + "ErrLog.txt", true))
           {
               sw.WriteLine("Time:" + System.DateTime.Now.ToLongTimeString());
               sw.WriteLine("Message:" + outmsg);
           }
       }

       public static void ExceptionLog(Exception ex)
       {
           using (StreamWriter sw = new StreamWriter(AppDomain.CurrentDomain.BaseDirectory + "Log\\ErrLog.txt", true))
           {
               sw.WriteLine("Time:" + System.DateTime.Now.ToLongTimeString());
               sw.WriteLine("Exception:" + ex);
               sw.WriteLine("Message:" + ex.Message);
           }
       }
    }
}
