using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using iiERP.Tools;

namespace iFramework.Util
{
    /// <summary>
    /// 类    名：常用转换<br/>
    /// 功能说明：常用转换<br/>
    /// 作    者：易小辉<br/>
    /// 创建时间：2012-01-11<br/>
    /// 最后修改：<br/>
    /// </summary>
    public class ConvertHelper
    {
        #region 基本数据类型转换
        /// <summary>
        /// 转换成字符串
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string ToString(object obj)
        {
            if (obj != null && obj != DBNull.Value)
            {
                return obj.ToString();
            }
            return string.Empty;
        }
        /// <summary>
        /// 转换成Unicode字符
        /// </summary>
        /// <param name="obj"></param>
        /// <returns>返回unicode字符或最小char值</returns>
        public static char ToChar(object obj)
        {
            char c;
            if (!char.TryParse(obj.ToString(), out c))
            {
                return char.MinValue;
            }
            return c;
        }
        /// <summary>
        /// 转换成Int整数
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static int ToInt(object obj)
        {
            int outValue = (int)ToDecimal(obj);
            return outValue;
        }
        /// <summary>
        /// 转换成Long型
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static long ToLong(object obj)
        {
            long outValue = (long)ToDecimal(obj);
            return outValue;
        }
        /// <summary>
        /// 转换成64位整数
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static Int64 ToInt64(object obj)
        {
            Int64 outValue = (Int64)ToDecimal(obj);
            return outValue;
        }
        /// <summary>
        /// 转换成无符号整数
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static uint ToUInt(object obj)
        {
            uint outValue = (uint)ToDecimal(obj);
            return outValue;
        }
        /// <summary>
        /// 转换成64位无符号整数
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static UInt64 ToUInt64(object obj)
        {
            UInt64 outValue = (UInt64)ToDecimal(obj);
            return outValue;
        }
        /// <summary>
        /// 转换成double类型（双精度浮点数字）
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static double ToDouble(object obj)
        {
            double outValue = 0;
            if (!double.TryParse(ToString(obj), out outValue))
            {
                outValue = 0;
            }
            return outValue;
        }
        /// <summary>
        /// 转换成decimal类型
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static decimal ToDecimal(object obj)
        {
            decimal outValue = 0;
            if (!decimal.TryParse(ToString(obj), out outValue))
            {
                outValue = 0;
            }
            return outValue;
        }
        /// <summary>
        /// 转换成float(单精度浮点数字)
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static float ToFloat(object obj)
        {
            float outValue = 0;
            if (!float.TryParse(ToString(obj), out outValue))
            {
                outValue = 0;
            }
            return outValue;
        }
        /// <summary>
        /// 转换成Single(单精度浮点数字)
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static Single ToSingle(object obj)
        {
            Single outValue = 0;
            if (!Single.TryParse(ToString(obj), out outValue))
            {
                outValue = 0;
            }
            return outValue;
        }
        /// <summary>
        /// 转换成bool类型数据
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static bool ToBool(object obj)
        {
            bool outValue = false;
            string str = ToString(obj);
            str = str == "1" ? "true" : str;
            str = str == "0" ? "false" : str;
            if (!bool.TryParse(str, out outValue))
            {
                outValue = false;
            }
            return outValue;
        }

        /// <summary>
        /// 获取字节
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static byte ToByte(object obj)
        {
            byte outValue = 0;
            if (byte.TryParse(ToString(obj), out outValue))
            {
                return outValue;
            }
            else
            {
                return 0;
            }
        }
        /// <summary>
        /// 获取byte数组
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static Byte[] ToBytes(object obj)
        {
            if (!string.IsNullOrEmpty(ToString(obj)))
            {
                return (Byte[])obj;
            }
            else
                return null;
        }
        /// <summary>
        /// 转换成日期时间
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static DateTime ToDateTime(object obj, DateTime? defaultValue = null)
        {
            DateTime outValue;
            if (!DateTime.TryParse(ToString(obj), out outValue))
            {
                if (defaultValue == null)
                {
                    return HMTDateTime.Now;
                }
                else
                {
                    return (DateTime)defaultValue;
                }
            }
            return outValue;
        }
        /// <summary>
        /// 转换成日期时间
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="isNullable">如果为true,当obj不能转换为datetime时返回null</param>
        /// <returns></returns>
        public static DateTime? ToDateTime(object obj, bool isNullable)
        {
            DateTime outValue;
            if (!DateTime.TryParse(ToString(obj), out outValue))
            {
                if (isNullable == true)
                {
                    return null;
                }
                else
                {
                    return HMTDateTime.Now;
                }
            }
            return outValue;
        }

