using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Web; 

namespace YZ.Common.Util
{
    public static class StringHelper
    {
        public static string ConvertToBase64Str(string str)
        {
            str = Regex.Unescape(str);
            return str.Replace("\r", "")
                            .Replace("\n", "")
                            .Replace(" ", "+");
        }
        /// <summary>
        /// 提取首字母,只能取到常用的一级汉字
        /// </summary>
        /// <param name="strText">需要转换的字符串</param>
        /// <returns>转换结果</returns>
        public static string GetFirstSpell(string words)
        {
            int len = words.Length;
            string myStr = "";
            for (int i = 0; i < len; i++)
            {
                myStr += GetOneSpell(words.Substring(i, 1));
            }
            return myStr;
        }

        /// <summary>
        /// 获取单个汉字的首拼音
        /// </summary>
        /// <param name="myChar">需要转换的字符</param>
        /// <returns>转换结果</returns>
        private static string GetOneSpell(string myChar)
        {
            byte[] arrCN = System.Text.Encoding.Default.GetBytes(myChar);
            if (arrCN.Length > 1)
            {
                int area = (short)arrCN[0];
                int pos = (short)arrCN[1];
                int code = (area << 8) + pos;
                int[] areacode = { 45217, 45253, 45761, 46318, 46826, 47010, 47297, 47614, 48119, 48119, 49062, 49324, 49896, 50371, 50614, 50622, 50906, 51387, 51446, 52218, 52698, 52698, 52698, 52980, 53689, 54481 };
                for (int i = 0; i < 26; i++)
                {
                    int max = 55290;
                    if (i != 25) max = areacode[i + 1];
                    if (areacode[i] <= code && code < max)
                    {
                        return System.Text.Encoding.Default.GetString(new byte[] { (byte)(65 + i) });
                    }
                }
                return "";
            }
            else return myChar;
        }

        #region Common

        /// <summary>
        /// 判断字符为空
        /// </summary>
        /// <param name="value">输入字符串</param>
        /// <returns>空为True</returns>
        public static bool IsEmpty(string value)
        {
            return ((value == null) || (value.Length == 0));
        }

        /// <summary>
        /// 判断字符不为空
        /// </summary>
        /// <param name="value">输入字符串</param>
        /// <returns>不为空为True</returns>
        public static bool IsNotEmpty(string value)
        {
            return (IsEmpty(value) == false);
        }

        /// <summary>
        /// 判断字符为空时返回默认值
        /// </summary>
        /// <param name = "value">输入字符串</param>
        /// <param name = "defaultValue">默认字符串</param>
        /// <returns>为空时返回默认字符串</returns>
        public static string IfEmpty(string value, string defaultValue)
        {
            return (IsNotEmpty(value) ? value : defaultValue);
        }

        /// <summary>
        /// 格式的参数值使用字符串格式
        /// </summary>
        /// <param name = "value">输入字符串</param>
        /// <param name = "parameters">参数</param>
        /// <returns>格式化后的字符串</returns>
        public static string FormatWith(string value, params object[] parameters)
        {
            return string.Format(value, parameters);
        }

        /// <summary>
        /// 截取指定最大长度前的字符串
        /// </summary>
        /// <param name = "value">输入字符串</param>
        /// <param name = "maxLength">最大长度</param>
        /// <returns>截取后的字符串</returns>
        public static string TrimToMaxLength(string value, int maxLength)
        {
            return (value == null || value.Length <= maxLength ? value : value.Substring(0, maxLength));
        }

        /// <summary>
        /// 截取指定最大长度前的字符串后追加字符
        /// </summary>
        /// <param name = "value">输入字符串</param>
        /// <param name = "maxLength">最大长度</param>
        /// <param name = "suffix">追加的字符</param>
        /// <returns>截取指定最大长度前的字符串后追加字符后的字符</returns>
        public static string TrimToMaxLength(string value, int maxLength, string suffix)
        {
            return (value == null || value.Length <= maxLength ? value : string.Concat(value.Substring(0, maxLength), suffix));
        }

        /// <summary>
        /// 判断字符串是否包含在输入值的字符串中
        /// </summary>
        /// <param name = "inputValue">输入字符串</param>
        /// <param name = "comparisonValue">比较的字符串</param>
        /// <param name = "comparisonType">比较类型</param>
        /// <returns>
        /// 	<c>true</c> 如果输入值包含指定的值,否则, <c>false</c>.
        /// </returns>
        public static bool Contains(string inputValue, string comparisonValue, StringComparison comparisonType)
        {
            return (inputValue.IndexOf(comparisonValue, comparisonType) != -1);
        }

        /// <summary>
        /// 判断字符串是否不包含在输入值的字符串中
        /// </summary>
        /// <param name = "inputValue">输入字符串</param>
        /// <param name = "comparisonValue">比较的字符串</param>
        /// <returns>
        /// 	<c>true</c> 如果输入值包含指定的值(不区分大小写),否则, <c>false</c>.
        /// </returns>
        public static bool ContainsEquivalenceTo(string inputValue, string comparisonValue)
        {
            return BothStringsAreEmpty(inputValue, comparisonValue) || StringContainsEquivalence(inputValue, comparisonValue);
        }

        /// <summary>
        /// 在输入的字符串前后加指定个数的字节
        /// </summary>
        /// <param name="value">输入字符串</param>
        /// <param name="width">加字节个数</param>
        /// <param name="padChar">字节</param>
        /// <param name="truncate">超出长度时是否截取</param>
        /// <returns></returns>
        public static string PadBoth(string value, int width, char padChar, bool truncate = false)
        {
            int diff = width - value.Length;
            if (diff == 0 || diff < 0 && !(truncate))
            {
                return value;
            }
            else if (diff < 0)
            {
                return value.Substring(0, width);
            }
            else
            {
                return value.PadLeft(width - diff / 2, padChar).PadRight(width, padChar);
            }
        }

        /// <summary>
        /// 反转字符串
        /// </summary>
        /// <param name = "value">待反转字符串</param>
        /// <returns>已反转的字符串</returns>
        public static string Reverse(string value)
        {
            if (IsEmpty(value) || (value.Length == 1))
                return value;

            var chars = value.ToCharArray();
            Array.Reverse(chars);
            return new string(chars);
        }

        /// <summary>
        /// 确保一个字符串的指定的前缀
        /// </summary>
        /// <param name = "value">输入字符串</param>
        /// <param name = "prefix">指定的前缀</param>
        /// <returns>包含前缀的字符串</returns>
        /// <example>
        /// 	<code>
        /// 		var extension = "txt";
        /// 		var fileName = string.Concat(file.Name, extension.EnsureStartsWith("."));
        /// 	</code>
        /// </example>
        public static string EnsureStartsWith(string value, string prefix)
        {
            return value.StartsWith(prefix) ? value : string.Concat(prefix, value);
        }

        /// <summary>
        /// 确保一个字符串的指定的后缀
        /// </summary>
        /// <param name = "value">输入字符串</param>
        /// <param name = "prefix">指定的后缀</param>
        /// <returns>包含后缀的字符串</returns>
        /// <example>
        /// 	<code>
        /// 		var url = "http://www.pgk.de";
        /// 		url = url.EnsureEndsWith("/"));
        /// 	</code>
        /// </example>
        public static string EnsureEndsWith(string value, string suffix)
        {
            return value.EndsWith(suffix) ? value : string.Concat(value, suffix);
        }

        /// <summary>
        /// 重复指定的字符串值所提供的重复次数
        /// </summary>
        /// <param name = "value">输入字符串</param>
        /// <param name = "repeatCount">重复次数</param>
        /// <returns>重复后的字符串</returns>
        public static string Repeat(string value, int repeatCount)
        {
            if (value.Length == 1)
                return new string(value[0], repeatCount);

            var sb = new StringBuilder(repeatCount * value.Length);
            while (repeatCount-- > 0)
                sb.Append(value);
            return sb.ToString();
        }

        /// <summary>
        /// 判断字符串是不是一个数值型字符串
        /// </summary>
        /// <param name = "value">输入字符串</param>
        /// <returns>
        ///true为是数值型字符串
        /// </returns>
        public static bool IsNumeric(string value)
        {
            float output;
            return float.TryParse(value, out output);
        }

        /// <summary>
        /// 抓取字符串中所有的数字
        /// </summary>
        /// <param name = "value">输入字符串</param>
        /// <returns>
        /// 字符串中所有的数字
        /// </returns>
        public static string ExtractDigits(string value)
        {
            return value.Where(Char.IsDigit).Aggregate(new StringBuilder(value.Length), (sb, c) => sb.Append(c)).ToString();
        }

        /// <summary>
        /// 拼接字符串
        /// </summary>
        /// <param name = "value">输入字符串</param>
        /// <param name = "values">拼接的字符串数组</param>
        /// <returns>The concatenated string.</returns>
        public static string ConcatWith(string value, params string[] values)
        {
            return string.Concat(value, string.Concat(values));
        }

        /// <summary>
        /// 将提供的字符串到Guid值
        /// </summary>
        /// <param name = "value">输入字符串</param>
        /// <returns>The Guid</returns>
        public static Guid ToGuid(string value)
        {
            return new Guid(value);
        }

        /// <summary>
        /// 将提供的字符串到Guid值,如果是空则返回一个空的GUID默认值
        /// </summary>
        /// <param name = "value">输入字符串</param>
        /// <returns>The Guid</returns>
        public static Guid ToGuidSave(string value)
        {
            return ToGuidSave(value, Guid.Empty);
        }

        /// <summary>
        /// 将提供的字符串到Guid值,如果是空则返回一个空的GUID默认值
        /// </summary>
        /// <param name = "value">输入字符串</param>
        /// <param name = "defaultValue">默认值</param>
        /// <returns>The Guid</returns>
        public static Guid ToGuidSave(string value, Guid defaultValue)
        {
            if (IsEmpty(value))
                return defaultValue;

            try
            {
                return ToGuid(value);
            }
            catch { }

            return defaultValue;
        }

        /// <summary>
        /// 获取指定字符串前面的字符
        /// </summary>
        /// <param name = "value">输入字符串</param>
        /// <param name = "x">指定的字符串</param>
        /// <returns></returns>
        /// <remarks>Unlike GetBetween and GetAfter, this does not Trim the result.</remarks>
        public static string GetBefore(string value, string x)
        {
            var xPos = value.IndexOf(x);
            return xPos == -1 ? String.Empty : value.Substring(0, xPos);
        }

