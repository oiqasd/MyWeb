using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text.RegularExpressions;

namespace Web.Helper
{
    public class CommonHelper
    {
        #region 验证
        /// <summary>
        /// 只能是字母和汉字
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool IsChinaOrEnglish(string str)
        {
            const string reg = "^[\u4E00-\u9FA5A-Za-z]+$";
            if (string.IsNullOrEmpty(str))
            {
                return false;
            }
            Regex rg = new Regex(reg);
            if (!rg.IsMatch(str))
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// 只能是字母和数字
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool IsNumberOrEnglish(string str)
        {
            const string reg = "^[A-Za-z0-9]+$";
            if (string.IsNullOrEmpty(str))
            {
                return false;
            }
            Regex rg = new Regex(reg);
            if (!rg.IsMatch(str))
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// 验证密码只能由数字，字母以及下划线组成
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool IsPassword(string str)
        {
            const string reg = "^(?![0-9]+$)(?![a-zA-Z]+$)[0-9A-Za-z]{6,20}$";
            if (string.IsNullOrEmpty(str))
            {
                return false;
            }
            return Regex.IsMatch(str, reg);
        }

        /// <summary>
        /// 验证手机号码
        /// </summarcheck mobliey>
        /// <param name="mobile">手机号</param>
        /// <returns></returns>
        public static bool IsMobile(string mobile)
        {
            if (string.IsNullOrEmpty(mobile))
            {
                return false;
            }
            //判断手机号格式是否正确
            const string patternMobile1 = @"^1\d{10}$";// /(^0{0,1}1[3|4|5|6|7|8|9][0-9]{9}$)/; 
            const string patternMobile2 = @"^1[35678]\d{9}$";
            Regex regex1 = new Regex(patternMobile1);
            Regex regex2 = new Regex(patternMobile2);
            if (!regex1.IsMatch(mobile) && !regex2.IsMatch(mobile))
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// 验证身份证
        /// </summary>
        /// <param name="cardId"></param>
        /// <returns></returns>
        public static bool IsIdCard(string cardId)
        {
            if (cardId.Length == 18)
            {
                bool check = IsIDCard18(cardId);
                return check;
            }
            else if (cardId.Length == 15)
            {
                bool check = IsIDCard15(cardId);
                return check;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 验证十八位身份证号
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        private static bool IsIDCard18(string Id)
        {
            long n = 0;
            if (long.TryParse(Id.Remove(17), out n) == false || n < Math.Pow(10, 16) || long.TryParse(Id.Replace('x', '0').Replace('X', '0'), out n) == false)
            {
                return false;//数字验证
            }
            string address = "11x22x35x44x53x12x23x36x45x54x13x31x37x46x61x14x32x41x50x62x15x33x42x51x63x21x34x43x52x64x65x71x81x82x91";
            if (address.IndexOf(Id.Remove(2)) == -1)
            {
                return false;//省份验证
            }
            string birth = Id.Substring(6, 8).Insert(6, "-").Insert(4, "-");
            DateTime time = new DateTime();
            if (DateTime.TryParse(birth, out time) == false)
            {
                return false;//生日验证
            }
            string[] arrVarifyCode = ("1,0,x,9,8,7,6,5,4,3,2").Split(',');
            string[] Wi = ("7,9,10,5,8,4,2,1,6,3,7,9,10,5,8,4,2").Split(',');
            char[] Ai = Id.Remove(17).ToCharArray();
            int sum = 0;
            for (int i = 0; i < 17; i++)
            {
                sum += int.Parse(Wi[i]) * int.Parse(Ai[i].ToString());
            }
            int y = -1;
            Math.DivRem(sum, 11, out y);
            if (arrVarifyCode[y] != Id.Substring(17, 1).ToLower())
            {
                return false;//校验码验证
            }
            return true;//符合GB11643-1999标准
        }

        /// <summary>
        /// 验证十五位身份证号码
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        private static bool IsIDCard15(string Id)
        {
            long n = 0;
            if (long.TryParse(Id, out n) == false || n < Math.Pow(10, 14))
            {
                return false;//数字验证
            }
            string address = "11x22x35x44x53x12x23x36x45x54x13x31x37x46x61x14x32x41x50x62x15x33x42x51x63x21x34x43x52x64x65x71x81x82x91";
            if (address.IndexOf(Id.Remove(2)) == -1)
            {
                return false;//省份验证
            }
            string birth = Id.Substring(6, 6).Insert(4, "-").Insert(2, "-");
            DateTime time = new DateTime();
            if (DateTime.TryParse(birth, out time) == false)
            {
                return false;//生日验证
            }
            return true;//符合15位身份证标准
        }

        /// <summary>
        /// 验证是否是有效邮箱地址
        /// </summary>
        /// <param name="strln">输入的字符</param>
        /// <returns></returns>
        public static bool IsEmail(string strln)
        {
            return Regex.IsMatch(strln, @"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$");
        }

        /// <summary>
        /// 验证是否只含有汉字
        /// </summary>
        /// <param name="strln">输入的字符</param>
        /// <returns></returns>
        public static bool IsOnllyChinese(string strln)
        {
            return Regex.IsMatch(strln, @"^[\u4e00-\u9fa5]+$");
        }

        /// <summary>
        /// 验证输入字符串为数字
        /// </summary>
        /// <param name="strln">输入字符</param>
        /// <returns>返回一个bool类型的值</returns>
        public static bool IsNumber(string strln)
        {
            return Regex.IsMatch(strln, "^([0]|([1-9]+\\d{0,}?))(.[\\d]+)?$");
        }

        #endregion

        #region 得到IP地址
        /// <summary>
        /// 得到IP地址
        /// </summary>
        /// <returns></returns>
        public static  string GetSindId()
        {
            HttpRequest request = HttpContext.Current.Request;
            string result = request.ServerVariables["HTTP_X_FORWARDED_FOR"];
            if (string.IsNullOrEmpty(result))
            { result = request.ServerVariables["REMOTE_ADDR"]; }
            if (string.IsNullOrEmpty(result)) { result = request.UserHostAddress; }
            return result;
        }
        #endregion

       
    }
}