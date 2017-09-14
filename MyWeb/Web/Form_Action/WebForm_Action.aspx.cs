using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Web.Helper;
using Web.Util;
using YZ.Biz;
using YZ.Biz.Entity;
using YZ.Common;
using YZ.Common.Log;
using YZ.Common.Util; 

namespace YZ.Web.Asp.Form_Action
{
    public partial class WebForm_Action : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                var action = Request["action"];
                if (action == "login")
                {
                    //登录
                    var name = Request["username"];
                    var pass = Request["password"];
                    Action_Login(name, pass);
                }
                else if (action == "loginout")
                {
                    //退出
                    loginout();
                }
                else if (action == "register")
                {
                    //注册
                    register();
                }
                else if (action == "asynclogin")
                {
                    //登录
                    var name = Request["username"];
                    var pass = Request["password"];
                    Response.Clear();
                    Response.Write(asynclogin(name, pass));
                    //HttpContext.Current.ApplicationInstance.CompleteRequest();
                    Response.End();
                }
                else if (action == "VaildateUsername")
                {
                    //验证用户名
                    var name = Request["username"];
                    if (string.IsNullOrWhiteSpace(name)) return;
                    Response.Clear();
                    Response.Write(VaildateUsername(name));
                    //HttpContext.Current.ApplicationInstance.CompleteRequest();
                    Response.End();
                }
                else if (action == "VaildatePassword")
                {
                    //验证密码
                    var name = Request["username"];
                    var password = Request["password"];
                    if (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(password)) return;
                    Response.Clear();
                    Response.Write(VaildatePassword(name, password));
                    //HttpContext.Current.ApplicationInstance.CompleteRequest();
                    Response.End();
                }
                else if (action == "numberfortune")
                {
                    //号码运势测试
                    var number = Request["number"];
                    Response.Clear();
                    Response.Write(NumberFortune(number));
                    //HttpContext.Current.ApplicationInstance.CompleteRequest();
                    Response.End();
                }
                else if (action == "comment")
                {
                    //评论功能
                    var type = Request["type"];//0:直接评论 1:回复评论

                    ResponseMsg resMsg = new ResponseMsg();
                    YZ.Biz.Comment model = new Comment();
                    model.titleID = Convert.ToInt32(Request["aid"]);//文章id
                    model.state = YZ.Common.ConfigHelper.AppSetting<int>("CommentDefaultState", 1);//初始状态
                    model.content = Request["msg"];//内容
                    model.ipaddress = Request["ip"];//IP地址
                    model.isp = Request["isp"];//网络服务商
                    model.browserInfo = Request["browser"];//浏览器信息
                    model.os = Request["os"];//系统
                    model.createtime = DateTime.Now;
                    model.userId = UserID;
                    model.username = UserName;

                    if (type == "1")
                    {
                        model.isson = true;
                        model.parentid = Convert.ToInt32(Request["pid"]);
                    }
                    else//直接评论
                    {
                        model.isson = false;
                        model.cool = 0;
                    }
                    if (YZ.Biz.CommentRepository.Instance.Add(model))
                    {
                        resMsg.IsSuccess = true;
                        resMsg.Message = "添加成功";
                    }
                    else
                    {
                        resMsg.Message = "添加失败";
                    }
                    Response.Clear();
                    Response.Write(JsonHelper.Serialize(resMsg));
                    //HttpContext.Current.ApplicationInstance.CompleteRequest();
                    Response.End();

                }
                else if (action == "numcool")
                {
                    //点赞
                    ResponseMsg resMsg = new ResponseMsg();
                    if (YZ.Biz.CommentRepository.Instance.Praise(Convert.ToInt32(Request["cid"])))
                    {
                        resMsg.IsSuccess = true;
                        resMsg.Message = "点赞成功";
                    }
                    else
                    {
                        resMsg.Message = "点赞失败";
                    }
                    Response.Clear();
                    Response.Write(JsonHelper.Serialize(resMsg));
                    //HttpContext.Current.ApplicationInstance.CompleteRequest();
                    Response.End();
                }
                else
                {
                    //登录页面
                    Response.Redirect("../login.html", false);
                }
            }
            catch (Exception ex)
            {
                //respose.End() 异常
                if (ex is System.Threading.ThreadAbortException) { }
                //System.Threading.Thread.ResetAbort();
                else
                    LogHelper.Fatal("WebForm_Action-Page_Load", ex.Message, ex);
            }
        }

        #region 登录
        private string asynclogin(string name, string pass)
        {
            try
            {
                Dictionary<int, string> ret = new Dictionary<int, string>();
                UserRepository biz = new UserRepository();
                var user = biz.GetUserinfoByLogin(name);
                if (user == null)
                {
                    ret.Add(0, "用户不存在");
                    return JsonHelper.Serialize(ret);
                }

                string password = BCrypt.Net.BCrypt.HashPassword(pass, user.U_Salt);
                if (password != user.U_Password)
                {
                    ret.Add(0, "密码错误");
                    return JsonHelper.Serialize(ret);

                }
                //更新用户登录时间
                System.Threading.Thread th = new System.Threading.Thread(UpdateLastLoginTime);
                th.Start(user.U_Id);
                //缓存用户信息
                CachedUserInfo(user.U_UserName);

                Session[SessionHelper.SessionKey_User_UserName] = user.U_UserName;
                Session[SessionHelper.SessionKey_User_UserID] = user.U_Id;
                Response.Redirect("../index.aspx", false);
                ret.Add(1, "登录成功");
                return JsonHelper.Serialize(ret);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="name"></param>
        /// <param name="pass"></param>
        private void Action_Login(string name, string pass)
        {
            //ScriptManager.RegisterClientScriptBlock(this.Page,this.GetType(), "error", "alert('用户名不存在');", true);
            try
            {
                UserRepository biz = new UserRepository();
                var user = biz.GetUserinfoByLogin(name);
                if (user == null)
                {
                    Response.Write("<script>alert('该用户名不存在')</script>");
                    Response.Redirect("../login.html", false);
                    return;
                }

                string password = BCrypt.Net.BCrypt.HashPassword(pass, user.U_Salt);
                if (password != user.U_Password)
                {
                    Response.Write("<script>alert('用户名或密码错误')</script>");
                    Response.Redirect("../login.html", false);
                    return;
                }
                //更新用户登录时间
                System.Threading.Thread th = new System.Threading.Thread(UpdateLastLoginTime);
                th.Start(user.U_Id);
                //缓存用户信息
                CachedUserInfo(user.U_UserName);

                Session[SessionHelper.SessionKey_User_UserName] = user.U_UserName;
                Session[SessionHelper.SessionKey_User_UserID] = user.U_Id;
                var returnurl = Request.Params["returnurl"];
                if (!string.IsNullOrEmpty(returnurl))
                    Response.Redirect(Server.UrlDecode(returnurl), false);
                else
                    Response.Redirect("../index.aspx", false);
                return;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 更新用户登录时间
        /// </summary>
        /// <param name="id"></param>
        private void UpdateLastLoginTime(object id)
        {
            try
            {
                UserRepository biz = new UserRepository();
                biz.UpdateUserLoginTime((long)id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 缓存用户信息
        /// </summary>
        /// <param name="id"></param>
        private void CachedUserInfo(string name)
        {
            try
            {
                CacheRepository biz = new CacheRepository();
                biz.CachedUserInfoByName((string)name);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region 退出
        /// <summary>
        /// 退出
        /// </summary>
        private void loginout()
        {
            //Session[SessionHelper.SessionKey_User_UserName] = null;
            //Session[SessionHelper.SessionKey_User_PhoneNumber] = null;
            Session.Abandon();
            Response.Redirect("../index.aspx");
            return;
        }
        #endregion

        #region 注册
        private void register()
        {
            try
            {
                UserInfo uinfo = new UserInfo();
                uinfo.U_UserName = Request["username"];
                uinfo.U_Salt = BCrypt.Net.BCrypt.GenerateSalt();
                uinfo.U_Password = BCrypt.Net.BCrypt.HashPassword(Request["password"], uinfo.U_Salt);
                uinfo.U_NickName = uinfo.U_UserName;
                UserRepository biz = new UserRepository();
                if (biz.AddUser(uinfo))
                {
                    Session[SessionHelper.SessionKey_User_UserName] = uinfo.U_UserName;
                    Response.Redirect("~/userCenter.aspx", false);
                    // return "1";
                }
                else
                {
                    Response.Redirect("~/reg.html", false);
                    // return "0";
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region 验证
        /// <summary>
        /// 验证用户名
        /// </summary>
        /// <param name="strUserName"></param>
        /// <returns>1：存在   0：不存在</returns>
        private string VaildateUsername(string strUserName)
        {
            try
            {
                UserRepository biz = new UserRepository();
                var user = biz.GetUserinfoByLogin(strUserName);
                if (user != null)
                {
                    return "1";
                }
                return "0";
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// 验证密码
        /// </summary>
        /// <param name="strPass"></param>
        /// <returns>1：正确   0：错误  2：不存在</returns>
        private string VaildatePassword(string strUserName, string password)
        {
            try
            {
                UserRepository biz = new UserRepository();
                var user = biz.GetUserinfoByLogin(strUserName);
                if (user == null)
                {
                    return "2";
                }
                if (user.U_Password != BCrypt.Net.BCrypt.HashPassword(password, user.U_Salt)) return "0";
                else
                {
                    return "1";
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region 号码运势测试
        public string NumberFortune(string num)
        {
            try
            {
                PhoneModel model = new PhoneModel();
                model = YZ.Common.PhoneLuck.Instance.LuckTest(num);
                Dictionary<string, string> dic = new Dictionary<string, string>();
                dic.Add("Explain1", model.Explain);
                dic.Add("Mark1", model.Mark);

                model = new PhoneModel();
                model = PhoneLuck.Instance.CharacterTest(num);
                dic.Add("Explain2", model.Explain);
                dic.Add("Mark2", model.Mark);

                return JsonHelper.Serialize(dic);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

    }
}