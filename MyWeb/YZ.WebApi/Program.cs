using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using YZ.Common;
using YZ.Common.Log;
using YZ.WebApi;

namespace YZ.Service.WebApi
{
    class Program
    {

        static void Main(string[] args)
        {

            if (ConfigHelper.AppSetting<int>("ServerRunType", 1) == 1)
            {
                ConsoleStart();
            }
            else
            {
                ServiceStart();
            }

        }

        /// <summary>
        /// 控制台启动
        /// </summary>
        static void ConsoleStart()
        {
            try
            {

                AppHost.Instance.Start();

                while (Console.ReadLine().ToLower() != "exit")
                {
                    //空循环，保证只有输入Exit才退出
                }

                Console.WriteLine("系统退出...");
                AppHost.Instance.HostDispose();
                Console.WriteLine("系统关闭。");
                Console.ReadKey(false);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw ex;
            }
        }

        /// <summary>
        /// 服务启动
        /// </summary>
        static void ServiceStart()
        {
            ServiceBase[] ServicesToRun = new ServiceBase[]{
              new WebApiService() 
            };
            ServiceBase.Run(ServicesToRun);
        }
    }

}
