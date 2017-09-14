using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Web.Util;
using YZ.Biz;
using YZ.Common.Log;

namespace YZ.Web.Asp
{
    public partial class userInfo : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            base.Page_Load(sender, e);
            try
            {
                string action = Request["action"];
                if (!string.IsNullOrEmpty(action))
                {
                    string result = string.Empty;
                    if (action == "LoadInfo")
                    {
                        result = LoadInfo();
                    }
                    else if (action == "Add")
                    {
                        result = Add();
                    }
                    Response.Clear();
                    Response.ClearContent();
                    Response.ClearHeaders();
                    Response.Write(result);
                    //HttpContext.Current.ApplicationInstance.CompleteRequest();
                    Response.End();

                }
            }
            catch (Exception ex)
            {
                if (ex is System.Threading.ThreadAbortException) { }
                //System.Threading.Thread.ResetAbort();
                else
                    LogHelper.Fatal("UserInfo-Page_Load",ex.Message,ex);
            }
        }

        /// <summary>
        /// 加载个人信息
        /// </summary>
        /// <returns></returns>
        private string LoadInfo()
        {
            try
            {
                UserRepository biz = new UserRepository();
                UserInfo userinfo = biz.GetUserinfoByUserName(UserName);

                if (userinfo == null)
                {
                    return "0";
                }

                UserDetail userDetail = biz.GetUserDetailByUserId(userinfo.U_Id);
                Dictionary<string, string> dic = new Dictionary<string, string>();
                dic["NickName"] = userinfo.U_NickName;
                dic["RealName"] = userinfo.U_RealName;
                dic["Email"] = userinfo.U_Email; ;

                if (userDetail != null)
                {
                    dic["Address"] = userDetail.UD_Address;
                    dic["Age"] = userDetail.UD_Age;
                    if (userDetail.UD_Birth != null)
                        dic["Birth"] = userDetail.UD_Birth.ToString();
                    dic["BooldType"] = userDetail.UD_BooldType;
                    dic["Cale"] = userDetail.UD_Cale;
                    dic["Company"] = userDetail.UD_Company;
                    dic["Const"] = userDetail.UD_Const;
                    dic["ContactWay"] = userDetail.UD_ContactWay;
                    dic["Education"] = userDetail.UD_Education;
                    dic["EmotState"] = userDetail.UD_EmotState;
                    dic["HomeTown"] = userDetail.UD_HomeTown;
                    dic["NowPlace"] = userDetail.UD_NowPlace;
                    dic["PaperType"] = userDetail.UD_PaperType;
                    dic["PaperNumber"] = userDetail.UD_PaperNumber;
                    dic["Remark"] = userDetail.UD_Remark;
                    dic["School"] = userDetail.UD_School;
                    dic["Sex"] = userDetail.UD_Sex;
                    dic["Worker"] = userDetail.UD_Worker;

                }

                string jsonStr = GetJsonStr(dic);
                return jsonStr;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private static string GetJsonStr(Dictionary<string, string> dic)
        {
            StringBuilder sb = new StringBuilder();
            if (dic.Count == 0)
            {
                return null;
            }
            sb.Append("{");
            int i = 1;
            foreach (var item in dic)
            {
                if (i < dic.Count)
                {
                    sb.Append("\"" + item.Key + "\":\"" + item.Value + "\",");
                }
                else
                {
                    sb.Append("\"" + item.Key + "\":\"" + item.Value + "\"");
                }

                i++;
            }

            sb.Append("}");
            return sb.ToString();
        }

        /// <summary>
        /// 添加个人信息
        /// </summary>
        /// <param name="address"></param>
        /// <param name="position"></param>
        /// <param name="graduationSchool"></param>
        /// <param name="education"></param>
        /// <param name="companyIndustry"></param>
        /// <param name="companySize"></param>
        /// <param name="monthIncome"></param>
        /// <param name="marry"></param>
        /// <param name="haveHouse"></param>
        /// <param name="haveCar"></param>
        /// <returns></returns>

        private string Add()
        {
            try
            {
                UserRepository biz = new UserRepository();
                if (UserName == null)
                {
                    return "0";
                }
                UserInfo userinfo = biz.GetUserinfoByUserName(UserName);
                if (userinfo == null)
                {
                    return "0";
                }
                //if (address.Length > 100 || position.Length > 20 || graduationSchool.Length > 50 || education.Length > 50 ||
                //    companyIndustry.Length > 50 || companySize.Length > 50 || monthIncome.Length > 50 || marry.Length > 50 ||
                //    haveCar.Length > 50 || haveHouse.Length > 50)
                //{
                //    return "0";
                //}

                userinfo.U_NickName = HttpContext.Current.Request["NickName"];
                userinfo.U_RealName = HttpContext.Current.Request["RealName"];
                userinfo.U_Email = HttpContext.Current.Request["Email"];

                UserDetail addModel = biz.GetUserDetailByUserId(UserID);
                bool flg = true;
                if (addModel == null)
                {
                    addModel = new UserDetail();
                    flg = false;
                }

                addModel.U_Id = UserID;
                addModel.UD_Sex = HttpContext.Current.Request["Sex"];
                addModel.UD_Age = HttpContext.Current.Request["Age"];
                DateTime datetime;
                if (DateTime.TryParse(HttpContext.Current.Request["Birth"], out datetime))
                {
                    addModel.UD_Birth = datetime;
                }
                addModel.UD_Cale = HttpContext.Current.Request["Cale"];
                addModel.UD_Const = HttpContext.Current.Request["Const"];
                addModel.UD_BooldType = HttpContext.Current.Request["BooldType"];
                addModel.UD_EmotState = HttpContext.Current.Request["EmotState"];
                addModel.UD_ContactWay = HttpContext.Current.Request["ContactWay"];
                addModel.UD_Education = HttpContext.Current.Request["Education"];
                addModel.UD_School = HttpContext.Current.Request["School"];
                addModel.UD_PaperType = HttpContext.Current.Request["PaperType"];
                addModel.UD_PaperNumber = HttpContext.Current.Request["PaperNumber"];
                addModel.UD_Company = HttpContext.Current.Request["Company"];
                addModel.UD_Worker = HttpContext.Current.Request["Worker"];
                addModel.UD_HomeTown = HttpContext.Current.Request["HomeTown"];
                addModel.UD_NowPlace = HttpContext.Current.Request["NowPlace"];
                addModel.UD_Address = HttpContext.Current.Request["Address"];
                addModel.UD_Remark = HttpContext.Current.Request["Remark"];

                if (flg)
                {
                    if (biz.AddUserDetail(addModel))
                    {
                        return "1";
                    }
                }
                else
                {
                    if (biz.UpdateDataBase())
                    {
                        return "1";
                    }
                }

                return "0";
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}