        /// <summary>
        /// 转换成日期
        /// </summary>
        /// <param name="obj"></param>
        /// <returns>返回日期部分</returns>
        public static DateTime ToDate(object obj)
        {
            DateTime outValue;
            if (!DateTime.TryParse(ToString(obj), out outValue))
            {
                DateTime t = HMTDateTime.Now;
                return new DateTime(t.Year, t.Month, t.Day);
            }
            return new DateTime(outValue.Year, outValue.Month, outValue.Day);
        }

        /// <summary>
        /// 转换成日期或null
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="isNullable">如果为true,当obj不能转换为datetime时返回null</param>
        /// <returns>返回日期部分或null</returns>
        public static DateTime? ToDate(object obj, bool isNullable)
        {
            DateTime outValue;
            if (!DateTime.TryParse(ToString(obj), out outValue))
            {
                if (isNullable == true)
                {
                    return null;
                }
                else
                {
                    DateTime t = HMTDateTime.Now;
                    return new DateTime(t.Year, t.Month, t.Day);
                }
            }
            return new DateTime(outValue.Year, outValue.Month, outValue.Day);
        }

        /// <summary>
        /// 转换成日期时间字符串
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static string ToDateTimeString(object obj,string formate="yyyy-MM-dd HH:mm:ss")
        {
            if (!ValidationHelper.IsDateTime(ToString(obj)))
            {
                return string.Empty;
            }
            else
            {
                formate = formate.Replace(@"/", @"\/");
                return Convert.ToDateTime(obj).ToString(formate);
            }
        }
        /// <summary>
        /// 转换成短日期字符串
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static string ToDateString(object obj,string formate="yyyy-MM-dd")
        {
            if (!ValidationHelper.IsDateTime(ToString(obj)))
            {
                return string.Empty;
            }
            else
            {
                formate = formate.Replace(@"/", @"\/");
                return Convert.ToDateTime(obj).ToString(formate);
            }
        }
        /// <summary>
        /// 转换成时间字符串
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static string ToTimeString(object obj)
        {
            if (!ValidationHelper.IsDateTime(ToString(obj)))
            {
                return string.Empty;
            }
            else
            {
                return Convert.ToDateTime(obj).ToString("HH:mm:ss");
            }
        }

        /// <summary>
        /// json to datetime
        /// </summary>
        /// <param name="jsonDate"></param>
        /// <returns></returns>
        public static DateTime JsonToDateTime(string jsonDate)
        {
            string value = jsonDate.Substring(6, jsonDate.Length - 8);
            DateTimeKind kind = DateTimeKind.Utc;
            int index = value.IndexOf('+', 1);
            if (index == -1)
                index = value.IndexOf('-', 1);
            if (index != -1)
            {
                kind = DateTimeKind.Local;
                value = value.Substring(0, index);
            }
            long javaScriptTicks = long.Parse(value, System.Globalization.NumberStyles.Integer, System.Globalization.CultureInfo.InvariantCulture);
            long InitialJavaScriptDateTicks = (new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)).Ticks;
            DateTime utcDateTime = new DateTime((javaScriptTicks * 10000) + InitialJavaScriptDateTicks, DateTimeKind.Utc);
            DateTime dateTime;
            switch (kind)
            {
                case DateTimeKind.Unspecified:
                    dateTime = DateTime.SpecifyKind(utcDateTime.ToLocalTime(), DateTimeKind.Unspecified);
                    break;
                case DateTimeKind.Local:
                    dateTime = utcDateTime.ToLocalTime();
                    break;
                default:
                    dateTime = utcDateTime;
                    break;
            }
            return dateTime;
        }

        /// <summary>
        /// 转换成Guid
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static Guid ToGuid(object obj)
        {
            Guid result;
            if (!Guid.TryParse(ToString(obj), out result))
            {
                return Guid.Empty;
            }
            return result;
        }

