using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Test
{
    public partial class test : Form
    {
        public test()
        {
            InitializeComponent();
            Control.CheckForIllegalCrossThreadCalls = false; 
        }

       
        private void btnTest_Click(object sender, EventArgs e)
        {
            try
            {
                for (int i = 0; i < Convert.ToInt32(txtCount.Text); i++)
                {
                    try
                    {
                        Task.Factory.StartNew((obj) =>
                        {
                            try
                            {
                                ///aaa client = new aaa();

                                //Console.WriteLine("第 {0} 个请求开始。。。", obj);
                                txtMsg.AppendText(string.Format("第 {0} 个请求开始。。。", obj));
                                txtMsg.AppendText(Environment.NewLine);
                                //client.Get("12312"); 

                                //Console.WriteLine("第 {0} 个请求结束。。。", obj);
                                txtMsg.AppendText(string.Format("第 {0} 个请求结束。。。", obj));
                                txtMsg.AppendText(Environment.NewLine);
                            }
                            catch (Exception ex)
                            {
                                //Console.WriteLine(ex.Message);
                                txtMsg.AppendText(ex.Message);
                            }
                        }, i);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }

                    Application.DoEvents();
                }

                Console.Read();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
