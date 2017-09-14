using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.Helper
{
    public static class SessionHelper
    {
        /// <summary>
        /// 注册key
        /// </summary>
        public static string P_RegisterKey = "P_Register_KEY";

        /// <summary>
        /// 验证码的Key
        /// </summary>
        public static string P_ValidCodeKey = "P_ValidCode_KEY";

        /// <summary>
        /// 手机验证码失效时间Key
        /// </summary>
        public static string P_CaptchaFailedKey = "P_CaptchaFailed_KEY";

        /// <summary>
        /// 用户信息key
        /// </summary>
        public static string P_UserInfoKey = "P_UserInfo_KEY";

        /// <summary>
        /// 当前登录用户的用户名
        /// </summary>
        public const string SessionKey_User_UserName = "SessionKey_User_UserName";

        /// <summary>
        /// 当前登录用户的手机号
        /// </summary>
        public const string SessionKey_User_PhoneNumber = "SessionKey_User_PhoneNumber";

        /// <summary>
        /// 当前登录用户的用户ID
        /// </summary>
        public const string SessionKey_User_UserID = "SessionKey_User_UserID";

        /// <summary>
        /// 当前登录用户的Email
        /// </summary>
        public const string SessionKey_User_Email = "SessionKey_User_Email";

        //===========================后台======================================
        /// <summary>
        /// 当前后台登录用户的用户名
        /// </summary>
        public const string SessionKey_Employee_EmployeeName = "SessionKey_Employee_EmployeeName";

        /// <summary>
        /// 当前后台登录用户的用户ID
        /// </summary>
        public const string SessionKey_Employee_EmployeeID = "SessionKey_Employee_EmployeeID";

        /// <summary>
        /// 当前登录后台用户的手机号
        /// </summary>
        public const string SessionKey_Employee_PhoneNumber = "SessionKey_Employee_PhoneNumber";

        /// <summary>
        /// 当前后台用户
        /// </summary>
        public const string CurrentAdminUser = "Admin_CurrentEmployee";


        /// <summary>
        ///当前上传大图片的名称
        /// </summary>
        public const string SessionKey_Image_BigIamge = "SessionKey_Image_BigIamge";

        /// <summary>
        ///当前上传小图片的名称
        /// </summary>
        public const string SessionKey_Image_SmallIamge = "SessionKey_Image_SmallIamge";

    }
}