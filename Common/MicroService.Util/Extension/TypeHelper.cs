using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace iFramework.Util
{
    public static class TypeHelper
    {
        /// <summary>
        /// 根据属性名获取属性值，注意只适合非集合类属性.
        /// </summary>
        /// <param name="obj">对象</param>
        /// <param name="PropertyName">属性</param>
        /// <returns>属性值或者为空.</returns>
        public static Type GetTypeByPropertyName(this Type obj, string PropertyName)
        {
            if (obj == null)
            {
                return null;
            }
            if (string.IsNullOrEmpty(PropertyName))
            {
                return null;
            }
            System.Reflection.PropertyInfo pInfo = obj.GetProperty(PropertyName);
            if (pInfo != null)
            {
                return pInfo.PropertyType;
            }
            return null;
        }
        public static bool IsDigit(Type type)
        {
            if (!type.IsValueType)
            {
                return false;
            }
            if (type.Name.IndexOf("String") > 0)
            {
                return false;
            }

            return true;
        }
        /// <summary>
        /// 根据表名前缀获取实体类
        /// </summary>
        /// <param name="PrefixName">前缀</param>
        public static Type GetTypeByTableNamePrefix(string PrefixName)
        {
            var theAs = AppDomain.CurrentDomain.GetAssemblies();
            foreach (var a in theAs)
            {
                if (a.FullName.IndexOf("iiERP.Model") >= 0)
                {
                    var theTypes = a.GetTypes();
                    foreach (var t in theTypes)
                    {
                        if (t.Name.IndexOf(PrefixName) >= 0)
                        {
                            return t;
                        }
                    }
                }
            }
            return null;
        }
        /// <summary>
        /// 根据前缀名获取类型,注意重复.
        /// </summary>
        /// <param name="PrefixName">前缀</param>
        public static Type GetTypeByClassPrefixName(string PrefixName)
        {
            var theAs = AppDomain.CurrentDomain.GetAssemblies();
            foreach (var a in theAs)
            {
                var theTypes = a.GetTypes();
                foreach (var t in theTypes)
                {
                    if (t.Name.ToUpper().IndexOf(PrefixName.ToUpper()) >= 0)
                    {
                        return t;
                    }
                }

            }
            return null;
        }
        /// <summary>
        /// iiErp.Model中获取实体类型
        /// </summary>
        /// <param name="PrefixName"></param>
        /// <returns></returns>
        public static Type GetTypeByTableName(this string TableName)
        {
            var theAs = AppDomain.CurrentDomain.GetAssemblies();
            foreach (var a in theAs)
            {
                if (a.FullName.IndexOf("iiERP.Model") >= 0)
                {
                    var theTypes = a.GetTypes();
                    foreach (var theT in theTypes)
                    {
                        if (theT.Name == TableName)
                        {
                            return theT;
                        }
                    }
                }
            }
            return null;
        }
        /// <summary>
        /// 根据类名获取类型
        /// </summary>
        /// <param name="ClassName">类名</param>
        /// <returns></returns>
        public static Type GetTypeByClassName(this string ClassName,bool IgnoreCase=true)
        {
            var theAs = AppDomain.CurrentDomain.GetAssemblies();
            foreach (var a in theAs)
            {
                if (a.FullName.IndexOf("iiERP.Business") >= 0
                    || a.FullName.IndexOf("iiERP.Model") >= 0
                    || a.FullName.IndexOf("iFramework.Util") >= 0
                    || a.FullName.IndexOf("iiERP.Common") >=0 )
                {
                    var theTypes = a.GetTypes();
                    foreach (var theT in theTypes)
                    {
                        if (IgnoreCase == true)
                        {
                            if (theT.Name.ToUpper() == ClassName.ToUpper())
                            {
                                return theT;
                            }
                        }
                        else
                        {
                            if (theT.Name == ClassName)
                            {
                                return theT;
                            }
                        }
                    }
                }
                
            }
            return null;
        }
        /// <summary>
        /// 根据雷明获取类型
        /// </summary>
        /// <param name="ClassName"></param>
        /// <returns></returns>
        public static Type GetType(string ClassName,bool IgnoreCase=true)
        {
            var theType = Type.GetType(ClassName, false, true);
            if (theType == null)
            {
                theType = ClassName.GetTypeByClassName(IgnoreCase);
            }
            return theType;
        }

        public static object CreateInstance(string ClassName)
        {
            var theType = GetType(ClassName);
            if (theType != null)
            {
                return CreateInstance(theType);
            }
            return null;
        }

        public static object CreateInstance(Type Type)
        {
            return Activator.CreateInstance(Type);
        }

        public static string GetPropertyTypeName(this Type AType, string PropertyName)
        {
            var theType = AType.GetTypeByPropertyName(PropertyName);
            if (theType != null)
            {
                return theType.Name;
            }
            return "";
        }
    }
}
