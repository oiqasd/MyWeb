<%@ WebHandler Language="C#" Class="Users" %>

using System.Web;
using YZ.Biz;

public class Users : IHttpHandler
{

    public void ProcessRequest(HttpContext context)
    {
        context.Response.ContentType = "text/plain";

        string abc = context.Request.Params["abc"];

        switch (abc)
        {
            case "ExistsEmail":
                ExistsEmail(context);
                break;
            case "ExistsUserName":
                ExistsUserName(context);
                break;
            case "ExistsMobile":
                ExistsMobile(context);
                break;
        }

    }

    //验证邮箱是否存在
    private void ExistsEmail(HttpContext context)
    {

        string email = context.Request.Params["Email"];

        if (!string.IsNullOrEmpty(email))
        {
            UserRepository biz = new UserRepository();
            context.Response.Write(biz.EmailExists(email));
        }
        else
        {
            context.Response.Write(true.ToString());
        }

    }

    /// <summary>
    /// 判断用户名是否存在
    /// </summary>
    /// <param name="context">true 存在</param>
    private void ExistsUserName(HttpContext context)
    {
        string userName = context.Request.Form["UserName"];

        if (string.IsNullOrEmpty(context.Request.Form["UserName"]))
        {
            context.Response.Write(true.ToString());
        }
        else
        {
            UserRepository biz = new UserRepository();
            context.Response.Write(biz.UserNameExists(userName));
        }
    }

    /// <summary>
    /// 判断手机号是否存在
    /// </summary>
    /// <param name="context"></param>
    private void ExistsMobile(HttpContext context)
    {
        string moblie = context.Request.Form["moblie"];

        if (string.IsNullOrEmpty(context.Request.Form["moblie"]))
        {
            context.Response.Write(true.ToString());
        }
        else
        {
            UserRepository biz = new UserRepository();
            context.Response.Write(biz.PhoneNumberExists(moblie));
        }
    }

    ///// <summary>
    ///// 判断安全问题是否存在
    ///// </summary>
    ///// <param name="context"></param>
    //private void ExistsQuestion(HttpContext context)
    //{
    //    string email = context.Request.Form["Email"];

    //    if (string.IsNullOrEmpty(context.Request.Form["Email"]))
    //    {
    //        context.Response.Write(true.ToString());
    //    }
    //    else
    //    {
    //        B_userInfo bll = new B_userInfo();
    //        context.Response.Write(bll.ExistsQuestion(email));
    //    }
    //}

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }

}