        /// <summary>
        /// 获取指定字符串之间的字符
        /// </summary>
        /// <param name = "value">输入字符串</param>
        /// <param name = "x">指定的前面的字符串</param>
        /// <param name = "y">指定的后面字符串</param>
        /// <returns></returns>
        /// <remarks>Unlike GetBefore, this method trims the result</remarks>
        public static string GetBetween(string value, string x, string y)
        {
            var xPos = value.IndexOf(x);
            var yPos = value.LastIndexOf(y);

            if (xPos == -1 || xPos == -1)
                return String.Empty;

            var startIndex = xPos + x.Length;
            return startIndex >= yPos ? String.Empty : value.Substring(startIndex, yPos - startIndex).Trim();
        }

        /// <summary>
        /// 获取指定字符串后面的字符
        /// </summary>
        /// <param name = "value">输入字符串</param>
        /// <param name = "x">指定的字符串</param>
        /// <returns></returns>
        /// <remarks>Unlike GetBefore, this method trims the result</remarks>
        public static string GetAfter(string value, string x)
        {
            var xPos = value.LastIndexOf(x);

            if (xPos == -1)
                return String.Empty;

            var startIndex = xPos + x.Length;
            return startIndex >= value.Length ? String.Empty : value.Substring(startIndex).Trim();
        }

        /// <summary>
        /// 字符串连接
        /// </summary>
        /// <typeparam name = "T">
        /// 	The type of the array to join
        /// </typeparam>
        /// <param name = "separator">分隔符</param>
        /// <param name = "value">
        /// 	An array of values
        /// </param>
        /// <returns>
        /// 	The join.
        /// </returns>
        public static string Join<T>(string separator, T[] value)
        {
            if (value == null || value.Length == 0)
                return string.Empty;
            if (separator == null)
                separator = string.Empty;
            Converter<T, string> converter = o => o.ToString();
            return string.Join(separator, Array.ConvertAll(value, converter));
        }

        /// <summary>
        /// 从字符串中删除任何给定字符
        /// </summary>
        /// <param name = "value">输入字符串</param>
        /// <param name = "removeCharc">删除字符</param>
        public static string Remove(string value, params char[] removeCharc)
        {
            var result = value;
            if (!string.IsNullOrEmpty(result) && removeCharc != null)
                Array.ForEach(removeCharc, c => result = Remove(result, c.ToString()));

            return result;
        }

        /// <summary>
        /// 从字符串中删除任何给定字符串
        /// </summary>
        /// <param name = "value">输入字符串</param>
        /// <param name="strings">删除字符串</param>
        /// <returns></returns>
        public static string Remove(string value, params string[] strings)
        {
            return strings.Aggregate(value, (current, c) => current.Replace(c, string.Empty));
        }

        /// <summary>
        /// 判断指定的字符串是否包含空,空的或只包含空白字符
        /// </summary>
        /// <param name="value">输入字符串</param>
        /// <returns>true,包含</returns>
        public static bool IsEmptyOrWhiteSpace(string value)
        {
            return (IsEmpty(value) || value.All(t => char.IsWhiteSpace(t)));
        }

        /// <summary>
        /// 判断指定的字符串是否不包含空,空的或只包含空白字符
        /// </summary>
        /// <param name="value">输入字符串</param>
        /// <returns>true,不包含</returns>
        public static bool IsNotEmptyOrWhiteSpace(string value)
        {
            return (IsEmptyOrWhiteSpace(value) == false);
        }

        /// <summary>
        ///  检查字符串是否为空,空的或只包含空白字符并返回一个默认值
        /// </summary>
        /// <param name = "value">输入字符串</param>
        /// <param name = "defaultValue">默认值</param>
        /// <returns></returns>
        public static string IfEmptyOrWhiteSpace(string value, string defaultValue)
        {
            return (IsEmptyOrWhiteSpace(value) ? defaultValue : value);
        }

        /// <summary>
        /// 首字母大写
        /// </summary>
        /// <param name = "value">输入字符串</param>
        public static string ToUpperFirstLetter(string value)
        {
            if (IsEmptyOrWhiteSpace(value)) return string.Empty;

            char[] valueChars = value.ToCharArray();
            valueChars[0] = char.ToUpper(valueChars[0]);

            return new string(valueChars);
        }

        /// <summary>
        /// 返回字符串的左边
        /// </summary>
        /// <param name="value">输入字符串</param>
        /// <param name="characterCount">左边字符数</param>
        /// <returns>左边字符串</returns>
        public static string Left(string value, int characterCount)
        {
            if (value == null)
                throw new ArgumentNullException("value");
            if (characterCount >= value.Length)
                throw new ArgumentOutOfRangeException("characterCount", characterCount, "characterCount must be less than length of string");
            return value.Substring(0, characterCount);
        }

        /// <summary>
        /// 返回字符串的右边
        /// </summary>
        /// <param name="value">输入字符串</param>
        /// <param name="characterCount">右边字符数</param>
        /// <returns>右边字符串</returns>
        public static string Right(string value, int characterCount)
        {
            if (value == null)
                throw new ArgumentNullException("value");
            if (characterCount >= value.Length)
                throw new ArgumentOutOfRangeException("characterCount", characterCount, "characterCount must be less than length of string");
            return value.Substring(value.Length - characterCount);
        }

        /// <summary>返回指定位置前的字符串</summary>
        /// <param name="value">输入字符串</param>
        /// <param name="index">索引</param>
        /// <returns></returns>
        public static string GetSubString(string value, int index)
        {
            return index < 0 ? value : value.Substring(index, value.Length - index);
        }

        /// <summary>
        /// 字符串如果操过指定长度则将超出的部分用指定字符串代替
        /// </summary>
        /// <param name="value">要检查的字符串</param>
        /// <param name="length">指定长度</param>
        /// <param name="p_TailString">用于替换的字符串</param>
        /// <returns>截取后的字符串</returns>
        public static string GetSubString(string value, int maxLength, string suffix)
        {
            return GetSubString(value, 0, maxLength, suffix);
        }

        public static string GetUnicodeSubString(string value, int maxLength, string suffix)
        {
            value = value.TrimEnd();
            string result = string.Empty;// 最终返回的结果
            int byteLen = System.Text.Encoding.Default.GetByteCount(value);// 单字节字符长度
            int charLen = value.Length;// 把字符平等对待时的字符串长度
            int byteCount = 0;// 记录读取进度
            int pos = 0;// 记录截取位置
            if (byteLen > maxLength)
            {
                for (int i = 0; i < charLen; i++)
                {
                    if (Convert.ToInt32(value.ToCharArray()[i]) > 255)// 按中文字符计算加2
                        byteCount += 2;
                    else// 按英文字符计算加1
                        byteCount += 1;
                    if (byteCount > maxLength)// 超出时只记下上一个有效位置
                    {
                        pos = i;
                        break;
                    }
                    else if (byteCount == maxLength)// 记下当前位置
                    {
                        pos = i + 1;
                        break;
                    }
                }

                if (pos >= 0)
                    result = value.Substring(0, pos) + suffix;
            }
            else
                result = value;

            return result;
        }

        /// <summary>
        /// 取指定长度的字符串
        /// </summary>
        /// <param name="value">要检查的字符串</param>
        /// <param name="index">起始位置</param>
        /// <param name="maxLength">指定长度</param>
        /// <param name="suffix">用于替换的字符串</param>
        /// <returns>截取后的字符串</returns>
        public static string GetSubString(string value, int index, int maxLength, string suffix)
        {
            string result = value;

            Byte[] bComments = Encoding.UTF8.GetBytes(value);
            foreach (char c in Encoding.UTF8.GetChars(bComments))
            {    //当是日文或韩文时(注:中文的范围:\u4e00 - \u9fa5, 日文在\u0800 - \u4e00, 韩文为\xAC00-\xD7A3)
                if ((c > '\u0800' && c < '\u4e00') || (c > '\xAC00' && c < '\xD7A3'))
                {
                    //if (System.Text.RegularExpressions.Regex.IsMatch(p_SrcString, "[\u0800-\u4e00]+") || System.Text.RegularExpressions.Regex.IsMatch(p_SrcString, "[\xAC00-\xD7A3]+"))
                    //当截取的起始位置超出字段串长度时
                    if (index >= value.Length)
                        return "";
                    else
                        return value.Substring(index,
                                                       ((maxLength + index) > value.Length) ? (value.Length - index) : maxLength);
                }
            }

            if (maxLength >= 0)
            {
                byte[] bsSrcString = Encoding.Default.GetBytes(value);

                //当字符串长度大于起始位置
                if (bsSrcString.Length > index)
                {
                    int p_EndIndex = bsSrcString.Length;

                    //当要截取的长度在字符串的有效长度范围内
                    if (bsSrcString.Length > (index + maxLength))
                    {
                        p_EndIndex = maxLength + index;
                    }
                    else
                    {   //当不在有效范围内时,只取到字符串的结尾

                        maxLength = bsSrcString.Length - index;
                        suffix = "";
                    }

                    int nRealLength = maxLength;
                    int[] anResultFlag = new int[maxLength];
                    byte[] bsResult = null;

                    int nFlag = 0;
                    for (int i = index; i < p_EndIndex; i++)
                    {
                        if (bsSrcString[i] > 127)
                        {
                            nFlag++;
                            if (nFlag == 3)
                                nFlag = 1;
                        }
                        else
                            nFlag = 0;

                        anResultFlag[i] = nFlag;
                    }

                    if ((bsSrcString[p_EndIndex - 1] > 127) && (anResultFlag[maxLength - 1] == 1))
                        nRealLength = maxLength + 1;

                    bsResult = new byte[nRealLength];

                    Array.Copy(bsSrcString, index, bsResult, 0, nRealLength);

                    result = Encoding.Default.GetString(bsResult);
                    result = result + suffix;
                }
            }

            return result;
        }
        /// <summary>返回指定位置前的字符串</summary>
        /// <param name="value">输入字符串</param>
        /// <param name="index">索引</param>
        /// <returns></returns>
        public static string SubstringFrom(string value, int index)
        {
            return index < 0 ? value : value.Substring(index, value.Length - index);
        }

        public static byte[] GetBytes(string data)
        {
            return Encoding.Default.GetBytes(data);
        }

        public static byte[] GetBytes(string data, Encoding encoding)
        {
            return encoding.GetBytes(data);
        }

        public static string ToTitleCase(string value)
        {
            return ToTitleCase(value, CultureInfo.CurrentCulture);
        }

        public static string ToTitleCase(string value, CultureInfo culture)
        {
            return culture.TextInfo.ToTitleCase(value);
        }

