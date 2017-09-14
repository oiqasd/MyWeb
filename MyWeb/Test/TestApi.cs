using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using YZ.Common.Util;

namespace Test
{
    public partial class TestApi : Form
    {
        List<SDKApiInfo> API = new List<SDKApiInfo>();
        public TestApi()
        {
            InitializeComponent(); 
        }

        private void TestApi_Load(object sender, EventArgs e)
        {
            API = ParamJson.GetApiInfo();
            LoadData();
        }

        private void LoadData()
        {
            listBox1.Items.Clear();
            foreach (var item in API)
            {
                string str = item.ActionDesc;
                listBox1.Items.Add(str);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedItem == null)
            {
                MessageBox.Show("未选择api");
                return;
            }
            (sender as Button).Enabled = false;
            lblFlag.Text = "";
            try
            {
                string url = System.Configuration.ConfigurationManager.AppSettings["webapiurl"];
                string md5key = System.Configuration.ConfigurationManager.AppSettings["md5key"];
                string appid = System.Configuration.ConfigurationManager.AppSettings["appid"];

                ServiceRequest req = new ServiceRequest();
                req.AppId = appid;
                req.TimeStamp = DateTimeHelper.ConvertDateTimeInt(DateTime.Now);
                //req.IsEncrypt = 0;
                req.Version = "v1";
                //req.ClientIp = "127.0.0.1";

                GetTextInput(API[listBox1.SelectedIndex].Type, API[listBox1.SelectedIndex].ActionName,
                    API[listBox1.SelectedIndex], ref req);

                SortedDictionary<String, Object> dicReq = Utils.EntityToMap(req);
                dicReq.Remove("Sign");
                //组合加密字符串
                string strData = string.Empty;
                foreach (string key in dicReq.Keys)
                {
                    object o = dicReq[key];
                    if (o != null)
                        strData += o.ToString();
                }

                //md5签名
                strData += md5key;

                string signStr = string.Format("{0}{1}{2}{3}{4}{5}{6}", req.AppId, req.Version, req.MessageType, req.ActionName, req.Data, req.TimeStamp, md5key);
                string sign = YZ.Common.Cryptography.CrypMD5.MD5(signStr);
                req.Sign = sign;


                string ret = HttpHelper.DoHttpPost(url, JsonHelper.Serialize(req));
                NEasyServiceResponse res = new NEasyServiceResponse();
                if (!string.IsNullOrEmpty(ret))
                    res = JsonHelper.Deserialize<NEasyServiceResponse>(ret);
                if (res != null)
                {
                    if (res.Code == 200)
                        lblFlag.Text = string.Format("成功执行:{0}", res.Code);
                    else
                        lblFlag.Text = string.Format("执行失败:{0},错误他提示:{1}", res.Code, res.Msg);
                    if (!string.IsNullOrEmpty(res.Body))
                        richTextBox1.AppendText(res.Body);
                }
            }
            catch (Exception ex)
            {
                richTextBox1.AppendText(ex.Message);
            }
            finally
            {
                (sender as Button).Enabled = true;
            }
        }
        private void GetTextInput(string ControllerName, string ActionName, SDKApiInfo info, ref ServiceRequest jr)
        {
            SortedDictionary<string, object> sd = new SortedDictionary<string, object>();
            int i = 0;
            foreach (var item in info.NeedParams)
            {
                var textbox = tableLayoutPanel1.Controls.Find("text_" + item[0], false);
                if (null != textbox[0])
                {
                    string str = textbox[0].Text;
                    sd.Add(item[0], str);
                    i++;
                }
            }
            jr.MessageType = ControllerName;
            jr.ActionName = ActionName;
            if (sd.Count == 0)
                jr.Data = "";
            else
                jr.Data = JsonHelper.Serialize(sd);
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            apiname.Text = API[listBox1.SelectedIndex].ActionName;
            apidesc.Text = API[listBox1.SelectedIndex].ActionDesc;
            ShowInput(API[listBox1.SelectedIndex]);
            lblFlag.Text = "";
        }

        private void ShowInput(SDKApiInfo info)
        {
            tableLayoutPanel1.Controls.Clear();
            foreach (var item in info.NeedParams)
            {
                Label l = new Label();
                l.Text = item[0];
                l.TextAlign = ContentAlignment.MiddleLeft;
                tableLayoutPanel1.Controls.Add(l);
                TextBox t = new TextBox();
                t.Text = item[1];
                t.Name = "text_" + l.Text;
                tableLayoutPanel1.Controls.Add(t);
            }
        }

    }

}