        /// <summary>
        /// 转换成Guid
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="isNullable">如果为true,当obj不能转换为Guid类型时返回null</param>
        /// <returns></returns>
        public static Guid? ToGuid(object obj, bool isNullable)
        {
            Guid result;
            if (!Guid.TryParse(ToString(obj), out result))
            {
                if (isNullable)
                {
                    return null;
                }
                return Guid.Empty;
            }
            return result;
        }
        #endregion

        #region 获取整数或小数部分
        /// <summary>
        /// 返回整数部分
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static int ToTruncate(object obj)
        {
            return (int)Math.Truncate(ToDecimal(obj));
        }
        /// <summary>
        /// 返回小数部分
        /// </summary>
        /// <param name="obj"></param>
        /// <returns>返回小数位</returns>
        public static decimal ToTrunDecimal(object obj)
        {
            decimal d = ToDecimal(obj);
            decimal outValue = d - Math.Truncate(d);
            return outValue;
        }
        /// <summary>
        /// 获取小数部分字符串
        /// </summary>
        /// <param name="obj"></param>
        /// <returns>返回小数位的字符串</returns>
        public static string ToTrunDecimalString(object obj)
        {
            string s = ToString(obj);
            string[] ss = s.Split('.');
            if (ss.Length <= 1) return "";
            return ss[1];
        }
        #endregion

        #region 数组转换
        /// <summary>
        /// 字符型数组转整型数组
        /// </summary>
        /// 
        public static int[] StrArrayToIntArray(string[] strArray)
        {
            int[] intArray = new int[strArray.Length];
            for (int i = 0; i < strArray.Length; i++)
            {
                int.TryParse(strArray[i], out intArray[i]);
            }
            return intArray;
        }

        /// <summary>
        /// 整型数组转字符型数组
        /// </summary>
        /// 
        public static string[] IntArrayToStrArray(int[] intArray)
        {
            string[] strArray = new string[intArray.Length];
            for (int i = 0; i < intArray.Length; i++)
            {
                strArray[i] = intArray[i].ToString();
            }
            return strArray;
        }
        #endregion

        #region IP转换
        /// <summary>
        /// IP地址类型转换，从string到int
        /// </summary>
        /// <param name="IP"></param>
        /// <returns></returns>
        public static int IPToInt(string IP)
        {
            return BitConverter.ToInt32(IPAddress.Parse(IP).GetAddressBytes(), 0);
        }
        /// <summary>
        /// IP地址类型转换，从int到string
        /// </summary>
        /// <param name="IP"></param>
        /// <returns></returns>
        public static string IntToIP(int IP)
        {
            byte[] ipBytes = new byte[8];
            BitConverter.GetBytes(IP).CopyTo(ipBytes, 0);

            IPAddress myIP = new IPAddress(BitConverter.ToInt64(ipBytes, 0));
            return myIP.ToString();
        }
        #endregion

