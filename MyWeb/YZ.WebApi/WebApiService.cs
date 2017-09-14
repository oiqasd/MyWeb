using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using YZ.Common.Log;
using YZ.WebApi;

namespace YZ.Service.WebApi
{
    partial class WebApiService : ServiceBase
    {
        public WebApiService()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            // TODO:  在此处添加代码以启动服务。
            AppHost.Instance.Start();
            LogHelper.Info("服务启动", "");
        }

        protected override void OnStop()
        {
            // TODO:  在此处添加代码以执行停止服务所需的关闭操作。
            AppHost.Instance.HostDispose();
            LogHelper.Info("服务关闭", "");
        }
    }
}
