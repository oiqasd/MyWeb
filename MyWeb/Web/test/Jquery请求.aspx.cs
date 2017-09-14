using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Web.test
{
    public partial class Jquery请求 : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            Console.WriteLine("yyyasda");
            var act = Request["Action"];
            var prm = Request["Name"];
            if (act == "get")
            {
                Response.Clear();
                Response.Write(get1(prm));
                Response.End();
            }
        }

        public string get1(string prm)
        {
            return prm;
        }


        private delegate int Method();

        private int method()
        {

            return 100;
        }

        private void MethodComplete(IAsyncResult asyanc)
        {
            if (asyanc == null)
            {
                return;
            }
            textbox.Text = (asyanc.AsyncState as Method).EndInvoke(asyanc).ToString();
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            //Method my = method;
            //IAsyncResult ias = my.BeginInvoke( MethodComplete,my);

            HttpWebRequest hreq = (HttpWebRequest)WebRequest.Create("http://www.qianbb.net:8081/");
            IAsyncResult ias = hreq.BeginGetResponse(Httprequest, hreq);
        }

        //---------------------------------------


        private void Httprequest(IAsyncResult iar)
        {
            if (iar == null) return;

            HttpWebRequest hrq = iar.AsyncState as HttpWebRequest;
            HttpWebResponse hrs = (HttpWebResponse)hrq.EndGetResponse(iar);
            System.IO.StreamReader sr = new System.IO.StreamReader(hrs.GetResponseStream());
            isa.InnerText = sr.ReadToEnd();
        }
    }
}