        /// <summary>
        /// 转为复数
        /// </summary>
        /// <param name="singular">输入字符串</param>
        /// <returns></returns>
        public static string ToPlural(string singular)
        {
            // Multiple words in the form A of B : Apply the plural to the first word only (A)
            int index = singular.LastIndexOf(" of ");
            if (index > 0) return ToPlural((singular.Substring(0, index)) + singular.Remove(0, index));

            // single Word rules
            //sibilant ending rule
            if (singular.EndsWith("sh")) return singular + "es";
            if (singular.EndsWith("ch")) return singular + "es";
            if (singular.EndsWith("us")) return singular + "es";
            if (singular.EndsWith("ss")) return singular + "es";
            //-ies rule
            if (singular.EndsWith("y")) return singular.Remove(singular.Length - 1, 1) + "ies";
            // -oes rule
            if (singular.EndsWith("o")) return singular.Remove(singular.Length - 1, 1) + "oes";
            // -s suffix rule
            return singular + "s";
        }

        /// <summary>
        /// 转为安全的HTML代码
        /// </summary>
        /// <param name="s">The current instance.</param>
        /// <returns>An HTML safe string.</returns>
        public static string ToHtmlSafe(string s)
        {
            return ToHtmlSafe(s, false, false);
        }

        /// <summary>
        /// 转为安全的HTML代码
        /// </summary>
        /// <param name="s">The current instance.</param>
        /// <param name="all">Whether to make all characters entities or just those needed.</param>
        /// <returns>An HTML safe string.</returns>
        public static string ToHtmlSafe(string s, bool all)
        {
            return ToHtmlSafe(s, all, false);
        }

        /// <summary>
        /// 转为安全的HTML代码
        /// </summary>
        /// <param name="s">The current instance.</param>
        /// <param name="all">Whether to make all characters entities or just those needed.</param>
        /// <param name="replace">Whether or not to encode spaces and line breaks.</param>
        /// <returns>An HTML safe string.</returns>
        public static string ToHtmlSafe(string s, bool all, bool replace)
        {
            if (IsEmptyOrWhiteSpace(s))
                return string.Empty;
            var entities = new[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 26, 28, 29, 30, 31, 34, 39, 38, 60, 62, 123, 124, 125, 126, 127, 160, 161, 162, 163, 164, 165, 166, 167, 168, 169, 170, 171, 172, 173, 174, 175, 176, 177, 178, 179, 180, 181, 182, 183, 184, 185, 186, 187, 188, 189, 190, 191, 215, 247, 192, 193, 194, 195, 196, 197, 198, 199, 200, 201, 202, 203, 204, 205, 206, 207, 208, 209, 210, 211, 212, 213, 214, 215, 216, 217, 218, 219, 220, 221, 222, 223, 224, 225, 226, 227, 228, 229, 230, 231, 232, 233, 234, 235, 236, 237, 238, 239, 240, 241, 242, 243, 244, 245, 246, 247, 248, 249, 250, 251, 252, 253, 254, 255, 256, 8704, 8706, 8707, 8709, 8711, 8712, 8713, 8715, 8719, 8721, 8722, 8727, 8730, 8733, 8734, 8736, 8743, 8744, 8745, 8746, 8747, 8756, 8764, 8773, 8776, 8800, 8801, 8804, 8805, 8834, 8835, 8836, 8838, 8839, 8853, 8855, 8869, 8901, 913, 914, 915, 916, 917, 918, 919, 920, 921, 922, 923, 924, 925, 926, 927, 928, 929, 931, 932, 933, 934, 935, 936, 937, 945, 946, 947, 948, 949, 950, 951, 952, 953, 954, 955, 956, 957, 958, 959, 960, 961, 962, 963, 964, 965, 966, 967, 968, 969, 977, 978, 982, 338, 339, 352, 353, 376, 402, 710, 732, 8194, 8195, 8201, 8204, 8205, 8206, 8207, 8211, 8212, 8216, 8217, 8218, 8220, 8221, 8222, 8224, 8225, 8226, 8230, 8240, 8242, 8243, 8249, 8250, 8254, 8364, 8482, 8592, 8593, 8594, 8595, 8596, 8629, 8968, 8969, 8970, 8971, 9674, 9824, 9827, 9829, 9830 };
            var sb = new StringBuilder();
            foreach (var c in s)
            {
                if (all || entities.Contains(c))
                    sb.Append("&#" + ((int)c) + ";");
                else
                    sb.Append(c);
            }

            return replace ? sb.Replace("", "<br />").Replace("\n", "<br />").Replace(" ", "&nbsp;").ToString() : sb.ToString();
        }

        /// <summary>
        /// Returns true if strings are equals, without consideration to case (<see cref="StringComparison.InvariantCultureIgnoreCase"/>)
        /// </summary>
        public static bool EquivalentTo(string s, string whateverCaseString)
        {
            return string.Equals(s, whateverCaseString, StringComparison.InvariantCultureIgnoreCase);
        }

        /// <summary>
        /// Replace all values in string
        /// </summary>
        /// <param name="value">The input string.</param>
        /// <param name="oldValues">List of old values, which must be replaced</param>
        /// <param name="replacePredicate">Function for replacement old values</param>
        /// <returns>Returns new string with the replaced values</returns>
        /// <example>
        /// 	<code>
        ///         var str = "White Red Blue Green Yellow Black Gray";
        ///         var achromaticColors = new[] {"White", "Black", "Gray"};
        ///         str = str.ReplaceAll(achromaticColors, v => "[" + v + "]");
        ///         // str == "[White] Red Blue Green Yellow [Black] [Gray]"
        /// 	</code>
        /// </example>
        public static string ReplaceAll(string value, IEnumerable<string> oldValues, Func<string, string> replacePredicate)
        {
            var sbStr = new StringBuilder(value);
            foreach (var oldValue in oldValues)
            {
                var newValue = replacePredicate(oldValue);
                sbStr.Replace(oldValue, newValue);
            }

            return sbStr.ToString();
        }

        /// <summary>
        /// Replace all values in string
        /// </summary>
        /// <param name="value">The input string.</param>
        /// <param name="oldValues">List of old values, which must be replaced</param>
        /// <param name="newValue">New value for all old values</param>
        /// <returns>Returns new string with the replaced values</returns>
        /// <example>
        /// 	<code>
        ///         var str = "White Red Blue Green Yellow Black Gray";
        ///         var achromaticColors = new[] {"White", "Black", "Gray"};
        ///         str = str.ReplaceAll(achromaticColors, "[AchromaticColor]");
        ///         // str == "[AchromaticColor] Red Blue Green Yellow [AchromaticColor] [AchromaticColor]"
        /// 	</code>
        /// </example>
        public static string ReplaceAll(string value, IEnumerable<string> oldValues, string newValue)
        {
            var sbStr = new StringBuilder(value);
            foreach (var oldValue in oldValues)
                sbStr.Replace(oldValue, newValue);

            return sbStr.ToString();
        }

        /// <summary>
        /// Replace all values in string
        /// </summary>
        /// <param name="value">The input string.</param>
        /// <param name="oldValues">List of old values, which must be replaced</param>
        /// <param name="newValues">List of new values</param>
        /// <returns>Returns new string with the replaced values</returns>
        /// <example>
        /// 	<code>
        ///         var str = "White Red Blue Green Yellow Black Gray";
        ///         var achromaticColors = new[] {"White", "Black", "Gray"};
        ///         var exquisiteColors = new[] {"FloralWhite", "Bistre", "DavyGrey"};
        ///         str = str.ReplaceAll(achromaticColors, exquisiteColors);
        ///         // str == "FloralWhite Red Blue Green Yellow Bistre DavyGrey"
        /// 	</code>
        /// </example>
        public static string ReplaceAll(string value, IEnumerable<string> oldValues, IEnumerable<string> newValues)
        {
            var sbStr = new StringBuilder(value);
            var newValueEnum = newValues.GetEnumerator();
            foreach (var old in oldValues)
            {
                if (!newValueEnum.MoveNext())
                    throw new ArgumentOutOfRangeException("newValues", "newValues sequence is shorter than oldValues sequence");
                sbStr.Replace(old, newValueEnum.Current);
            }
            if (newValueEnum.MoveNext())
                throw new ArgumentOutOfRangeException("newValues", "newValues sequence is longer than oldValues sequence");

            return sbStr.ToString();
        }

        #endregion

        #region Regex based extension methods

        /// <summary>
        /// 	Uses regular expressions to determine if the string matches to a given regex pattern.
        /// </summary>
        /// <param name = "value">The input string.</param>
        /// <param name = "regexPattern">The regular expression pattern.</param>
        /// <returns>
        /// 	<c>true</c> if the value is matching to the specified pattern; otherwise, <c>false</c>.
        /// </returns>
        /// <example>
        /// 	<code>
        /// 		var s = "12345";
        /// 		var isMatching = s.IsMatchingTo(@"^\d+$");
        /// 	</code>
        /// </example>
        public static bool IsMatchingTo(string value, string regexPattern)
        {
            return IsMatchingTo(value, regexPattern, RegexOptions.None);
        }

        /// <summary>
        /// 	Uses regular expressions to determine if the string matches to a given regex pattern.
        /// </summary>
        /// <param name = "value">The input string.</param>
        /// <param name = "regexPattern">The regular expression pattern.</param>
        /// <param name = "options">The regular expression options.</param>
        /// <returns>
        /// 	<c>true</c> if the value is matching to the specified pattern; otherwise, <c>false</c>.
        /// </returns>
        /// <example>
        /// 	<code>
        /// 		var s = "12345";
        /// 		var isMatching = s.IsMatchingTo(@"^\d+$");
        /// 	</code>
        /// </example>
        public static bool IsMatchingTo(string value, string regexPattern, RegexOptions options)
        {
            return Regex.IsMatch(value, regexPattern, options);
        }

        /// <summary>
        /// 	Uses regular expressions to replace parts of a string.
        /// </summary>
        /// <param name = "value">The input string.</param>
        /// <param name = "regexPattern">The regular expression pattern.</param>
        /// <param name = "replaceValue">The replacement value.</param>
        /// <returns>The newly created string</returns>
        /// <example>
        /// 	<code>
        /// 		var s = "12345";
        /// 		var replaced = s.ReplaceWith(@"\d", m => string.Concat(" -", m.Value, "- "));
        /// 	</code>
        /// </example>
        public static string ReplaceWith(string value, string regexPattern, string replaceValue)
        {
            return ReplaceWith(value, regexPattern, replaceValue, RegexOptions.None);
        }

        /// <summary>
        /// 	Uses regular expressions to replace parts of a string.
        /// </summary>
        /// <param name = "value">The input string.</param>
        /// <param name = "regexPattern">The regular expression pattern.</param>
        /// <param name = "replaceValue">The replacement value.</param>
        /// <param name = "options">The regular expression options.</param>
        /// <returns>The newly created string</returns>
        /// <example>
        /// 	<code>
        /// 		var s = "12345";
        /// 		var replaced = s.ReplaceWith(@"\d", m => string.Concat(" -", m.Value, "- "));
        /// 	</code>
        /// </example>
        public static string ReplaceWith(string value, string regexPattern, string replaceValue, RegexOptions options)
        {
            return Regex.Replace(value, regexPattern, replaceValue, options);
        }

