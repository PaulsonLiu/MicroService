using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace iFramework.Util
{
    /// <summary>
    /// 字典的扩展方法
    /// </summary>
    public static class DictionaryExtentions
    {
        /// <summary>
        /// 新增并覆盖已经存在的
        /// </summary>
        /// <typeparam name="TKey"></typeparam>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="dictionary"></param>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public static void AddOverride<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key, TValue value)
        {
            if (dictionary != null)
            {
                if (dictionary.ContainsKey(key))
                {
                    dictionary[key] = value;
                }
                else
                {
                    dictionary.Add(key, value);
                }
            }
        }

        /// <summary>
        /// 新增单不覆盖
        /// </summary>
        /// <typeparam name="TKey"></typeparam>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="dictionary"></param>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public static void AddIfNotExists<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key, TValue value)
        {
            if (dictionary != null)
            {
                if (dictionary.ContainsKey(key) == false)
                {
                    dictionary.Add(key, value);
                }
            }

        }

        /// <summary>
        /// 不存在不为空覆盖
        /// </summary>
        /// <typeparam name="TKey"></typeparam>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="dictionary"></param>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public static void AddIfNotExistsAndNotEmpty<TKey>(this IDictionary<TKey, string> dictionary, TKey key, string value)
        {
            if (dictionary != null)
            {
                if (dictionary.ContainsKey(key) == false)
                {
                    dictionary.Add(key, value);
                }
                if (dictionary.ContainsKey(key) && string.IsNullOrWhiteSpace(dictionary[key]))
                {
                    dictionary[key] = value;
                }
            }

        }
    }
}
