using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Web.Util.Expand;

namespace YZ.Web.Asp
{
    public partial class UcComment : System.Web.UI.UserControl
    {
        public int articleid { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                articleid = Convert.ToInt32(this.Attributes["aid"]);// aid.ToString();
                List<YZ.Biz.Comment> list = YZ.Biz.CommentRepository.Instance.CommentList(articleid, 0, YZ.Common.ConfigHelper.AppSetting<int>("CommentDefaultCount", 5));
                int index=1;
                if (list != null && list.Count > 0)
                {
                    foreach (var m in list)
                    {
                        UserControl uc = (UserControl)Page.LoadControl("control/UcSubComment.ascx");
                        uc.Attributes["data-id"] = m.id.ToString();
                        uc.Attributes["data-msg"] = m.content;
                        uc.Attributes["data-ip"] = m.ipaddress;
                        uc.Attributes["data-uname"] = m.username;
                        uc.Attributes["data-cool"] = m.cool.ToString();
                        uc.Attributes["data-index"] = index.ToString();
                        uc.Attributes["data-date"] = m.createtime.ToString(1);
                        place.Controls.Add(uc);
                        index += 1;
                    }
                }
            }

            //UserControl uc = (UserControl)Page.LoadControl("control/UcSubComment.ascx");
            //uc.Attributes["data-id"] = "1";
            //place.Controls.Add(uc);
            //UserControl uc1 = (UserControl)Page.LoadControl("control/UcSubComment.ascx");
            //uc1.Attributes["data-id"] = "2";
            //place.Controls.Add(uc1);
            //place.Controls.Add((UserControl)Page.LoadControl("UcSubComment.ascx"));
        }
    }
}