        /// <summary>
        /// 	Uses regular expressions to replace parts of a string.
        /// </summary>
        /// <param name = "value">The input string.</param>
        /// <param name = "regexPattern">The regular expression pattern.</param>
        /// <param name = "evaluator">The replacement method / lambda expression.</param>
        /// <returns>The newly created string</returns>
        /// <example>
        /// 	<code>
        /// 		var s = "12345";
        /// 		var replaced = s.ReplaceWith(@"\d", m => string.Concat(" -", m.Value, "- "));
        /// 	</code>
        /// </example>
        public static string ReplaceWith(string value, string regexPattern, MatchEvaluator evaluator)
        {
            return ReplaceWith(value, regexPattern, RegexOptions.None, evaluator);
        }

        /// <summary>
        /// 	Uses regular expressions to replace parts of a string.
        /// </summary>
        /// <param name = "value">The input string.</param>
        /// <param name = "regexPattern">The regular expression pattern.</param>
        /// <param name = "options">The regular expression options.</param>
        /// <param name = "evaluator">The replacement method / lambda expression.</param>
        /// <returns>The newly created string</returns>
        /// <example>
        /// 	<code>
        /// 		var s = "12345";
        /// 		var replaced = s.ReplaceWith(@"\d", m => string.Concat(" -", m.Value, "- "));
        /// 	</code>
        /// </example>
        public static string ReplaceWith(string value, string regexPattern, RegexOptions options, MatchEvaluator evaluator)
        {
            return Regex.Replace(value, regexPattern, evaluator, options);
        }

        /// <summary>
        /// 	Uses regular expressions to determine all matches of a given regex pattern.
        /// </summary>
        /// <param name = "value">The input string.</param>
        /// <param name = "regexPattern">The regular expression pattern.</param>
        /// <returns>A collection of all matches</returns>
        public static MatchCollection GetMatches(string value, string regexPattern)
        {
            return GetMatches(value, regexPattern, RegexOptions.None);
        }

        /// <summary>
        /// 	Uses regular expressions to determine all matches of a given regex pattern.
        /// </summary>
        /// <param name = "value">The input string.</param>
        /// <param name = "regexPattern">The regular expression pattern.</param>
        /// <param name = "options">The regular expression options.</param>
        /// <returns>A collection of all matches</returns>
        public static MatchCollection GetMatches(string value, string regexPattern, RegexOptions options)
        {
            return Regex.Matches(value, regexPattern, options);
        }

        /// <summary>
        /// 	Uses regular expressions to determine all matches of a given regex pattern and returns them as string enumeration.
        /// </summary>
        /// <param name = "value">The input string.</param>
        /// <param name = "regexPattern">The regular expression pattern.</param>
        /// <returns>An enumeration of matching strings</returns>
        /// <example>
        /// 	<code>
        /// 		var s = "12345";
        /// 		foreach(var number in s.GetMatchingValues(@"\d")) {
        /// 		Console.WriteLine(number);
        /// 		}
        /// 	</code>
        /// </example>
        public static IEnumerable<string> GetMatchingValues(string value, string regexPattern)
        {
            return GetMatchingValues(value, regexPattern, RegexOptions.None);
        }

        /// <summary>
        /// 	Uses regular expressions to determine all matches of a given regex pattern and returns them as string enumeration.
        /// </summary>
        /// <param name = "value">The input string.</param>
        /// <param name = "regexPattern">The regular expression pattern.</param>
        /// <param name = "options">The regular expression options.</param>
        /// <returns>An enumeration of matching strings</returns>
        /// <example>
        /// 	<code>
        /// 		var s = "12345";
        /// 		foreach(var number in s.GetMatchingValues(@"\d")) {
        /// 		Console.WriteLine(number);
        /// 		}
        /// 	</code>
        /// </example>
        public static IEnumerable<string> GetMatchingValues(string value, string regexPattern, RegexOptions options)
        {
            foreach (Match match in GetMatches(value, regexPattern, options))
            {
                if (match.Success) yield return match.Value;
            }
        }

        public static string[] Split(string value, char separator)
        {
            if (string.IsNullOrEmpty(value))
                return null;
            return value.Split(separator);
        }

        /// <summary>
        /// 	Uses regular expressions to split a string into parts.
        /// </summary>
        /// <param name = "value">The input string.</param>
        /// <param name = "regexPattern">The regular expression pattern.</param>
        /// <returns>The splitted string array</returns>
        public static string[] RegexSplit(string value, string regexPattern)
        {
            return RegexSplit(value, regexPattern, RegexOptions.None);
        }

        /// <summary>
        /// 	Uses regular expressions to split a string into parts.
        /// </summary>
        /// <param name = "value">The input string.</param>
        /// <param name = "regexPattern">The regular expression pattern.</param>
        /// <param name = "options">The regular expression options.</param>
        /// <returns>The splitted string array</returns>
        public static string[] RegexSplit(string value, string regexPattern, RegexOptions options)
        {
            return Regex.Split(value, regexPattern, options);
        }

        /// <summary>
        /// 	Splits the given string into words and returns a string array.
        /// </summary>
        /// <param name = "value">The input string.</param>
        /// <returns>The splitted string array</returns>
        public static string[] GetWords(string value)
        {
            return RegexSplit(value, @"\W");
        }

        /// <summary>
        /// 	Gets the nth "word" of a given string, where "words" are substrings separated by a given separator
        /// </summary>
        /// <param name = "value">The string from which the word should be retrieved.</param>
        /// <param name = "index">Index of the word (0-based).</param>
        /// <returns>
        /// 	The word at position n of the string.
        /// 	Trying to retrieve a word at a position lower than 0 or at a position where no word exists results in an exception.
        /// </returns>
        /// <remarks>
        /// 	Originally contributed by MMathews
        /// </remarks>
        public static string GetWordByIndex(string value, int index)
        {
            var words = GetWords(value);

            if ((index < 0) || (index > words.Length - 1))
                throw new IndexOutOfRangeException("The word number is out of range.");

            return words[index];
        }

        /// <summary>
        /// Removed all special characters from the string.
        /// </summary>
        /// <param name="value">The input string.</param>
        /// <returns>The adjusted string.</returns>
        [Obsolete("Please use RemoveAllSpecialCharacters instead")]
        public static string AdjustInput(string value)
        {
            return string.Join(null, Regex.Split(value, "[^a-zA-Z0-9]"));
        }

        /// <summary>
        /// Removed all special characters from the string.
        /// </summary>
        /// <param name="value">The input string.</param>
        /// <returns>The adjusted string.</returns>
        public static string RemoveAllSpecialCharacters(string value)
        {
            var sb = new StringBuilder(value.Length);
            foreach (var c in value.Where(c => Char.IsLetterOrDigit(c)))
                sb.Append(c);
            return sb.ToString();
        }

        /// <summary>
        /// Add space on every upper character
        /// </summary>
        /// <param name="value">The input string.</param>
        /// <returns>The adjusted string.</returns>
        public static string SpaceOnUpper(string value)
        {
            return Regex.Replace(value, "([A-Z])(?=[a-z])|(?<=[a-z])([A-Z]|[0-9]+)", " $1$2").TrimStart();
        }

        #region ExtractArguments extension

        /// <summary>
        /// Options to match the template with the original string
        /// </summary>
        public enum ComparsionTemplateOptions
        {
            /// <summary>
            /// Free template comparsion
            /// </summary>
            Default,

            /// <summary>
            /// Template compared from beginning of input string
            /// </summary>
            FromStart,

            /// <summary>
            /// Template compared with the end of input string
            /// </summary>
            AtTheEnd,

            /// <summary>
            /// Template compared whole with input string
            /// </summary>
            Whole,
        }

