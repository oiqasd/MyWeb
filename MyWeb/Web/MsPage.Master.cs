using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Web.Helper;

namespace YZ.Web.Asp
{
    public partial class MsPage : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session[SessionHelper.SessionKey_User_UserID] == null || (long)Session[SessionHelper.SessionKey_User_UserID] == 0)
            {
                divusr.InnerHtml = "<a class='theme-login'>登录</a>&nbsp;&nbsp;<a href='reg.html' target='_blank'>注册</a>";// href='login.html'
            }
            else
            {
                //string str = "<ul class='sub-menu'><li><a href='LoginOut.aspx'>退出</a></li></ul>";
                divusr.InnerHtml = "<a href='userCenter.aspx'>" + Session[SessionHelper.SessionKey_User_UserName] + "</a>&nbsp;&nbsp;<a href='LoginOut.aspx'>退出</a>";
            }
        }
    }
}