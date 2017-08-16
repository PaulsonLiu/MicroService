using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace iFramework.Util
{
    public static class IEnumeableExtensions
    {
        /// <summary>
        /// 驱除空值并返回
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <param name="source"></param>
        /// <returns></returns>
        public static IEnumerable<TSource> ExceptEmpty<TSource>(this IEnumerable<TSource> source)
        {
            foreach (var item in source)
            {
                if (item is string)
                {
                    if(string.IsNullOrWhiteSpace(item.ToString())==false)
                    {
                        yield return item;
                    }
                }
                if (item != null)
                {
                    yield return item;
                }
            }
        }
      
        /// <summary>
       /// 遍历列表并执行方法
       /// </summary>
       /// <typeparam name="TSource"></typeparam>
       /// <param name="source"></param>
       /// <param name="action"></param>
        public static IEnumerable<TSource> Foreach<TSource>(this IEnumerable<TSource> source, Action<TSource> action)
        {
            if (source != null && action != null)
            {
                foreach (var item in source)
                {
                    action(item);
                }
            }
            return source;
 
        }
        /// <summary>
        /// 不为空且有值
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <param name="source"></param>
        /// <returns></returns>
        public static bool NotNullOrEmpty<TSource>(this IEnumerable<TSource> source)
        {
            if (source != null && source.Count() > 0)
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        /// <summary>
        /// 去掉重复值
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <param name="source"></param>
        /// <param name="keyFun"></param>
        /// <returns></returns>
        public static IEnumerable<TSource> DistinctByKey<TSource>(this IEnumerable<TSource> source, Func<TSource, object> keyFun)
        {
            HashSet<object> keySet = new HashSet<object>();
            foreach (var item in source)
            {
                object curKey = item;
                if (keyFun != null)
                {
                    curKey = keyFun(item);
                }
                if (keySet.Contains(curKey))
                {
                    continue;
                }
                else
                {
                    keySet.Add(curKey);
                    yield return item;
                }
            }

        }

        /// <summary>
        /// 删除符合条件的项
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <param name="source"></param>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public static void Remove<TSource>(this IList<TSource> source, Func<TSource, bool> predicate)
        {
            if (source != null)
            {
                var removeList = source.Where(predicate).ToArray();
                foreach (var item in removeList)
                {
                    source.Remove(item);
                }

            }

        }
        public static void UnionAddRang<T>(this List<T> list, IEnumerable<T> source, Func<T, T, bool> Comparer)
        {
            if (source == null)
            {
                return;
            }
            if (list == null)
            {
                list = new List<T>();
            }
            foreach (var sitem in source)
            {
                if (Comparer != null)
                {
                    bool theFind = false;
                    foreach (var ritem in list)
                    {
                        if (Comparer(sitem, ritem) == true)
                        {
                            theFind = true;
                            break;
                        }
                    }
                    if (theFind == false)
                    {
                        list.Add(sitem);
                    }
                }
                else
                {
                    list.Add(sitem);
                }
            }
        }
    }
}