        private const RegexOptions _defaultRegexOptions = RegexOptions.None;
        private const ComparsionTemplateOptions _defaultComparsionTemplateOptions = ComparsionTemplateOptions.Default;
        private static readonly string[] _reservedRegexOperators = new[] { @"\", "^", "$", "*", "+", "?", ".", "(", ")" };

        private static string GetRegexPattern(string template, ComparsionTemplateOptions compareTemplateOptions)
        {
            template = ReplaceAll(template, _reservedRegexOperators, v => @"\" + v);

            bool comparingFromStart = compareTemplateOptions == ComparsionTemplateOptions.FromStart ||
                                      compareTemplateOptions == ComparsionTemplateOptions.Whole;
            bool comparingAtTheEnd = compareTemplateOptions == ComparsionTemplateOptions.AtTheEnd ||
                                     compareTemplateOptions == ComparsionTemplateOptions.Whole;
            var pattern = new StringBuilder();

            if (comparingFromStart)
                pattern.Append("^");

            pattern.Append(Regex.Replace(template, @"\{[0-9]+\}",
                                            delegate(Match m)
                                            {
                                                var argNum = m.ToString().Replace("{", "").Replace("}", "");
                                                return String.Format("(?<{0}>.*?)", int.Parse(argNum) + 1);
                                            }
                          ));

            if (comparingAtTheEnd || (template.LastOrDefault() == '}' && compareTemplateOptions == ComparsionTemplateOptions.Default))
                pattern.Append("$");

            return pattern.ToString();
        }

        /// <summary>
        /// Extract arguments from string by template
        /// </summary>
        /// <param name="value">The input string. For example, "My name is Aleksey".</param>
        /// <param name="template">Template with arguments in the format {№}. For example, "My name is {0} {1}.".</param>
        /// <param name="compareTemplateOptions">Template options for compare with input string.</param>
        /// <param name="regexOptions">Regex options.</param>
        /// <returns>Returns parsed arguments.</returns>
        /// <example>
        /// 	<code>
        /// 		var str = "My name is Aleksey Nagovitsyn. I'm from Russia.";
        /// 		var args = str.ExtractArguments(@"My name is {1} {0}. I'm from {2}.");
        ///         // args[i] is [Nagovitsyn, Aleksey, Russia]
        /// 	</code>
        /// </example>
        public static IEnumerable<string> ExtractArguments(string value, string template,
                                                           ComparsionTemplateOptions compareTemplateOptions = _defaultComparsionTemplateOptions,
                                                           RegexOptions regexOptions = _defaultRegexOptions)
        {
            return ExtractGroupArguments(value, template, compareTemplateOptions, regexOptions).Select(g => g.Value);
        }

        /// <summary>
        /// Extract arguments as regex groups from string by template
        /// </summary>
        /// <param name="value">The input string. For example, "My name is Aleksey".</param>
        /// <param name="template">Template with arguments in the format {№}. For example, "My name is {0} {1}.".</param>
        /// <param name="compareTemplateOptions">Template options for compare with input string.</param>
        /// <param name="regexOptions">Regex options.</param>
        /// <returns>Returns parsed arguments as regex groups.</returns>
        /// <example>
        /// 	<code>
        /// 		var str = "My name is Aleksey Nagovitsyn. I'm from Russia.";
        /// 		var groupArgs = str.ExtractGroupArguments(@"My name is {1} {0}. I'm from {2}.");
        ///         // groupArgs[i].Value is [Nagovitsyn, Aleksey, Russia]
        /// 	</code>
        /// </example>
        public static IEnumerable<Group> ExtractGroupArguments(string value, string template,
                                                               ComparsionTemplateOptions compareTemplateOptions = _defaultComparsionTemplateOptions,
                                                               RegexOptions regexOptions = _defaultRegexOptions)
        {
            var pattern = GetRegexPattern(template, compareTemplateOptions);
            var regex = new Regex(pattern, regexOptions);
            var match = regex.Match(value);

            return Enumerable.Cast<Group>(match.Groups).Skip(1);
        }

        #endregion ExtractArguments extension

        #endregion

        #region Bytes & Base64

        /// <summary>
        /// 	Converts the string to a byte-array using the default encoding
        /// </summary>
        /// <param name = "value">The input string.</param>
        /// <returns>The created byte array</returns>
        public static byte[] ToBytes(string value)
        {
            return ToBytes(value, null);
        }

        /// <summary>
        /// 	Converts the string to a byte-array using the supplied encoding
        /// </summary>
        /// <param name = "value">The input string.</param>
        /// <param name = "encoding">The encoding to be used.</param>
        /// <returns>The created byte array</returns>
        /// <example>
        /// 	<code>
        /// 		var value = "Hello World";
        /// 		var ansiBytes = value.ToBytes(Encoding.GetEncoding(1252)); // 1252 = ANSI
        /// 		var utf8Bytes = value.ToBytes(Encoding.UTF8);
        /// 	</code>
        /// </example>
        public static byte[] ToBytes(string value, Encoding encoding)
        {
            encoding = (encoding ?? Encoding.Default);
            return encoding.GetBytes(value);
        }

        /// <summary>
        /// 	Encodes the input value to a Base64 string using the default encoding.
        /// </summary>
        /// <param name = "value">The input value.</param>
        /// <returns>The Base 64 encoded string</returns>
        public static string EncodeBase64(string value)
        {
            return EncodeBase64(value, null);
        }

        /// <summary>
        /// 	Encodes the input value to a Base64 string using the supplied encoding.
        /// </summary>
        /// <param name = "value">The input value.</param>
        /// <param name = "encoding">The encoding.</param>
        /// <returns>The Base 64 encoded string</returns>
        public static string EncodeBase64(string value, Encoding encoding)
        {
            encoding = (encoding ?? Encoding.UTF8);
            var bytes = encoding.GetBytes(value);
            return Convert.ToBase64String(bytes);
        }

        /// <summary>
        /// 	Decodes a Base 64 encoded value to a string using the default encoding.
        /// </summary>
        /// <param name = "encodedValue">The Base 64 encoded value.</param>
        /// <returns>The decoded string</returns>
        public static string DecodeBase64(string encodedValue)
        {
            encodedValue = encodedValue.Replace("\n", "").Replace(" ", "+");
            encodedValue = Regex.Unescape(encodedValue);
            return DecodeBase64(encodedValue, null);
        }

        /// <summary>
        /// 	Decodes a Base 64 encoded value to a string using the supplied encoding.
        /// </summary>
        /// <param name = "encodedValue">The Base 64 encoded value.</param>
        /// <param name = "encoding">The encoding.</param>
        /// <returns>The decoded string</returns>
        public static string DecodeBase64(string encodedValue, Encoding encoding)
        {
            encoding = (encoding ?? Encoding.UTF8);
            var bytes = Convert.FromBase64String(encodedValue);
            return encoding.GetString(bytes);
        }

        #endregion

        #region String to Enum

        /// <summary>
        ///     Parse a string to a enum item if that string exists in the enum otherwise return the default enum item.
        /// </summary>
        /// <typeparam name="TEnum">The Enum type.</typeparam>
        /// <param name="dataToMatch">The data will use to convert into give enum</param>
        /// <param name="ignorecase">Whether the enum parser will ignore the given data's case or not.</param>
        /// <returns>Converted enum.</returns>
        /// <example>
        /// 	<code>
        /// 		public enum EnumTwo {  None, One,}
        /// 		object[] items = new object[] { "One".ParseStringToEnum<EnumTwo>(), "Two".ParseStringToEnum<EnumTwo>() };
        /// 	</code>
        /// </example>
        public static TEnum ParseStringToEnum<TEnum>(string dataToMatch, bool ignorecase = default(bool))
                where TEnum : struct
        {
            return IsItemInEnum<TEnum>(dataToMatch)() ? default(TEnum) : (TEnum)Enum.Parse(typeof(TEnum), dataToMatch, default(bool));
        }

        /// <summary>
        ///     To check whether the data is defined in the given enum.
        /// </summary>
        /// <typeparam name="TEnum">The enum will use to check, the data defined.</typeparam>
        /// <param name="dataToCheck">To match against enum.</param>
        /// <returns>Anonoymous method for the condition.</returns>
        public static Func<bool> IsItemInEnum<TEnum>(string dataToCheck)
            where TEnum : struct
        {
            return () => { return string.IsNullOrEmpty(dataToCheck) || !Enum.IsDefined(typeof(TEnum), dataToCheck); };
        }

        #endregion

        /// <summary>
        /// 判断指定的字符串是否不包含空,空的或只包含空白字符且包含在输入字符串中
        /// </summary>
        /// <param name="inputValue">输入字符串</param>
        /// <param name="comparisonValue">比较值</param>
        /// <returns>true为空包含在输入字符串中</returns>
        private static bool StringContainsEquivalence(string inputValue, string comparisonValue)
        {
            return (IsNotEmptyOrWhiteSpace(inputValue) && Contains(inputValue, comparisonValue, StringComparison.InvariantCultureIgnoreCase));
        }

        /// <summary>
        /// 判断指定的字符串是否包含空,空的或只包含空白字符
        /// </summary>
        /// <param name="inputValue">输入字符串</param>
        /// <param name="comparisonValue">比较值</param>
        /// <returns>true都为空</returns>
        private static bool BothStringsAreEmpty(string inputValue, string comparisonValue)
        {
            return (IsEmptyOrWhiteSpace(inputValue) && IsEmptyOrWhiteSpace(comparisonValue));
        }

        /// <summary>
        /// 返回删除字符串的左边的字符数的字符
        /// </summary>
        /// <param name="str">输入字符串</param>
        /// <param name="number_of_characters">删除字符个数</param>
        /// <returns></returns>
        public static String RemoveLeft(String str, int number_of_characters)
        {
            if (str.Length <= number_of_characters) return "";
            return str.Substring(number_of_characters);
        }

        /// <summary>
        /// 返回删除字符串的右边的字符数的字符
        /// </summary>
        /// <param name="str">输入字符串</param>
        /// <param name="number_of_characters">删除字符个数</param>
        /// <returns></returns>
        public static String RemoveRight(String str, int number_of_characters)
        {
            if (str.Length <= number_of_characters) return "";
            return str.Substring(0, str.Length - number_of_characters);
        }

        /// <summary>
        /// Encrypt this string into a byte array.
        /// </summary>
        /// <param name="plain_text">The string being extended and that will be encrypted.</param>
        /// <param name="password">The password to use then encrypting the string.</param>
        /// <returns></returns>
        /// <remarks></remarks>
        public static byte[] EncryptToByteArray(String plain_text, String password)
        {
            var ascii_encoder = new ASCIIEncoding();
            byte[] plain_bytes = ascii_encoder.GetBytes(plain_text);
            return CryptBytes(password, plain_bytes, true);
        }

        /// <summary>
        /// Decrypt the encryption stored in this byte array.
        /// </summary>
        /// <param name="encrypted_bytes">The byte array to decrypt.</param>
        /// <param name="password">The password to use when decrypting.</param>
        /// <returns></returns>
        /// <remarks></remarks>
        public static String DecryptFromByteArray(byte[] encrypted_bytes, String password)
        {
            byte[] decrypted_bytes = CryptBytes(password, encrypted_bytes, false);
            var ascii_encoder = new ASCIIEncoding();
            return new String(ascii_encoder.GetChars(decrypted_bytes));
        }

        /// <summary>
        /// Encrypt this string and return the result as a string of hexadecimal characters.
        /// </summary>
        /// <param name="plain_text">The string being extended and that will be encrypted.</param>
        /// <param name="password">The password to use then encrypting the string.</param>
        /// <returns></returns>
        /// <remarks></remarks>
        public static String EncryptToString(String plain_text, String password)
        {
            return BytesToHexString(EncryptToByteArray(plain_text, password));
        }

        /// <summary>
        /// Decrypt the encryption stored in this string of hexadecimal values.
        /// </summary>
        /// <param name="encrypted_bytes_string">The hexadecimal string to decrypt.</param>
        /// <param name="password">The password to use then encrypting the string.</param>
        /// <returns></returns>
        /// <remarks></remarks>
        public static String DecryptFromString(String encrypted_bytes_string, String password)
        {
            return DecryptFromByteArray(HexStringToBytes(encrypted_bytes_string), password);
        }

        /// <summary>
        /// Encrypt or decrypt a byte array using the TripleDESCryptoServiceProvider crypto provider and Rfc2898DeriveBytes to build the key and initialization vector.
        /// </summary>
        /// <param name="password">The password string to use in encrypting or decrypting.</param>
        /// <param name="in_bytes">The array of bytes to encrypt.</param>
        /// <param name="encrypt">True to encrypt, False to decrypt.</param>
        /// <returns></returns>
        /// <remarks></remarks>
        private static byte[] CryptBytes(String password, byte[] in_bytes, bool encrypt)
        {
            // Make a triple DES service provider.
            var des_provider = new TripleDESCryptoServiceProvider();

            // Find a valid key size for this provider.
            int key_size_bits = 0;
            for (int i = 1024; i >= 1; i--)
            {
                if (des_provider.ValidKeySize(i))
                {
                    key_size_bits = i;
                    break;
                }
            }

            // Get the block size for this provider.
            int block_size_bits = des_provider.BlockSize;

            // Generate the key and initialization vector.
            byte[] key = null;
            byte[] iv = null;
            byte[] salt = { 0x10, 0x20, 0x12, 0x23, 0x37, 0xA4, 0xC5, 0xA6, 0xF1, 0xF0, 0xEE, 0x21, 0x22, 0x45 };
            MakeKeyAndIV(password, salt, key_size_bits, block_size_bits, ref key, ref iv);

            // Make the encryptor or decryptor.
            ICryptoTransform crypto_transform = encrypt
                                                    ? des_provider.CreateEncryptor(key, iv)
                                                    : des_provider.CreateDecryptor(key, iv);

            // Create the output stream.
            var out_stream = new MemoryStream();

            // Attach a crypto stream to the output stream.
            var crypto_stream = new CryptoStream(out_stream,
                                                 crypto_transform, CryptoStreamMode.Write);

            // Write the bytes into the CryptoStream.
            crypto_stream.Write(in_bytes, 0, in_bytes.Length);
            try
            {
                crypto_stream.FlushFinalBlock();
            }
            catch (CryptographicException)
            {
                // Ignore this one. The password is bad.
            }

            // Save the result.
            byte[] result = out_stream.ToArray();

            // Close the stream.
            try
            {
                crypto_stream.Close();
            }
            catch (CryptographicException)
            {
                // Ignore this one. The password is bad.
            }
            out_stream.Close();

            return result;
        }

        /// <summary>
        /// Use the password to generate key bytes and an initialization vector with Rfc2898DeriveBytes.
        /// </summary>
        /// <param name="password">The input password to use in generating the bytes.</param>
        /// <param name="salt">The input salt bytes to use in generating the bytes.</param>
        /// <param name="key_size_bits">The input size of the key to generate.</param>
        /// <param name="block_size_bits">The input block size used by the crypto provider.</param>
        /// <param name="key">The output key bytes to generate.</param>
        /// <param name="iv">The output initialization vector to generate.</param>
        /// <remarks></remarks>
        private static void MakeKeyAndIV(String password, byte[] salt, int key_size_bits, int block_size_bits,
                                         ref byte[] key, ref byte[] iv)
        {
            var derive_bytes =
                new Rfc2898DeriveBytes(password, salt, 1234);

            key = derive_bytes.GetBytes(key_size_bits / 8);
            iv = derive_bytes.GetBytes(block_size_bits / 8);
        }

        /// <summary>
        /// Convert a byte array into a hexadecimal string representation.
        /// </summary>
        /// <param name="bytes"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public static String BytesToHexString(byte[] bytes)
        {
            String result = "";
            foreach (byte b in bytes)
            {
                result += " " + b.ToString("X").PadLeft(2, '0');
            }
            if (result.Length > 0) result = result.Substring(1);
            return result;
        }

        /// <summary>
        /// Convert this string containing hexadecimal into a byte array.
        /// </summary>
        /// <param name="str">The hexadecimal string to convert.</param>
        /// <returns></returns>
        /// <remarks></remarks>
        public static byte[] HexStringToBytes(String str)
        {
            str = str.Replace(" ", "");
            int max_byte = str.Length / 2 - 1;
            var bytes = new byte[max_byte + 1];
            for (int i = 0; i <= max_byte; i++)
            {
                bytes[i] = byte.Parse(str.Substring(2 * i, 2), NumberStyles.AllowHexSpecifier);
            }

            return bytes;
        }

        /// <summary>
        /// Returns a default value if the string is null or empty.
        /// </summary>
        /// <param name="s">Original String</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns></returns>
        public static string DefaultIfNullOrEmpty(string s, string defaultValue)
        {
            return String.IsNullOrEmpty(s) ? defaultValue : s;
        }

        /// <summary>
        /// Throws an <see cref="System.ArgumentException"/> if the string value is empty.
        /// </summary>
        /// <param name="obj">The value to test.</param>
        /// <param name="message">The message to display if the value is null.</param>
        /// <param name="name">The name of the parameter being tested.</param>
        public static string ExceptionIfNullOrEmpty(string obj, string message, string name)
        {
            if (String.IsNullOrEmpty(obj))
                throw new ArgumentException(message, name);
            return obj;
        }

        /// <summary>
        /// Joins  the values of a string array if the values are not null or empty.
        /// </summary>
        /// <param name="objs">The string array used for joining.</param>
        /// <param name="separator">The separator to use in the joined string.</param>
        /// <returns></returns>
        public static string JoinNotNullOrEmpty(string[] objs, string separator)
        {
            var items = new List<string>();
            foreach (string s in objs)
            {
                if (!String.IsNullOrEmpty(s))
                    items.Add(s);
            }
            return String.Join(separator, items.ToArray());
        }

        /// <summary>
        /// Parses the commandline params.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>A StringDictionary type object of command line parameters.</returns>
        public static StringDictionary ParseCommandlineParams(string[] value)
        {
            var parameters = new StringDictionary();
            var spliter = new Regex(@"^-{1,2}|^/|=|:", RegexOptions.IgnoreCase | RegexOptions.Compiled);
            var remover = new Regex(@"^['""]?(.*?)['""]?$", RegexOptions.IgnoreCase | RegexOptions.Compiled);
            string parameter = null;

            // Valid parameters forms:
            // {-,/,--}param{ ,=,:}((",')value(",'))
            // Examples: -param1 value1 --param2 /param3:"Test-:-work" /param4=happy -param5 '--=nice=--'
            foreach (string txt in value)
            {
                // Look for new parameters (-,/ or --) and a possible enclosed value (=,:)
                string[] Parts = spliter.Split(txt, 3);
                switch (Parts.Length)
                {
                    // Found a value (for the last parameter found (space separator))
                    case 1:
                        if (parameter != null)
                        {
                            if (!parameters.ContainsKey(parameter))
                            {
                                Parts[0] = remover.Replace(Parts[0], "$1");
                                parameters.Add(parameter, Parts[0]);
                            }
                            parameter = null;
                        }
                        // else Error: no parameter waiting for a value (skipped)
                        break;
                    // Found just a parameter
                    case 2:
                        // The last parameter is still waiting. With no value, set it to true.
                        if (parameter != null)
                        {
                            if (!parameters.ContainsKey(parameter)) parameters.Add(parameter, "true");
                        }
                        parameter = Parts[1];
                        break;
                    // Parameter with enclosed value
                    case 3:
                        // The last parameter is still waiting. With no value, set it to true.
                        if (parameter != null)
                        {
                            if (!parameters.ContainsKey(parameter)) parameters.Add(parameter, "true");
                        }
                        parameter = Parts[1];
                        // Remove possible enclosing characters (",')
                        if (!parameters.ContainsKey(parameter))
                        {
                            Parts[2] = remover.Replace(Parts[2], "$1");
                            parameters.Add(parameter, Parts[2]);
                        }
                        parameter = null;
                        break;
                }
            }
            // In case a parameter is still waiting
            if (parameter != null)
            {
                if (!parameters.ContainsKey(parameter)) parameters.Add(parameter, "true");
            }

            return parameters;
        }

        /// <summary>
        /// Encodes the email address so that the link is still valid, but the email address is useless for email harvsters.
        /// </summary>
        /// <param name="emailAddress">The email address.</param>
        /// <returns></returns>
        public static string EncodeEmailAddress(string emailAddress)
        {
            int i;
            string repl;
            string tempHtmlEncode = emailAddress;
            for (i = tempHtmlEncode.Length; i >= 1; i--)
            {
                int acode = Convert.ToInt32(tempHtmlEncode[i - 1]);
                if (acode == 32)
                {
                    repl = " ";
                }
                else if (acode == 34)
                {
                    repl = "\"";
                }
                else if (acode == 38)
                {
                    repl = "&";
                }
                else if (acode == 60)
                {
                    repl = "<";
                }
                else if (acode == 62)
                {
                    repl = ">";
                }
                else if (acode >= 32 && acode <= 127)
                {
                    repl = "&#" + Convert.ToString(acode) + ";";
                }
                else
                {
                    repl = "&#" + Convert.ToString(acode) + ";";
                }
                if (repl.Length > 0)
                {
                    tempHtmlEncode = tempHtmlEncode.Substring(0, i - 1) +
                                     repl + tempHtmlEncode.Substring(i);
                }
            }
            return tempHtmlEncode;
        }

        /// <summary>
        /// Calculates the SHA1 hash of the supplied string and returns a base 64 string.
        /// </summary>
        /// <param name="stringToHash">String that must be hashed.</param>
        /// <returns>The hashed string or null if hashing failed.</returns>
        /// <exception cref="ArgumentException">Occurs when stringToHash or key is null or empty.</exception>
        public static string GetSHA1Hash(string stringToHash)
        {
            if (string.IsNullOrEmpty(stringToHash)) return null;
            //{
            //    throw new ArgumentException("An empty string value cannot be hashed.");
            //}

            Byte[] data = Encoding.UTF8.GetBytes(stringToHash);
            Byte[] hash = new SHA1CryptoServiceProvider().ComputeHash(data);
            return Convert.ToBase64String(hash);
        }

        /// <summary>
        /// Determines whether the string contains any of the provided values.
        /// </summary>
        /// <param name="this"></param>
        /// <param name="values"></param>
        /// <returns></returns>
        public static bool ContainsAny(string value, params string[] values)
        {
            return ContainsAny(value, StringComparison.CurrentCulture, values);
        }

        /// <summary>
        /// Determines whether the string contains any of the provided values.
        /// </summary>
        /// <param name="this"></param>
        /// <param name="comparisonType"></param>
        /// <param name="values"></param>
        /// <returns></returns>
        public static bool ContainsAny(string inputValue, StringComparison comparisonType, params string[] values)
        {
            foreach (string value in values)
            {
                if (inputValue.IndexOf(value, comparisonType) > -1) return true;
            }
            return false;
        }

        /// <summary>
        /// Determines whether the string contains all of the provided values.
        /// </summary>
        /// <param name="this"></param>
        /// <param name="values"></param>
        /// <returns></returns>
        public static bool ContainsAll(string value, params string[] values)
        {
            return ContainsAll(value, StringComparison.CurrentCulture, values);
        }

        /// <summary>
        /// Determines whether the string contains all of the provided values.
        /// </summary>
        /// <param name="this"></param>
        /// <param name="comparisonType"></param>
        /// <param name="values"></param>
        /// <returns></returns>
        public static bool ContainsAll(string inputValue, StringComparison comparisonType, params string[] values)
        {
            foreach (string value in values)
            {
                if (inputValue.IndexOf(value, comparisonType) == -1) return false;
            }
            return true;
        }

        /// <summary>
        /// Determines whether the string is equal to any of the provided values.
        /// </summary>
        /// <param name="this"></param>
        /// <param name="comparisonType"></param>
        /// <param name="values"></param>
        /// <returns></returns>
        public static bool EqualsAny(string inputValue, StringComparison comparisonType, params string[] values)
        {
            foreach (string value in values)
            {
                if (inputValue.Equals(value, comparisonType)) return true;
            }
            return false;
        }

        /// <summary>
        /// 比较字符串是否相同
        /// </summary>
        /// <param name="value"></param>
        /// <param name="pattern"></param>
        /// <returns></returns>
        public static bool IsLike(string value, string pattern)
        {
            if (value == pattern) return true;
            if (string.IsNullOrEmpty(value)) return false;

            if (pattern[0] == '*' && pattern.Length > 1)
            {
                for (int index = 0; index < value.Length; index++)
                {
                    if (IsLike(value.Substring(index), pattern.Substring(1)))
                        return true;
                }
            }
            else if (pattern[0] == '*')
            {
                return true;
            }
            else if (pattern[0] == value[0])
            {
                return IsLike(value.Substring(1), pattern.Substring(1));
            }
            return false;
        }

        /// <summary>
        /// 截取字符串
        /// </summary>
        /// <param name="inputValue"></param>
        /// <param name="length"></param>
        /// <param name="useElipses"></param>
        /// <returns></returns>
        public static string Truncate(string inputValue, int length, bool useElipses = false)
        {
            int e = useElipses ? 3 : 0;
            if (length - e <= 0) throw new InvalidOperationException(string.Format("Length must be greater than {0}.", e));

            if (string.IsNullOrEmpty(inputValue) || inputValue.Length <= length) return inputValue;

            return inputValue.Substring(0, length) + new String('.', e);
        }

        /// <summary>
        /// 返回字符串真实长度, 1个汉字长度为2
        /// </summary>
        /// <returns>字符长度</returns>
        public static int GetStringLength(string value)
        {
            return Encoding.Default.GetBytes(value).Length;
        }

        public static string GetEmailHostName(string strEmail)
        {
            if (strEmail.IndexOf("@") < 0)
            {
                return string.Empty;
            }
            return strEmail.Substring(strEmail.LastIndexOf("@")).ToLower();
        }

        /// <summary>
        /// 返回 HTML 字符串的编码结果
        /// </summary>
        /// <param name="value">字符串</param>
        /// <returns>编码结果</returns>
        public static string HtmlEncode(string value)
        {
            return HttpUtility.HtmlEncode(value);
        }

        /// <summary>
        /// 返回 HTML 字符串的解码结果
        /// </summary>
        /// <param name="value">字符串</param>
        /// <returns>解码结果</returns>
        public static string HtmlDecode(string value)
        {
            return HttpUtility.HtmlDecode(value);
        }

        /// <summary>
        /// 返回 URL 字符串的编码结果
        /// </summary>
        /// <param name="value">字符串</param>
        /// <returns>编码结果</returns>
        public static string UrlEncode(string value)
        {
            //936 gb2312 简体中文 (GB2312)
            //950 big5 繁体中文 (Big5)
            return HttpUtility.UrlEncode(value);
        }

        public static string UrlGB2312Encode(string value)
        {
            //936 gb2312 简体中文 (GB2312)
            //950 big5 繁体中文 (Big5)
            return HttpUtility.UrlEncode(value, Encoding.GetEncoding(936));
        }

        public static string UrlBig5Encode(string value)
        {
            //936 gb2312 简体中文 (GB2312)
            //950 big5 繁体中文 (Big5)
            return HttpUtility.UrlEncode(value, Encoding.GetEncoding(950));
        }

        /// <summary>
        /// 返回 URL 字符串的编码结果
        /// </summary>
        /// <param name="value">字符串</param>
        /// <returns>编码结果</returns>
        public static string UrlEncode(string value, Encoding encoding)
        {
            //936 gb2312 简体中文 (GB2312)
            //950 big5 繁体中文 (Big5)
            return HttpUtility.UrlEncode(value, encoding);
        }

        /// <summary>
        /// 返回 URL 字符串的编码结果
        /// </summary>
        /// <param name="value">字符串</param>
        /// <returns>解码结果</returns>
        public static string UrlDecode(string value)
        {
            //936 gb2312 简体中文 (GB2312)
            //950 big5 繁体中文 (Big5)
            return HttpUtility.UrlDecode(value);
        }

        public static string UrlGB2312Decode(string value)
        {
            //936 gb2312 简体中文 (GB2312)
            //950 big5 繁体中文 (Big5)
            return HttpUtility.UrlDecode(value, Encoding.GetEncoding(936));
        }

        public static string UrlBig5Decode(string value)
        {
            //936 gb2312 简体中文 (GB2312)
            //950 big5 繁体中文 (Big5)
            return HttpUtility.UrlDecode(value, Encoding.GetEncoding(950));
        }

        /// <summary>
        /// 返回 URL 字符串的编码结果
        /// </summary>
        /// <param name="value">字符串</param>
        /// <returns>解码结果</returns>
        public static string UrlDecode(string value, Encoding encoding)
        {
            //936 gb2312 简体中文 (GB2312)
            //950 big5 繁体中文 (Big5)
            return HttpUtility.UrlDecode(value, encoding);
        }

        /// <summary>
        /// 将全角数字转换为数字
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string SBCCaseToNumberic(string value)
        {
            char[] c = value.ToCharArray();
            for (int i = 0; i < c.Length; i++)
            {
                byte[] b = System.Text.Encoding.Unicode.GetBytes(c, i, 1);
                if (b.Length == 2)
                {
                    if (b[1] == 255)
                    {
                        b[0] = (byte)(b[0] + 32);
                        b[1] = 0;
                        c[i] = System.Text.Encoding.Unicode.GetChars(b)[0];
                    }
                }
            }
            return new string(c);
        }

        #region 获取相应扩展名的ContentType类型
        public static string GetContentType(string fileExtName)
        {
            switch (fileExtName)
            {
                #region 常用文件类型
                case ".jpeg": return "image/jpeg";
                case ".jpg": return "image/jpeg";
                case ".js": return "application/x-javascript";
                case ".jsp": return "text/html";
                case ".gif": return "image/gif";
                case ".htm": return "text/html";
                case ".html": return "text/html";
                case ".asf": return "video/x-ms-asf";
                case ".avi": return "video/avi";
                case ".bmp": return "application/x-bmp";
                case ".asp": return "text/asp";
                case ".wma": return "audio/x-ms-wma";
                case ".wav": return "audio/wav";
                case ".wmv": return "video/x-ms-wmv";
                case ".ra": return "audio/vnd.rn-realaudio";
                case ".ram": return "audio/x-pn-realaudio";
                case ".rm": return "application/vnd.rn-realmedia";
                case ".rmvb": return "application/vnd.rn-realmedia-vbr";
                case ".xhtml": return "text/html";
                case ".png": return "image/png";
                case ".ppt": return "application/x-ppt";
                case ".tif": return "image/tiff";
                case ".tiff": return "image/tiff";
                case ".xls": return "application/x-xls";
                case ".xlw": return "application/x-xlw";
                case ".xml": return "text/xml";
                case ".xpl": return "audio/scpls";
                case ".swf": return "application/x-shockwave-flash";
                case ".torrent": return "application/x-bittorrent";
                case ".dll": return "application/x-msdownload";
                case ".asa": return "text/asa";
                case ".asx": return "video/x-ms-asf";
                case ".au": return "audio/basic";
                case ".css": return "text/css";
                case ".doc": return "application/msword";
                case ".exe": return "application/x-msdownload";
                case ".mp1": return "audio/mp1";
                case ".mp2": return "audio/mp2";
                case ".mp2v": return "video/mpeg";
                case ".mp3": return "audio/mp3";
                case ".mp4": return "video/mpeg4";
                case ".mpa": return "video/x-mpg";
                case ".mpd": return "application/vnd.ms-project";
                case ".mpe": return "video/x-mpeg";
                case ".mpeg": return "video/mpg";
                case ".mpg": return "video/mpg";
                case ".mpga": return "audio/rn-mpeg";
                case ".mpp": return "application/vnd.ms-project";
                case ".mps": return "video/x-mpeg";
                case ".mpt": return "application/vnd.ms-project";
                case ".mpv": return "video/mpg";
                case ".mpv2": return "video/mpeg";
                case ".wml": return "text/vnd.wap.wml";
                case ".wsdl": return "text/xml";
                case ".xsd": return "text/xml";
                case ".xsl": return "text/xml";
                case ".xslt": return "text/xml";
                case ".htc": return "text/x-component";
                case ".mdb": return "application/msaccess";
                case ".zip": return "application/zip";
                case ".rar": return "application/x-rar-compressed";
                #endregion

                case "*": return "application/octet-stream";
                case ".001": return "application/x-001";
                case ".301": return "application/x-301";
                case ".323": return "text/h323";
                case ".906": return "application/x-906";
                case ".907": return "drawing/907";
                case ".a11": return "application/x-a11";
                case ".acp": return "audio/x-mei-aac";
                case ".ai": return "application/postscript";
                case ".aif": return "audio/aiff";
                case ".aifc": return "audio/aiff";
                case ".aiff": return "audio/aiff";
                case ".anv": return "application/x-anv";
                case ".awf": return "application/vnd.adobe.workflow";
                case ".biz": return "text/xml";
                case ".bot": return "application/x-bot";
                case ".c4t": return "application/x-c4t";
                case ".c90": return "application/x-c90";
                case ".cal": return "application/x-cals";
                case ".cat": return "application/vnd.ms-pki.seccat";
                case ".cdf": return "application/x-netcdf";
                case ".cdr": return "application/x-cdr";
                case ".cel": return "application/x-cel";
                case ".cer": return "application/x-x509-ca-cert";
                case ".cg4": return "application/x-g4";
                case ".cgm": return "application/x-cgm";
                case ".cit": return "application/x-cit";
                case ".class": return "java/*";
                case ".cml": return "text/xml";
                case ".cmp": return "application/x-cmp";
                case ".cmx": return "application/x-cmx";
                case ".cot": return "application/x-cot";
                case ".crl": return "application/pkix-crl";
                case ".crt": return "application/x-x509-ca-cert";
                case ".csi": return "application/x-csi";
                case ".cut": return "application/x-cut";
                case ".dbf": return "application/x-dbf";
                case ".dbm": return "application/x-dbm";
                case ".dbx": return "application/x-dbx";
                case ".dcd": return "text/xml";
                case ".dcx": return "application/x-dcx";
                case ".der": return "application/x-x509-ca-cert";
                case ".dgn": return "application/x-dgn";
                case ".dib": return "application/x-dib";
                case ".dot": return "application/msword";
                case ".drw": return "application/x-drw";
                case ".dtd": return "text/xml";
                case ".dwf": return "application/x-dwf";
                case ".dwg": return "application/x-dwg";
                case ".dxb": return "application/x-dxb";
                case ".dxf": return "application/x-dxf";
                case ".edn": return "application/vnd.adobe.edn";
                case ".emf": return "application/x-emf";
                case ".eml": return "message/rfc822";
                case ".ent": return "text/xml";
                case ".epi": return "application/x-epi";
                case ".eps": return "application/x-ps";
                case ".etd": return "application/x-ebx";
                case ".fax": return "image/fax";
                case ".fdf": return "application/vnd.fdf";
                case ".fif": return "application/fractals";
                case ".fo": return "text/xml";
                case ".frm": return "application/x-frm";
                case ".g4": return "application/x-g4";
                case ".gbr": return "application/x-gbr";
                case ".gcd": return "application/x-gcd";

                case ".gl2": return "application/x-gl2";
                case ".gp4": return "application/x-gp4";
                case ".hgl": return "application/x-hgl";
                case ".hmr": return "application/x-hmr";
                case ".hpg": return "application/x-hpgl";
                case ".hpl": return "application/x-hpl";
                case ".hqx": return "application/mac-binhex40";
                case ".hrf": return "application/x-hrf";
                case ".hta": return "application/hta";
                case ".htt": return "text/webviewhtml";
                case ".htx": return "text/html";
                case ".icb": return "application/x-icb";
                case ".ico": return "application/x-ico";
                case ".iff": return "application/x-iff";
                case ".ig4": return "application/x-g4";
                case ".igs": return "application/x-igs";
                case ".iii": return "application/x-iphone";
                case ".img": return "application/x-img";
                case ".ins": return "application/x-internet-signup";
                case ".isp": return "application/x-internet-signup";
                case ".IVF": return "video/x-ivf";
                case ".java": return "java/*";
                case ".jfif": return "image/jpeg";
                case ".jpe": return "application/x-jpe";
                case ".la1": return "audio/x-liquid-file";
                case ".lar": return "application/x-laplayer-reg";
                case ".latex": return "application/x-latex";
                case ".lavs": return "audio/x-liquid-secure";
                case ".lbm": return "application/x-lbm";
                case ".lmsff": return "audio/x-la-lms";
                case ".ls": return "application/x-javascript";
                case ".ltr": return "application/x-ltr";
                case ".m1v": return "video/x-mpeg";
                case ".m2v": return "video/x-mpeg";
                case ".m3u": return "audio/mpegurl";
                case ".m4e": return "video/mpeg4";
                case ".mac": return "application/x-mac";
                case ".man": return "application/x-troff-man";
                case ".math": return "text/xml";
                case ".mfp": return "application/x-shockwave-flash";
                case ".mht": return "message/rfc822";
                case ".mhtml": return "message/rfc822";
                case ".mi": return "application/x-mi";
                case ".mid": return "audio/mid";
                case ".midi": return "audio/mid";
                case ".mil": return "application/x-mil";
                case ".mml": return "text/xml";
                case ".mnd": return "audio/x-musicnet-download";
                case ".mns": return "audio/x-musicnet-stream";
                case ".mocha": return "application/x-javascript";
                case ".movie": return "video/x-sgi-movie";
                case ".mpw": return "application/vnd.ms-project";
                case ".mpx": return "application/vnd.ms-project";
                case ".mtx": return "text/xml";
                case ".mxp": return "application/x-mmxp";
                case ".net": return "image/pnetvue";
                case ".nrf": return "application/x-nrf";
                case ".nws": return "message/rfc822";
                case ".odc": return "text/x-ms-odc";
                case ".out": return "application/x-out";
                case ".p10": return "application/pkcs10";
                case ".p12": return "application/x-pkcs12";
                case ".p7b": return "application/x-pkcs7-certificates";
                case ".p7c": return "application/pkcs7-mime";
                case ".p7m": return "application/pkcs7-mime";
                case ".p7r": return "application/x-pkcs7-certreqresp";
                case ".p7s": return "application/pkcs7-signature";
                case ".pc5": return "application/x-pc5";
                case ".pci": return "application/x-pci";
                case ".pcl": return "application/x-pcl";
                case ".pcx": return "application/x-pcx";
                case ".pdf": return "application/pdf";
                case ".pdx": return "application/vnd.adobe.pdx";
                case ".pfx": return "application/x-pkcs12";
                case ".pgl": return "application/x-pgl";
                case ".pic": return "application/x-pic";
                case ".pko": return "application/vnd.ms-pki.pko";
                case ".pl": return "application/x-perl";
                case ".plg": return "text/html";
                case ".pls": return "audio/scpls";
                case ".plt": return "application/x-plt";
                case ".pot": return "application/vnd.ms-powerpoint";
                case ".ppa": return "application/vnd.ms-powerpoint";
                case ".ppm": return "application/x-ppm";
                case ".pps": return "application/vnd.ms-powerpoint";
                case ".pr": return "application/x-pr";
                case ".prf": return "application/pics-rules";
                case ".prn": return "application/x-prn";
                case ".prt": return "application/x-prt";
                case ".ps": return "application/x-ps";
                case ".ptn": return "application/x-ptn";
                case ".pwz": return "application/vnd.ms-powerpoint";
                case ".r3t": return "text/vnd.rn-realtext3d";
                case ".ras": return "application/x-ras";
                case ".rat": return "application/rat-file";
                case ".rdf": return "text/xml";
                case ".rec": return "application/vnd.rn-recording";
                case ".red": return "application/x-red";
                case ".rgb": return "application/x-rgb";
                case ".rjs": return "application/vnd.rn-realsystem-rjs";
                case ".rjt": return "application/vnd.rn-realsystem-rjt";
                case ".rlc": return "application/x-rlc";
                case ".rle": return "application/x-rle";
                case ".rmf": return "application/vnd.adobe.rmf";
                case ".rmi": return "audio/mid";
                case ".rmj": return "application/vnd.rn-realsystem-rmj";
                case ".rmm": return "audio/x-pn-realaudio";
                case ".rmp": return "application/vnd.rn-rn_music_package";
                case ".rms": return "application/vnd.rn-realmedia-secure";
                case ".rmx": return "application/vnd.rn-realsystem-rmx";
                case ".rnx": return "application/vnd.rn-realplayer";
                case ".rp": return "image/vnd.rn-realpix";
                case ".rpm": return "audio/x-pn-realaudio-plugin";
                case ".rsml": return "application/vnd.rn-rsml";
                case ".rt": return "text/vnd.rn-realtext";
                case ".rtf": return "application/msword";
                case ".rv": return "video/vnd.rn-realvideo";
                case ".sam": return "application/x-sam";
                case ".sat": return "application/x-sat";
                case ".sdp": return "application/sdp";
                case ".sdw": return "application/x-sdw";
                case ".sit": return "application/x-stuffit";
                case ".slb": return "application/x-slb";
                case ".sld": return "application/x-sld";
                case ".slk": return "drawing/x-slk";
                case ".smi": return "application/smil";
                case ".smil": return "application/smil";
                case ".smk": return "application/x-smk";
                case ".snd": return "audio/basic";
                case ".sol": return "text/plain";
                case ".sor": return "text/plain";
                case ".spc": return "application/x-pkcs7-certificates";
                case ".spl": return "application/futuresplash";
                case ".spp": return "text/xml";
                case ".ssm": return "application/streamingmedia";
                case ".sst": return "application/vnd.ms-pki.certstore";
                case ".stl": return "application/vnd.ms-pki.stl";
                case ".stm": return "text/html";
                case ".sty": return "application/x-sty";
                case ".svg": return "text/xml";
                case ".tdf": return "application/x-tdf";
                case ".tg4": return "application/x-tg4";
                case ".tga": return "application/x-tga";
                case ".tld": return "text/xml";
                case ".top": return "drawing/x-top";
                case ".tsd": return "text/xml";
                case ".txt": return "text/plain";
                case ".uin": return "application/x-icq";
                case ".uls": return "text/iuls";
                case ".vcf": return "text/x-vcard";
                case ".vda": return "application/x-vda";
                case ".vdx": return "application/vnd.visio";
                case ".vml": return "text/xml";
                case ".vpg": return "application/x-vpeg005";
                case ".vsd": return "application/vnd.visio";
                case ".vss": return "application/vnd.visio";
                case ".vst": return "application/vnd.visio";
                case ".vsw": return "application/vnd.visio";
                case ".vsx": return "application/vnd.visio";
                case ".vtx": return "application/vnd.visio";
                case ".vxml": return "text/xml";
                case ".wax": return "audio/x-ms-wax";
                case ".wb1": return "application/x-wb1";
                case ".wb2": return "application/x-wb2";
                case ".wb3": return "application/x-wb3";
                case ".wbmp": return "image/vnd.wap.wbmp";
                case ".wiz": return "application/msword";
                case ".wk3": return "application/x-wk3";
                case ".wk4": return "application/x-wk4";
                case ".wkq": return "application/x-wkq";
                case ".wks": return "application/x-wks";
                case ".wm": return "video/x-ms-wm";
                case ".wmd": return "application/x-ms-wmd";
                case ".wmf": return "application/x-wmf";
                case ".wmx": return "video/x-ms-wmx";
                case ".wmz": return "application/x-ms-wmz";
                case ".wp6": return "application/x-wp6";
                case ".wpd": return "application/x-wpd";
                case ".wpg": return "application/x-wpg";
                case ".wpl": return "application/vnd.ms-wpl";
                case ".wq1": return "application/x-wq1";
                case ".wr1": return "application/x-wr1";
                case ".wri": return "application/x-wri";
                case ".wrk": return "application/x-wrk";
                case ".ws": return "application/x-ws";
                case ".ws2": return "application/x-ws";
                case ".wsc": return "text/scriptlet";
                case ".wvx": return "video/x-ms-wvx";
                case ".xdp": return "application/vnd.adobe.xdp";
                case ".xdr": return "text/xml";
                case ".xfd": return "application/vnd.adobe.xfd";
                case ".xfdf": return "application/vnd.adobe.xfdf";
                case ".xq": return "text/xml";
                case ".xql": return "text/xml";
                case ".xquery": return "text/xml";
                case ".xwd": return "application/x-xwd";
                case ".x_b": return "application/x-x_b";
                case ".x_t": return "application/x-x_t";
            }
            return "application/octet-stream";
        }
        #endregion

        /// <summary>
        /// 将对象序列化成Json后Base64加密
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string BuildBase64Body(object obj)
        {
            if (obj != null)
            {
                return StringHelper.EncodeBase64(JsonHelper.Serialize(obj));
            }
            return "";
        }
    }
}
