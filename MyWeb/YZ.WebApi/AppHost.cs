using System;
using System.Text;
using ServiceStack;
using YZ.Service.WebApi;
using YZ.Common;


namespace YZ.WebApi
{
    /// <summary>
    /// 启动ServiceStack端口监听服务
    /// </summary>
    public class AppHost : AppHostHttpListenerBase
    {

        #region Instance

        private AppHost() : base("YZ's AppHost", typeof(ServiceHandler).Assembly) { }

        private static AppHost _Instance = null;
        private static object _syncObject = new object();
        public string ListeningOn { get; set; }

        public new static AppHost Instance
        {
            get
            {

                if (_Instance == null)
                {
                    lock (_syncObject)
                        if (_Instance == null)
                            _Instance = new AppHost();
                }

                return _Instance;
            }
        }



        #endregion

        //public AppHost()
        //    : base("YZ.Service AppHost", typeof(ServiceHandler).Assembly)
        //{

        //}

        public override void Configure(Funq.Container container)
        {
            //throw new NotImplementedException();
        }

        public void Start()
        {
            string servicePort = ConfigHelper.AppSetting<string>("ServerPort", "9998");
            ListeningOn = "http://*:" + servicePort + "/";

            _Instance.Init();
            _Instance.Start(ListeningOn);

            Console.WriteLine("AppHost Created at {0}, listening on {1}", DateTime.Now, ListeningOn);
        }

        public void HostDispose()
        {
            if (_Instance != null)
            {
                _Instance.Stop();
                _Instance.Dispose();
                _Instance = null;
            }
        }
    }
}
