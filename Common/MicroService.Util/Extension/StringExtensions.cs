using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace iFramework.Util
{
    public static class StringExtensions
    {
        /// <summary>
        /// 字符分割Added By Albert.tian on 20141216
        /// </summary>
        /// <param name="Value"></param>
        /// <param name="RemoveEmpty"></param>
        /// <param name="Separators"></param>
        /// <returns></returns>
        public static string[] Split(this string Value, bool RemoveEmpty = true, string Separator = ",", params string[] Separators)
        {
            var theSeparators = new List<string>();
            theSeparators.Add(Separator);
            if (Separators != null)
            {
                theSeparators.AddRange(Separators);
            }
            if (string.IsNullOrEmpty(Value))
            {
                return new string[0];
            }
            if (RemoveEmpty)
            {
                return Value.Split(theSeparators.ToArray(), StringSplitOptions.RemoveEmptyEntries);
            }
            else
            {
                return Value.Split(theSeparators.ToArray(), StringSplitOptions.None);
            }
        }
       

        public static int LengthB(this string Value, Encoding Encode=null)
        {
            if (string.IsNullOrEmpty(Value))
            {
                return 0;
            }
            var theEncoding = Encode;
            if (theEncoding == null)
            {
                theEncoding = Encoding.Default;
            }
            if (theEncoding != null)
            {
                return theEncoding.GetByteCount(Value);
            }
            return 0;
        }
        public static string SubstringB(this string Value, int Start, int LengthB, Encoding Encode)
        {
            if (string.IsNullOrEmpty(Value))
            {
                return "";
            }
            var theRet = "";
            int theStrLen = Value.Length;
            int theRetLen = 0;
            for (int i = Start; i < theStrLen; i++)
            {
                var theChar = Value[i];
                var theLen = Encode.GetByteCount(new char[] { theChar });
                if (theRetLen + theLen > LengthB)
                {
                    break;
                }
                theRetLen += theLen;
                theRet = theChar + theRet;
            }
            return theRet;
        }
        /// <summary>
        /// 从后向前截取字符串，按字节截取，注意为了保护完整性，因编码差异，
        /// 其结果字节数不一定等于LengthB。
        /// </summary>
        /// <param name="Value">字符串</param>
        /// <param name="LengthB">截取的字节长度</param>
        /// <param name="Encode">字符串的字符编码</param>
        /// <returns>截取后的字符串，注意编码未变</returns>
        public static string RightB(this string Value, int LengthB, Encoding Encode)
        {
            if (string.IsNullOrEmpty(Value))
            {
                return "";
            }
            var theRet = "";
            int theStrLen = Value.Length;
            int theRetLen = 0;
            for (int i = theStrLen - 1; i >= 0; i--)
            {
                var theChar = Value[i];
                var theLen = Encode.GetByteCount(new char[] { theChar });
                if (theRetLen + theLen > LengthB)
                {
                    break;
                }
                theRetLen += theLen;
                theRet = theChar + theRet;
            }
            return theRet;
        }
        public static string ToSplitString<T>(this IEnumerable<T> List, string SplitChar = ",", bool WrapBy = false, string WrapByStr = "'")
        {
            if (List == null)
            {
                return "";
            }
            var theWrapStr = "";
            if (WrapBy)
            {
                theWrapStr = WrapByStr;
            }
            var theResult = "";
            foreach (var theT in List)
            {
                if (theResult == "")
                {
                    theResult = theWrapStr + theT.ToString() + theWrapStr;
                }
                else
                {
                    theResult += SplitChar + theWrapStr + theT.ToString() + theWrapStr;
                }
            }
            return theResult;
        }

    }
}