        #region 转换人民币大写金额
        /// <summary>
        /// 转换人民币大写金额
        /// </summary>
        /// <param name="numstr"></param>
        /// <returns></returns>
        public string CurrencyToUpper(string numstr)
        {
            try
            {
                decimal num = Convert.ToDecimal(numstr);
                return CurrencyToUpper(num);
            }
            catch
            {
                return "非数字形式！";
            }
        }
        /// <summary>
        /// 转换人民币大写金额
        /// </summary>
        /// <param name="numint"></param>
        /// <returns></returns>
        public string ToCurrencyUpper(int numint)
        {
            try
            {
                decimal num = Convert.ToDecimal(numint);
                return CurrencyToUpper(num);
            }
            catch
            {
                return "非数字形式！";
            }
        }
        /// <summary> 
        /// 转换人民币大写金额 
        /// </summary> 
        /// <param name="num">金额</param> 
        /// <returns>返回大写形式</returns> 
        public string CurrencyToUpper(decimal num)
        {
            string strUpperMum = "零壹贰叁肆伍陆柒捌玖";            //0-9所对应的汉字 
            string strNumUnit = "万仟佰拾亿仟佰拾万仟佰拾元角分"; //数字位所对应的汉字 
            string strOfNum = "";    //从原num值中取出的值 
            string strNum = "";    //数字的字符串形式 
            string strReturnUpper = "";  //人民币大写金额形式 
            int i;    //循环变量 
            int sumLength;    //num的值乘以100的字符串长度 
            string ch1 = "";    //数字的汉语读法 
            string ch2 = "";    //数字位的汉字读法 
            int nzero = 0;  //用来计算连续的零值是几个 
            int temp;            //从原num值中取出的值 

            num = Math.Round(Math.Abs(num), 2);    //将num取绝对值并四舍五入取2位小数 
            strNum = ((long)(num * 100)).ToString();        //将num乘100并转换成字符串形式 
            sumLength = strNum.Length;      //找出最高位 
            if (sumLength > 15) { return "溢出"; }
            strNumUnit = strNumUnit.Substring(15 - sumLength);   //取出对应位数的strNumUnit的值。如：200.55,sumLength为5所以strNumUnit=佰拾元角分 

            //循环取出每一位需要转换的值 
            for (i = 0; i < sumLength; i++)
            {
                strOfNum = strNum.Substring(i, 1);          //取出需转换的某一位的值 
                temp = Convert.ToInt32(strOfNum);      //转换为数字 
                if (i != (sumLength - 3) && i != (sumLength - 7) && i != (sumLength - 11) && i != (sumLength - 15))
                {
                    //当所取位数不为元、万、亿、万亿上的数字时 
                    if (strOfNum == "0")
                    {
                        ch1 = "";
                        ch2 = "";
                        nzero = nzero + 1;
                    }
                    else
                    {
                        if (strOfNum != "0" && nzero != 0)
                        {
                            ch1 = "零" + strUpperMum.Substring(temp * 1, 1);
                            ch2 = strNumUnit.Substring(i, 1);
                            nzero = 0;
                        }
                        else
                        {
                            ch1 = strUpperMum.Substring(temp * 1, 1);
                            ch2 = strNumUnit.Substring(i, 1);
                            nzero = 0;
                        }
                    }
                }
                else
                {
                    //该位是万亿，亿，万，元位等关键位 
                    if (strOfNum != "0" && nzero != 0)
                    {
                        ch1 = "零" + strUpperMum.Substring(temp * 1, 1);
                        ch2 = strNumUnit.Substring(i, 1);
                        nzero = 0;
                    }
                    else
                    {
                        if (strOfNum != "0" && nzero == 0)
                        {
                            ch1 = strUpperMum.Substring(temp * 1, 1);
                            ch2 = strNumUnit.Substring(i, 1);
                            nzero = 0;
                        }
                        else
                        {
                            if (strOfNum == "0" && nzero >= 3)
                            {
                                ch1 = "";
                                ch2 = "";
                                nzero = nzero + 1;
                            }
                            else
                            {
                                if (sumLength >= 11)
                                {
                                    ch1 = "";
                                    nzero = nzero + 1;
                                }
                                else
                                {
                                    ch1 = "";
                                    ch2 = strNumUnit.Substring(i, 1);
                                    nzero = nzero + 1;
                                }
                            }
                        }
                    }
                }
                if (i == (sumLength - 11) || i == (sumLength - 3))
                {
                    //如果该位是亿位或元位，则必须写上 
                    ch2 = strNumUnit.Substring(i, 1);
                }
                strReturnUpper = strReturnUpper + ch1 + ch2;

                if (i == sumLength - 1 && strOfNum == "0")
                {
                    //最后一位（分）为0时，加上“整” 
                    strReturnUpper = strReturnUpper + '整';
                }
            }
            if (num == 0)
            {
                strReturnUpper = "零元整";
            }
            return strReturnUpper;
        }

        #endregion

        #region 数字转换为大写
        /// <summary>
        /// 数字转换为大写
        /// </summary>
        /// <param name="num"></param>
        /// <returns></returns>
        public string NumberToUpper(int num)
        {
            String str = num.ToString();
            string rstr = "";
            int n;
            for (int i = 0; i < str.Length; i++)
            {
                n = Convert.ToInt16(str[i].ToString());//char转数字,转换为字符串，再转数字
                switch (n)
                {
                    case 0: rstr = rstr + "〇"; break;
                    case 1: rstr = rstr + "一"; break;
                    case 2: rstr = rstr + "二"; break;
                    case 3: rstr = rstr + "三"; break;
                    case 4: rstr = rstr + "四"; break;
                    case 5: rstr = rstr + "五"; break;
                    case 6: rstr = rstr + "六"; break;
                    case 7: rstr = rstr + "七"; break;
                    case 8: rstr = rstr + "八"; break;
                    default: rstr = rstr + "九"; break;


                }

            }
            return rstr;
        }
        #endregion

