using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace YZ.Web.Asp
{
    /// <summary>
    /// 评论回复列表
    /// </summary>
    public partial class UcSubComment : System.Web.UI.UserControl
    {
        public string data_id { get; set; }
        public string data_msg { get; set; }
        public string data_ip { get; set; } 
        public string data_uname { get; set; }
        public string data_cool { get; set; }
        public string data_date { get; set; }
        public string data_index { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                data_id = this.Attributes["data-id"];
                data_msg = this.Attributes["data-msg"];
                data_ip = this.Attributes["data-ip"]; 
                data_uname = this.Attributes["data_uname"] ?? "网友";
                data_cool = this.Attributes["data-cool"];
                data_date = this.Attributes["data-date"];
                data_index = this.Attributes["data-index"];

            }
        }
    }
}