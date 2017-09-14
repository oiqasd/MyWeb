using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using YZ.Common.Util;

namespace YZ.Common
{
    public static class ValidateHelper
    {
        /// <summary>  
        /// 验证非空   
        /// </summary>  
        /// <param name="value">要验证的字符串</param>  
        /// <returns>true - 非空，false - 空</returns>  
        public static bool NotEmpty(string value)
        {
            return !string.IsNullOrEmpty(value) && value.Trim().Length > 0;
        }

        /// <summary>  
        /// 验证URL   
        /// </summary>  
        /// <param name="url">url地址</param>  
        /// <returns>true - 正确，false - 不正确</returns>  
        public static bool IsRightUrl(string url)
        {
            return Regex.IsMatch(url, "http(s)?://([//w-]+//.)+[//w-]+(//[w-.//?%&=]*)?");
        }

        /// <summary>  
        /// 验证IP  
        /// <para/>Author : AnDequan  
        /// <para/>Date   : 2010-12-23  
        /// </summary>  
        /// <param name="ip">ip地址</param>  
        /// <returns>true - IP正确，false - 不正确 </returns>  
        public static bool IsRightIP(string ip)
        {
            string[] sLines = new string[4];
            sLines = ip.Split('.');
            foreach (string item in sLines)
            {
                if (Regex.IsMatch(item, @"/d*"))
                {
                    if (Convert.ToInt32(item) >= 255)
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
            return true;
        }

        /// <summary>  
        /// 判断数字  
        /// </summary>  
        /// <param name="value">要验证的值</param>  
        /// <returns>true - 是数字，否则反之</returns>   
        public static bool IsNumber(string value)
        {

            //Regex r = new Regex(@"^[+-]?/d+(/.)?/d*$");
            return Regex.IsMatch(value, "^[0-9]*$");
        }
        /// <summary>  
        /// 判断带小数点  
        /// </summary>  
        /// <param name="value">要验证的值</param>  
        /// <returns>true - 带小数点，否则反之</returns>  
        public static bool IsWithPoint(string value)
        {
            return Regex.IsMatch(value, "^[0-9]+(.[0-9]{2})?$");
        }

        /// <summary>  
        /// 判断是否英文大写  
        /// </summary>  
        /// <param name="value">验证字符串</param>  
        /// <returns>true - 是,false - 否</returns>  
        public static bool IsEnglishUpper(string value)
        {
            return Regex.IsMatch(value, "^[A-Z]+$");
        }

        /// <summary>  
        /// 判断是否英文  
        /// </summary>  
        /// <param name="value">验证字符串</param>  
        /// <returns></returns>  
        public static bool IsEnglish(string value)
        {
            return Regex.IsMatch(value, "^[A-Za-z]+$");
        }

        /// <summary>  
        /// 判断邮箱地址  
        /// </summary>  
        /// <param name="sValue"></param>  
        /// <returns></returns>  
        public static bool IsEmail(string sValue)
        {

            return Regex.IsMatch(sValue, @"^/w+([-+.]/w+)*@/w+([-.]/w+)*/./w+([-.]/w+)*$");
        }

        /// <summary>  
        /// 判断电话号码  
        /// </summary>  
        /// <param name="sValue"></param>  
        /// <returns></returns>  
        public static bool IsTelePhone(string sValue)
        {
            return Regex.IsMatch(sValue, @"(^(/d{3,4}-)?/d{7,8})$|(13|15|18[0-9]{9})");
        }

        /// <summary>  
        /// 判断网站地址  
        /// </summary>  
        /// <param name="sValue"></param>  
        /// <returns></returns>  
        public static bool IsInternalAddress(string sValue)
        {
            return Regex.IsMatch(sValue, @"^http://([/w-]+/.)+[/w-]+(/[/w-./?%&=]*)?$");
        }

        /// <summary>
        /// 检查时间戳是否过期
        /// </summary>
        /// <returns></returns>
        public static bool CheckTimeStamp(long timeStamp)
        {
            long nowTimeStamp = DateTimeHelper.GetTimeStamp();
            if (Math.Abs(nowTimeStamp - timeStamp) > 3000)
                return false;
            return true;
        }

        /// <summary>
        /// 验证身份证号
        /// </summary>
        /// <param name="cardno"></param>
        /// <returns></returns>
        public static bool CheckIDNumber(string cardno)
        {
            string str = @"^\d+$";
            Regex reg = new Regex(str);

            //身份证在10-18位
            if (cardno.Length > 10 && cardno.Length <= 18)
            {
                //截取字符串
                string subCardNo = cardno.Substring(0, cardno.Length - 1);
                //身份证全部是数字或最后一位是X
                if (reg.IsMatch(cardno) || (reg.IsMatch(subCardNo) && cardno.ToUpper().EndsWith("X")))
                    return true;
                else
                    return false;
            }
            else
                return false;
        }

        /// <summary>
        /// 检查身份证
        /// </summary>
        /// <param name="idcard"></param>
        /// <returns></returns>
        public static bool CheckIDCard(string idcard)
        {
            bool flag = false;

            if (idcard.Length == 18)
            {
                flag = CheckIDCard18(idcard);
            }
            else if (idcard.Length == 15)
            {
                flag = CheckIDCard15(idcard);
            }

            return flag;
        }

        /// <summary>
        /// 验证18位的身份证
        /// </summary>
        /// <param name="idcard"></param>
        /// <returns></returns>
        private static bool CheckIDCard18(string idcard)
        {
            long n = 0;
            if (long.TryParse(idcard.Remove(17), out n) == false || n < Math.Pow(10, 16) || long.TryParse(idcard.Replace('x', '0').Replace('X', '0'), out n) == false)
            {
                return false;//数字验证
            }

            string address = "11x22x35x44x53x12x23x36x45x54x13x31x37x46x61x14x32x41x50x62x15x33x42x51x63x21x34x43x52x64x65x71x81x82x91";
            if (address.IndexOf(idcard.Remove(2)) == -1)
            {
                return false;//省份验证
            }

            string birth = idcard.Substring(6, 8).Insert(6, "-").Insert(4, "-");
            DateTime time = new DateTime();
            if (DateTime.TryParse(birth, out time) == false)
            {
                return false;//生日验证
            }

            string[] arrVarifyCode = ("1,0,x,9,8,7,6,5,4,3,2").Split(',');
            string[] Wi = ("7,9,10,5,8,4,2,1,6,3,7,9,10,5,8,4,2").Split(',');
            char[] Ai = idcard.Remove(17).ToCharArray();

            int sum = 0;
            for (int i = 0; i < 17; i++)
            {
                sum += int.Parse(Wi[i]) * int.Parse(Ai[i].ToString());
            }

            int y = -1;
            Math.DivRem(sum, 11, out y);
            if (arrVarifyCode[y] != idcard.Substring(17, 1).ToLower())
            {
                return false;//校验码验证
            }

            return true;//符合GB11643-1999标准
        }

        /// <summary>
        /// 验证15位的身份证
        /// </summary>
        /// <param name="idcard"></param>
        /// <returns></returns>
        private static bool CheckIDCard15(string idcard)
        {
            long n = 0;
            if (long.TryParse(idcard, out n) == false || n < Math.Pow(10, 14))
            {
                return false;//数字验证
            }

            string address = "11x22x35x44x53x12x23x36x45x54x13x31x37x46x61x14x32x41x50x62x15x33x42x51x63x21x34x43x52x64x65x71x81x82x91";
            if (address.IndexOf(idcard.Remove(2)) == -1)
            {
                return false;//省份验证
            }

            string birth = idcard.Substring(6, 6).Insert(4, "-").Insert(2, "-");
            DateTime time = new DateTime();
            if (DateTime.TryParse(birth, out time) == false)
            {
                return false;//生日验证
            }

            return true;//符合15位身份证标准
        }

        /// <summary>
        /// 验证手机号+电话好卡
        /// </summary>
        /// <param name="mobile">号码</param>
        /// <returns></returns>
        public static bool CheckMobil(string mobile)
        {
            string str = @"^1[\d]{10}$";
            string str1 = @"^([0-9]{3,4}-)?[0-9]{7,8}$";
            Regex reg = new Regex(str);
            Regex regtel = new Regex(str1);

            //手机号全是数字并以1开头 电话号码 XXXX-XXXXXXXX
            if (reg.IsMatch(mobile) || regtel.IsMatch(mobile))
                return true;
            else
                return false;
        }

        /// <summary>
        /// 检查手机验证码
        /// 数字验证码
        /// </summary>
        /// <param name="vercode"></param>
        public static bool CheckMobileVerCode(string vercode)
        {
            bool flag = true;
            if (vercode.Length == 6)
            {
                //验证数字
                char[] code = vercode.ToCharArray();
                for (int i = 0; i < code.Length; i++)
                {
                    if (!Char.IsNumber(code[i]))
                    {
                        flag = false;
                        break;
                    }
                }
            }
            else
                flag = false;
            return flag;
        }

        /// <summary>
        /// 验证密码的正确性
        /// 密码长度6-16位,不能输入除字母和数字以外的字符
        /// </summary>
        public static bool CheckPwd(string pwd, out string strouput)
        {
            strouput = string.Empty;
            bool flag = true;
            if (string.IsNullOrEmpty(pwd))
            {
                flag = false;
                strouput = "密码不能为空";
            }
            else
            {

                Regex regex = new Regex("^(?=.*?[a-zA-Z])(?=.*?[0-9])[a-zA-Z0-9]{6,16}$");
                if (!regex.IsMatch(pwd))
                {
                    flag = false;
                    strouput = "密码长度6-16位,不能输入除字母和数字以外的字符";
                }
            }

            return flag;
        }
    }
}