        #region 月转化为大写
        //月转化为大写
        public string MonthToUpper(int month)
        {
            if (month < 10)
            {
                return NumberToUpper(month);
            }
            else
                if (month == 10) { return "十"; }

                else
                {
                    return "十" + NumberToUpper(month - 10);
                }
        }
        #endregion

        #region 日转化为大写
        //日转化为大写
        public string DayToUpper(int day)
        {
            if (day < 20)
            {
                return MonthToUpper(day);
            }
            else
            {
                String str = day.ToString();
                if (str[1] == '0')
                {
                    return NumberToUpper(Convert.ToInt16(str[0].ToString())) + "十";

                }


                else
                {
                    return NumberToUpper(Convert.ToInt16(str[0].ToString())) + "十"
                        + NumberToUpper(Convert.ToInt16(str[1].ToString()));
                }
            }
        }
        #endregion

        #region 日期转换为大写
        //日期转换为大写
        public string DateToUpper(System.DateTime date)
        {
            int year = date.Year;
            int month = date.Month;
            int day = date.Day;
            return NumberToUpper(year) + "年" + MonthToUpper(month) + "月" + DayToUpper(day) + "日";

        }
        #endregion

        #region 补足位数
        /// <summary>
        /// 指定字符串的固定长度，如果字符串小于固定长度，
        /// 则在字符串的前面补足零，可设置的固定长度最大为9位
        /// </summary>
        /// <param name="text">原始字符串</param>
        /// <param name="limitedLength">字符串的固定长度</param>
        public static string RepairZero(string text, int limitedLength)
        {
            //补足0的字符串
            string temp = "";

            //补足0
            for (int i = 0; i < limitedLength - text.Length; i++)
            {
                temp += "0";
            }

            //连接text
            temp += text;

            //返回补足0的字符串
            return temp;
        }
        #endregion

        #region 各进制数间转换
        /// <summary>
        /// 实现各进制数间的转换。ConvertBase("15",10,16)表示将十进制数15转换为16进制的数。
        /// </summary>
        /// <param name="value">要转换的值,即原值</param>
        /// <param name="from">原值的进制,只能是2,8,10,16四个值。</param>
        /// <param name="to">要转换到的目标进制，只能是2,8,10,16四个值。</param>
        public static string ConvertBase(string value, int from, int to)
        {
            try
            {
                int intValue = Convert.ToInt32(value, from);  //先转成10进制
                string result = Convert.ToString(intValue, to);  //再转成目标进制
                if (to == 2)
                {
                    int resultLength = result.Length;  //获取二进制的长度
                    switch (resultLength)
                    {
                        case 7:
                            result = "0" + result;
                            break;
                        case 6:
                            result = "00" + result;
                            break;
                        case 5:
                            result = "000" + result;
                            break;
                        case 4:
                            result = "0000" + result;
                            break;
                        case 3:
                            result = "00000" + result;
                            break;
                    }
                }
                return result;
            }
            catch (Exception ex)
            {
                return "0";
            }
        }
        #endregion

        #region 将byte[]转换成int
        /// <summary>
        /// 将byte[]转换成int
        /// </summary>
        /// <param name="data">需要转换成整数的byte数组</param>
        public static int BytesToInt32(byte[] data)
        {
            //如果传入的字节数组长度小于4,则返回0
            if (data.Length < 4)
            {
                return 0;
            }

            //定义要返回的整数
            int num = 0;

            //如果传入的字节数组长度大于4,需要进行处理
            if (data.Length >= 4)
            {
                //创建一个临时缓冲区
                byte[] tempBuffer = new byte[4];

                //将传入的字节数组的前4个字节复制到临时缓冲区
                Buffer.BlockCopy(data, 0, tempBuffer, 0, 4);

                //将临时缓冲区的值转换成整数，并赋给num
                num = BitConverter.ToInt32(tempBuffer, 0);
            }

            //返回整数
            return num;
        }
        #endregion
      
    }
}
