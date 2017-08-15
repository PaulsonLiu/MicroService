using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace iFramework.Util
{
    public static class HMTEnumExtendtions
    {
        /// <summary>
        /// 把枚举类型转换为多语言
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="enumValue">枚举值</param>
        /// <param name="langValueFunc">多语言函数</param>
        /// <returns></returns>
        public static string ToLangString<T>(this T enumValue,Func<string,string>langValueFunc)
        {
            if (typeof(T).IsEnum )
            {
                if (langValueFunc != null)
                {
                    var values = enumValue.ToString().Split(new char[] { ',' }).Select(m => langValueFunc(m)); ;
                    return string.Join(",", values.ToArray());
                }
                else {
                    return enumValue.ToString();
                }
            }

            if (langValueFunc != null)
            {
                return langValueFunc(enumValue.ToString());
            }
            return null;
        }
    }
}
