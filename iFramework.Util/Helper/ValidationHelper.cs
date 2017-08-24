using System;
using System.Text;
using System.Text.RegularExpressions;

namespace iFramework.Util
{
    /// <summary>
    /// 用于验证的公共类
    /// </summary>
    public class ValidationHelper
    {
        #region 验证对象是否为数值类型
        /// <summary>
        /// 判断对象是否为数值类型
        /// </summary>
        /// <param name="Expression"></param>
        /// <returns></returns>
        public static bool IsNumeric(object Expression)
        {
            string str = ConvertHelper.ToString(Expression);
            if (string.IsNullOrEmpty(str)) { return false; }
            //正数^[1-9]+[0-9]*[.]?[0-9]*$
            if (str.Length > 0 && Regex.IsMatch(str, @"^[-]?[0-9]*[.]?[0-9]*$"))
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// 判断对象是否为正数
        /// </summary>
        /// <param name="Expression"></param>
        /// <returns></returns>
        public static bool IsUNumeric(object Expression)
        {
            string str = ConvertHelper.ToString(Expression);
            if (string.IsNullOrEmpty(str)) { return false; }
            if (str.Length > 0 && Regex.IsMatch(str, @"^[1-9]+[0-9]*[.]?[0-9]*$"))
            {
                return true;
            }
            return false;
        }
        #endregion

        #region 验证是否为整数
        /// <summary>
        /// 验证是否为整数
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool IsInt(object obj)
        {
            string str = ConvertHelper.ToString(obj);
            if (string.IsNullOrEmpty(str)) { return false; }
            int outValue;
            if (int.TryParse(str, out outValue))
            {
                return true;
            }
            return false;
        }
        /// <summary>
        /// 验证是否为正整数
        /// </summary>
        public static bool IsInt(object str, bool Plus)
        {
            str = ConvertHelper.ToString(str);
            if (string.IsNullOrEmpty(ConvertHelper.ToString(str))) { return false; }
            int outValue;
            if (int.TryParse(ConvertHelper.ToString(str), out outValue) && outValue >= 0)
            {
                return true;
            }
            return false;
        }
        #endregion

        #region 验证是否为Double类型的数字
        /// <summary>
        /// 判断对象是否为Double类型的数字
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool IsDouble(object obj)
        {
            string str = ConvertHelper.ToString(obj);
            if (string.IsNullOrEmpty(str)) { return false; }
            double outValue;
            if (double.TryParse(str, out outValue))
            {
                return true;
            }
            return false;
        }
        /// <summary>
        /// 判断对象是否为正Double类型的数字
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool IsDouble(object str, bool Plus)
        {
            if (string.IsNullOrEmpty(ConvertHelper.ToString(str))) { return false; }
            double outValue;
            if (double.TryParse(ConvertHelper.ToString(str), out outValue) && outValue >= 0)
            {
                return true;
            }
            return false;
        }
        #endregion

        #region 验证是否为Decimal类型的数字
        /// <summary>
        /// 判断对象是否为Decimal类型的数字
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool IsDecimal(object obj)
        {
            string str = ConvertHelper.ToString(obj);
            if (string.IsNullOrEmpty(str)) { return false; }
            decimal outValue;
            if (decimal.TryParse(str, out outValue))
            {
                return true;
            }
            return false;
        }
        /// <summary>
        /// 判断对象是否为正Decimal类型的数字
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool IsDecimal(object str, bool Plus)
        {
            if (string.IsNullOrEmpty(ConvertHelper.ToString(str))) { return false; }
            decimal outValue;
            if (decimal.TryParse(ConvertHelper.ToString(str), out outValue) && outValue >= 0)
            {
                return true;
            }
            return false;
        }
        #endregion

        #region 验是否可以转换为日期和时间
        /// <summary>
        /// 验是否可以转换为日期和时间
        /// </summary>
        /// <param name="strDateTime"></param>
        /// <returns></returns>
        public static bool IsDateTime(object obj)
        {
            string str = ConvertHelper.ToString(obj);
            if (string.IsNullOrEmpty(str)) { return false; }
            DateTime outValue;
            if (DateTime.TryParse(str, out outValue))
            {
                return true;
            }
            return false;
        }
        #endregion

        #region 验证是否可以转换为日期
        /// <summary>
        /// 验证是否可以转换为日期
        /// </summary>
        /// <param name="strDateTime"></param>
        /// <returns></returns>
        public static bool IsDate(object obj)
        {
            string str = ConvertHelper.ToString(obj);
            if (string.IsNullOrEmpty(str)) { return false; }
            string[] dateFormat = { "yyyy-M-d", "yyyy-M-dd", "yyyy-MM-d", "yyyy-MM-dd", "yyyy/M/d", "yyyy/M/dd", "yyyy/MM/d", "yyyy/MM/dd" };
            foreach (string format in dateFormat)
            {
                try
                {
                    DateTime newDateTime;
                    newDateTime = DateTime.ParseExact(str, format, null);
                    return true;
                }
                catch
                {

                }
            }
            return false;
        }
        #endregion

        #region 验证是否可以转换为时间
        /// <summary>
        /// 验证是否可以转换为时间
        /// </summary>
        /// <param name="strDateTime"></param>
        /// <returns></returns>
        public static bool IsTime(object obj)
        {
            string str = ConvertHelper.ToString(obj);
            if (string.IsNullOrEmpty(str)) { return false; }
            try
            {
                DateTime newDateTime = DateTime.ParseExact(str, "HH:mm:ss", null);
                return true;
            }
            catch
            {
                return false;
            }
        }
        #endregion

        #region 验证是否可以转换为布尔值
        public static bool IsBool(object obj)
        {
            string str = ConvertHelper.ToString(obj);
            bool outValue;
            str = str == "1" ? "true" : str;
            str = str == "0" ? "false" : str;
            if (bool.TryParse(str, out outValue))
            {
                return true;
            }
            return false;
        }
        #endregion

        #region 验证是否可以转换为Guid类型
        /// <summary>
        /// 验证是否可以转换为Guid类型
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static bool IsGuid(object obj)
        {
            string str = ConvertHelper.ToString(obj);
            if (string.IsNullOrEmpty(str)) { return false; }
            try
            {
                Guid gd = new Guid(str);
                return true;
            }
            catch
            {
                return false;
            }
        }
        #endregion

        #region 验证所给定的字符串数组(strNumber)中的数据是不是都为数值型
        /// <summary>
        /// 判断给定的字符串数组(strNumber)中的数据是不是都为数值型
        /// </summary>
        /// <param name="strNumber">要确认的字符串数组</param>
        /// <returns>是则返加true 不是则返回 false</returns>
        public static bool IsNumericArray(string[] strNumber)
        {
            if (strNumber == null)
            {
                return false;
            }
            if (strNumber.Length < 1)
            {
                return false;
            }
            foreach (string id in strNumber)
            {
                if (!IsNumeric(id))
                {
                    return false;
                }
            }
            return true;
        }
        #endregion

        #region 验证注册用户名格式是否合法
        /// <summary>
        /// 验证注册用户名格式是否合法
        /// </summary>
        /// <param name="strUserName">用户名</param>
        /// <returns>判断结果</returns>
        public static bool IsUserName(string strUserName)
        {
            return Regex.IsMatch(strUserName, @"^[a-zA-Z]{1}[a-zA-Z0-9]{3,19}$");
        }
        #endregion

        #region 验证是否符合email格式
        /// <summary>
        /// 验证是否符合email格式
        /// </summary>
        /// <param name="strEmail">要判断的email字符串</param>
        /// <returns>判断结果</returns>
        public static bool IsEmail(string strEmail)
        {
            //^([0-9a-zA-Z]([-.\w]*[0-9a-zA-Z])*@([0-9a-zA-Z][-\w]*[0-9a-zA-Z]\.)+[a-zA-Z]{2,9})$
            return Regex.IsMatch(strEmail, @"^[a-zA-Z0-9]+([-+_\.][a-zA-Z0-9]+)*@[a-zA-Z0-9]+([-_\.][a-zA-Z0-9]+)*\.[a-zA-Z]{2,3}$");
        }
        #endregion

        #region 验证是否为IP
        /// <summary>
        /// 判断是否为IP
        /// </summary>
        /// <param name="ip"></param>
        /// <returns></returns>
        public static bool IsIP(string ip)
        {
            return Regex.IsMatch(ip, @"^((2[0-4]\d|25[0-5]|[01]?\d\d?)\.){3}(2[0-4]\d|25[0-5]|[01]?\d\d?)$");

        }
        #endregion

        #region 验证是否是Url
        /// <summary>
        /// 验证是否是正确的Url
        /// </summary>
        /// <param name="strUrl">要验证的Url</param>
        /// <returns>判断结果</returns>
        public static bool IsURL(string strUrl)
        {
            return Regex.IsMatch(strUrl, @"http(s)?://([\w-]+\.)+[\w-]+(/[\w- ./?%&=]*)?");
        }
        #endregion

        #region 验证是否有Sql危险字符
        /// <summary>
        /// 验证是否有Sql危险字符
        /// </summary>
        /// <param name="str">要判断字符串</param>
        /// <returns>存在危险字符返回false,反之为true</returns>
        public static bool IsSafeSqlString(string str)
        {
            return !Regex.IsMatch(str, @"[-|;|,|\/|\(|\)|\[|\]|\}|\{|%|@|\*|!|\']");
        }
        #endregion

        #region 验证是否是手机号码
        /// <summary>
        /// 验证是否是手机号码
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool IsMobile(string str)
        {
            return !string.IsNullOrEmpty(str) && new Regex(@"^0{0,1}1[3,5,7,8]{1}\d{9}$").IsMatch(str);
        }


        /// <summary>
        /// 验证是否是移动的手机号码
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool IsChinaMobile(string str)
        {
            return !string.IsNullOrEmpty(str) && new Regex(@"^(13[4-9]|15[0-2|7-9]|18[7|8])\d{8}$").IsMatch(str);
        }


        /// <summary>
        /// 验证是否是联通的手机号码
        /// </summary>
        public static bool IsUnicomMobile(string str)
        {
            return !string.IsNullOrEmpty(str) && new Regex(@"^(13[0-2]|15[3|5|6]|186)\d{8}$").IsMatch(str);
        }
        #endregion

        #region 验证是否为固定电话号码
        /// <summary>
        /// 验证是否为固定电话号码
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool IsTelephone(string str)
        {
            return Regex.IsMatch(str, @"(\(\d{3,4}\)|\d{3,4}-|\s)?\d{8}");
        }

        public static bool IsAllTelephone(string str)
        {
            return Regex.IsMatch(str, @"^(0{0,1}1[3,5,8,7]{1}[\d]{9})|(((400)-(\d{3})-(\d{4}))|^((\d{7,8})|(\d{4}|\d{3})-(\d{7,8})|(\d{4}|\d{3})-(\d{3,7,8})-(\d{4}|\d{3}|\d{2}|\d{1})|(\d{7,8})-(\d{4}|\d{3}|\d{2}|\d{1}))$)$");
        }
        #endregion

        #region 验证是否为邮政编码
        /// <summary>
        /// 验证是否为邮政编码
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool IsPostCode(string str)
        {
            return Regex.IsMatch(str, @"\d{6}");
        }
        #endregion

        #region 验证是否为身份证号码
        /// <summary>
        /// 验证是否为身份证号码
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool IsIDcard(string str)
        {
            //return Regex.IsMatch(str, @"\d{17}[\d|X]|\d{15}");
            //如果为空，认为验证合格
            if (string.IsNullOrWhiteSpace(str))
            {
                return false;
            }
            str = str.Trim();
            //模式字符串
            StringBuilder pattern = new StringBuilder();
            pattern.Append(@"^(11|12|13|14|15|21|22|23|31|32|33|34|35|36|37|41|42|43|44|45|46|");
            pattern.Append(@"50|51|52|53|54|61|62|63|64|65|71|81|82|91)");
            pattern.Append(@"(\d{13}|\d{15}[\dx])$");

            //验证
            return RegexHelper.Validate(str, pattern.ToString());
        }
        #endregion

        #region 验证是否为图片文件名
        /// <summary>
        /// 判断文件名是否为浏览器可以直接显示的图片文件名
        /// </summary>
        /// <param name="filename">文件名</param>
        /// <returns>是否可以直接显示</returns>
        public static bool IsImgFilename(string filename)
        {
            filename = filename.Trim();
            if (filename.EndsWith(".") || filename.IndexOf(".") == -1)
            {
                return false;
            }
            string extname = filename.Substring(filename.LastIndexOf(".") + 1).ToLower();
            return (extname == "jpg" || extname == "jpeg" || extname == "png" || extname == "bmp" || extname == "gif");
        }
        #endregion       

        #region 判断对象是否为空
        /// <summary>
        /// 判断对象是否为空，为空返回true
        /// </summary>
        /// <typeparam name="T">要验证的对象的类型</typeparam>
        /// <param name="data">要验证的对象</param>        
        public static bool IsNullOrEmpty<T>( T data )
        {
            //如果为null
            if ( data == null )
            {
                return true;
            }

            //如果为""
            if ( data.GetType() == typeof( String ) )
            {
                if ( string.IsNullOrEmpty( data.ToString().Trim() ) )
                {
                    return true;
                }
            }

            //如果为DBNull
            if ( data.GetType() == typeof( DBNull ) )
            {
                return true;
            }

            //不为空
            return false;
        }

        /// <summary>
        /// 判断对象是否为空，为空返回true
        /// </summary>
        /// <param name="data">要验证的对象</param>
        public static bool IsNullOrEmpty( object data )
        {
            //如果为null
            if ( data == null )
            {
                return true;
            }

            //如果为""
            if ( data.GetType() == typeof( String ) )
            {
                if ( string.IsNullOrEmpty( data.ToString().Trim() ) )
                {
                    return true;
                }
            }

            //如果为DBNull
            if ( data.GetType() == typeof( DBNull ) )
            {
                return true;
            }

            //不为空
            return false;
        }
        #endregion

        #region 检测客户的输入中是否有危险字符串
        /// <summary>
        /// 检测客户输入的字符串是否有效,并将原始字符串修改为有效字符串或空字符串。
        /// 当检测到客户的输入中有攻击性危险字符串,则返回false,有效返回true。
        /// </summary>
        /// <param name="input">要检测的字符串</param>
        public static bool IsValidInput( ref string input )
        {
            try
            {
                if ( IsNullOrEmpty( input ) )
                {
                    //如果是空值,则跳出
                    return true;
                }
                else
                {
                    //替换单引号
                    input = input.Replace( "'", "''" ).Trim();

                    //检测攻击性危险字符串
                    string testString = "and |or |exec |insert |select |delete |update |count |chr |mid |master |truncate |char |declare ";
                    string[] testArray = testString.Split( '|' );
                    foreach ( string testStr in testArray )
                    {
                        if ( input.ToLower().IndexOf( testStr ) != -1 )
                        {
                            //检测到攻击字符串,清空传入的值
                            input = "";
                            return false;
                        }
                    }

                    //未检测到攻击字符串
                    return true;
                }
            }
            catch ( Exception ex )
            {
                return false;
            }
        }
        #endregion
    }
}
