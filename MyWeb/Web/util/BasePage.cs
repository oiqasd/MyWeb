using System;
using System.Web;
using System.Web.UI;
using Web.Helper;

namespace Web.Util
{

    /// <summary>
    /// UsersBase 的摘要说明
    /// </summary>
    public class BasePage : Page
    {
        public BasePage() { }

        /// <summary>
        /// 一定不为空
        /// </summary>
        protected string UserName
        {
            get
            {
                return Session[SessionHelper.SessionKey_User_UserName] as string;
            }
        }

        //protected string PhoneNumber
        //{
        //    get
        //    {
        //        return Session[SessionHelper.SessionKey_User_PhoneNumber] as string;
        //    }
        //}

        /// <summary>
        /// 用户ID
        /// </summary>
        protected long UserID
        {
            get
            {

                if (Session[SessionHelper.SessionKey_User_UserID] == null || (long)Session[SessionHelper.SessionKey_User_UserID] == 0)
                {
                    Session[SessionHelper.SessionKey_User_UserID] = GetUserId();
                }

                return (long)Session[SessionHelper.SessionKey_User_UserID];
            }
            //get
            //{
            //    if (Session[SessionHelper.SessionKey_User_UserID] == null)
            //    {
            //        return Guid.NewGuid();
            //    }
            //    try
            //    {
            //        return (Guid)Session[SessionHelper.SessionKey_User_UserID];
            //    }
            //    catch
            //    {
            //        return Guid.NewGuid();
            //    }

            //}
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(UserName) || UserID == 0)
            {
                Response.Redirect("~/login.html?returnurl=" + Server.UrlEncode(this.Page.Request.Url.AbsoluteUri),true);
                return;
            }
        }

        /// <summary>
        /// 添加日志
        /// </summary>
        /// <param name="user">操作者</param>
        /// <param name="contents">内容</param>
        /// <param name="state">状态</param>
        public void AddLog(string user, string contents, string state)
        {
            //B_sysLog bll = new B_sysLog();
            //M_sysLog log = new M_sysLog { operators = user, contents = contents, status = state, dates = DateTime.Now };
            //bll.Add(log);
        }

        /// <summary>
        /// 获取用户ID
        /// </summary>
        /// <returns></returns>
        private long GetUserId()
        {
            if (!string.IsNullOrEmpty(UserName))
            {
                YZ.Biz.CacheRepository biz = new YZ.Biz.CacheRepository();

                return biz.CachedUserInfoByName(UserName).U_Id;
            }
            return 0;
        }
    }

    public class BaseUserControl : UserControl
    {
        public BaseUserControl() { }

        /// <summary>
        /// 一定不为空
        /// </summary>
        protected string UserName
        {
            get
            {
                return Session[SessionHelper.SessionKey_User_UserName] as string;
            }
        }

        //protected string PhoneNumber
        //{
        //    get
        //    {
        //        return Session[SessionHelper.SessionKey_User_PhoneNumber] as string;
        //    }
        //}

        /// <summary>
        /// 用户ID
        /// </summary>
        protected long UserID
        {
            get
            {

                if (Session[SessionHelper.SessionKey_User_UserID] == null || (long)Session[SessionHelper.SessionKey_User_UserID] == 0)
                {
                    Session[SessionHelper.SessionKey_User_UserID] = GetUserId();
                }

                return (long)Session[SessionHelper.SessionKey_User_UserID];
            }
            //get
            //{
            //    if (Session[SessionHelper.SessionKey_User_UserID] == null)
            //    {
            //        return Guid.NewGuid();
            //    }
            //    try
            //    {
            //        return (Guid)Session[SessionHelper.SessionKey_User_UserID];
            //    }
            //    catch
            //    {
            //        return Guid.NewGuid();
            //    }

            //}
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(UserName) || UserID == 0)
            {
                System.Web.HttpContext.Current.Response.Redirect("~/login.html?returnurl=" + Server.UrlEncode(this.Page.Request.Url.AbsoluteUri));
                return;
            }
        }

        /// <summary>
        /// 添加日志
        /// </summary>
        /// <param name="user">操作者</param>
        /// <param name="contents">内容</param>
        /// <param name="state">状态</param>
        public void AddLog(string user, string contents, string state)
        {
            //B_sysLog bll = new B_sysLog();
            //M_sysLog log = new M_sysLog { operators = user, contents = contents, status = state, dates = DateTime.Now };
            //bll.Add(log);
        }

        /// <summary>
        /// 获取用户ID
        /// </summary>
        /// <returns></returns>
        private long GetUserId()
        {
            if (!string.IsNullOrEmpty(UserName))
            {
                YZ.Biz.CacheRepository biz = new YZ.Biz.CacheRepository();

                return biz.CachedUserInfoByName(UserName).U_Id;
            }
            return 0;
        }
    }
}