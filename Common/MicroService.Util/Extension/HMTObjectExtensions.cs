using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;

namespace iFramework.Util
{
    public static class  HMTObjectExtensions
    {
        /// <summary>
        /// 如果是控制或者null值则采用默认值覆盖
        /// </summary>
        /// <param name="srcObj">原始值</param>
        /// <param name="overrideObj">默认值要覆盖的值</param>
        /// <returns></returns>
        public static T IfNullOrEmptyOverride<T>(this T srcObj, T overrideObj)
        {
            if (srcObj == null)
            {
                return overrideObj;
            }
            else
            {
                if (srcObj is string)
                {
                    if(string.IsNullOrWhiteSpace(srcObj.ToString()))
                    {
                        return overrideObj;
                    }
                }
            }
            return srcObj;
 
        }

        /// <summary>
        /// 如果是控制或者null值则采用默认值覆盖
        /// </summary>
        /// <param name="srcObj">原始值</param>
        /// <param name="overrideObj">默认值要覆盖的值</param>
        /// <returns></returns>
        public static T IfNullOrEmptyOverride<T>(this T srcObj, Func<T> overrideObj)
        {
            if (srcObj == null)
            {
                if (overrideObj != null)
                {
                    return overrideObj();
                }
            }
            else
            {
                if (srcObj is string)
                {
                    if (string.IsNullOrWhiteSpace(srcObj.ToString()))
                    {
                        if (overrideObj != null)
                        {
                            return overrideObj();
                        }
                    }
                }
            }
            return srcObj;

        }

        /// <summary>
        /// 根据属性名获取属性值，注意只适合非集合类属性.
        /// </summary>
        /// <param name="obj">对象</param>
        /// <param name="PropertyName">属性</param>
        /// <returns>属性值或者为空.</returns>
        public static object GetValueByPropertyName(this object obj, string PropertyName)
        {
            if (obj == null)
            {
                return null;
            }
            Type typ = obj.GetType();
            System.Reflection.PropertyInfo pInfo = typ.GetProperty(PropertyName);
            if (pInfo != null)
            {
                if (pInfo.CanRead == true)
                {
                    return pInfo.GetValue(obj, null);
                }
            }
            return null;
        }
        /// <summary>
        /// 根据属性名设置属性值，注意只适合非集合类属性.
        /// </summary>
        /// <param name="obj">对象</param>
        /// <param name="PropertyName">属性</param>
        /// <param name="value">属性值.</param>
        /// <returns>属性值或者为空.</returns>
        public static void SetValueByPropertyName(this object obj, string PropertyName, object Value)
        {
            if (obj == null)
            {
                return;
            }
            Type typ = obj.GetType();
            System.Reflection.PropertyInfo pInfo = typ.GetProperty(PropertyName);
            if (pInfo != null)
            {
                if (pInfo.CanWrite)
                {
                    try
                    {
                        object theValue = null;
                        if (Value.GetType() != typeof(DBNull))
                        {
                            theValue = Value;
                        }
                        pInfo.SetValue(obj, theValue, null);
                    }
                    catch
                    {
                    }
                }
            }
        }

        /// 对象换字符、可把NULL换为“”
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string ToString(this object obj, string failString = "")
        {
            if (obj == null)
            {
                return failString;
            }
            else
            {
                return obj.ToString();
            }
        }
        /// 对象换字符、可把NULL换为“”
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string ToJsonValue(this object obj, string failString = "")
        {
            if (obj == null)
            {
                return failString;
            }
            else
            {
                return obj.ToString().ToLower();
            }
        }
        /// <summary>
        /// 转换为int类型
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static int ToInt(this object obj, int failInt = 0)
        {
            if (obj != null)
            {
                var dataType = obj.GetType();
                if (dataType == typeof(int) || dataType == typeof(int?))
                {
                    var intValue = Convert.ToInt32(obj);
                    return intValue;
                }
                if (dataType == typeof(long) || dataType == typeof(long?))
                {
                    var intValue = Convert.ToInt32(obj);
                    return intValue;
                }
                if (dataType == typeof(string))
                {
                    var strValue = obj.ToString().Trim().ToLower();
                    if (string.IsNullOrWhiteSpace(strValue))
                    {
                        return failInt;
                    }
                    var intvalue = 0;
                    var status = int.TryParse(strValue, out intvalue);
                    if (status)
                    {
                        return intvalue;
                    }
                }
                return failInt;
            }
            else
            {
                return failInt;
            }

        }
        /// <summary>
        /// 转化为布尔类型
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="failvalue"></param>
        /// <returns></returns>
        public static bool ToBoolean(this object obj, bool failvalue = false, string trueValueStr = "true,on", string falseValueStr = "false,off")
        {
            if (obj != null)
            {
                var dataType = obj.GetType();
                if (dataType == typeof(int) || dataType == typeof(int?))
                {
                    var intValue = Convert.ToInt32(obj);
                    return intValue > 0;
                }
                if (dataType == typeof(long) || dataType == typeof(long?))
                {
                    var intValue = Convert.ToInt64(obj);
                    return intValue > 0;
                }
                if (dataType == typeof(string))
                {
                    var strValue = obj.ToString().Trim().ToLower();
                    if (string.IsNullOrWhiteSpace(strValue))
                    {
                        return false;
                    }
                    var intvalue = 0;
                    var status = int.TryParse(strValue, out intvalue);
                    if (status)
                    {
                        return intvalue > 0;
                    }
                    if (trueValueStr.Contains(strValue))
                    {
                        return true;
                    }
                    if (falseValueStr.Contains(strValue))
                    {
                        return false;
                    }

                }
            }
            return failvalue;
        }

        /// <summary>
        /// 通过一个字典设置一个模型的值
        /// </summary>
        /// <param name="instance">当前的实例</param>
        /// <param name="valueParameter">字典值</param>
        /// <returns>更新的字段列表名</returns>
        public static IEnumerable<string> SetValue(this object instance, Dictionary<string, string> valueParameter, string datetimeFomart = null)
        {
            List<string> fieldNames = new List<string>();
            if (instance != null && valueParameter != null)
            {
                var properties = instance.GetType().GetProperties().ToArray();
                foreach (var item in properties)
                {
                    if (valueParameter.ContainsKey(item.Name))
                    {
                        var propertyType = item.PropertyType;
                        if (item.PropertyType.IsGenericType && item.PropertyType.GetGenericTypeDefinition() == typeof(Nullable<>))
                        {
                            propertyType = item.PropertyType.GetGenericArguments()[0];
                        }
                        if (propertyType.IsPrimitive ||
                            propertyType == typeof(string) ||
                            propertyType == typeof(decimal) ||
                            propertyType == typeof(DateTime))
                        {
                            item.SetValue(instance, ObjectToObject(valueParameter[item.Name], item.PropertyType, datetimeFomart), null);
                            fieldNames.Add(item.Name);
                        }
                    }

                }
            }
            return fieldNames;
        }
        /// <summary>
        /// 根据字段名和值设置一个字段的值
        /// </summary>
        /// <param name="instance"></param>
        /// <param name="fieldName"></param>
        /// <param name="fieldValue"></param>
        public static void SetValue(this object instance, string fieldName, object fieldValue)
        {
            if (!string.IsNullOrWhiteSpace(fieldName))
            {
                var fieldProperty = instance.GetType().GetProperty(fieldName.Trim());
                if (fieldProperty != null && fieldProperty.CanWrite)
                {
                    var value = ObjectToObject(fieldValue, fieldProperty.PropertyType);
                    fieldProperty.SetValue(instance, value, null);
                }
            }
        }
        /// <summary>
        /// 得到一个字段的值
        /// </summary>
        /// <param name="instance"></param>
        /// <param name="fieldName"></param>
        /// <returns></returns>
        public static object GetValue(this object instance, string fieldName)
        {
            if (instance != null && string.IsNullOrWhiteSpace(fieldName) == false)
            {
                var fieldValue = instance.GetType().GetProperty(fieldName).GetValue(instance, null);
                return fieldValue;
            }
            return null;
        }
        /// <summary>
        /// 判断对象是否有指定的属性
        /// </summary>
        /// <param name="instnce"></param>
        /// <param name="fieldName"></param>
        /// <returns></returns>
        public static bool HasProperty(this object instnce, string fieldName)
        {
            try
            {
                var ps = instnce.GetType().GetProperty(fieldName);
                return ps != null;

            }
            catch (Exception)
            {

                return false;
            }

        }
        /// <summary>
        /// 判断对象的属性是否有值
        /// </summary>
        /// <param name="instance"></param>
        /// <param name="fieldName"></param>
        /// <returns></returns>
        public static bool HasValue(this object instance, string fieldName)
        {
            if (instance != null && string.IsNullOrWhiteSpace(fieldName) == false)
            {
                var fieldValue = instance.GetType().GetProperty(fieldName).GetValue(instance, null);
                if (fieldValue != null && fieldValue.ToString().Trim().Length > 0)
                {
                    return true;
                }
            }
            return false;
        }
        /// <summary>
        /// 类型转换
        /// </summary>
        /// <param name="objectValue">源类型</param>
        /// <param name="targetType">目标类型</param>
        /// <param name="formate">格式</param>
        /// <returns></returns>
        public static object ObjectToObject(this object objectValue, Type targetType, string format = null)
        {

            if (targetType == typeof(int?))
            {
                if (objectValue == null || string.IsNullOrWhiteSpace(objectValue.ToString()))
                {
                    return null;
                }
                return Convert.ToInt32(objectValue);
            }

            if (targetType == typeof(DateTime?))
            {
                if (objectValue == null || string.IsNullOrWhiteSpace(objectValue.ToString()))
                {
                    return null;
                }
                else
                {
                    var dateString = objectValue.ToString();
                    string datePatternString = @"/Date\((\d+)\)/";
                    Regex datePattern = new Regex(datePatternString);
                    var match = datePattern.Match(dateString);
                    if (match.Success)
                    {                        
                        DateTime dt = new DateTime(1970, 1, 1,0,0,0,0,DateTimeKind.Unspecified);
                       // dt = HMTDateTime.ToCurrentTimeZone(dt);
                        return dt.AddMilliseconds(long.Parse(match.Groups[1].Value));

                    }
                }
               
                if (format == null)
                {
                    return Convert.ToDateTime(objectValue);
                }

                DateTimeFormatInfo formatInfo = new DateTimeFormatInfo();
                formatInfo.ShortDatePattern = format;
                return Convert.ToDateTime(objectValue, formatInfo);
            }


            if (targetType == typeof(int))
            {
                if (objectValue == null || string.IsNullOrWhiteSpace(objectValue.ToString()))
                {
                    return 0;
                }
                return Convert.ToInt32(objectValue);
            }

            if (targetType == typeof(DateTime))
            {
                if (objectValue == null || string.IsNullOrWhiteSpace(objectValue.ToString()))
                {
                    return DateTime.Now;
                }

                if (format == null)
                {
                    return Convert.ToDateTime(objectValue);
                }

                DateTimeFormatInfo formatInfo = new DateTimeFormatInfo();
                formatInfo.ShortDatePattern = format;
                return Convert.ToDateTime(objectValue, formatInfo);
                 
            }

            if (targetType == typeof(decimal) )
            {
                if (objectValue == null || string.IsNullOrWhiteSpace(objectValue.ToString()))
                {
                    return Convert.ToDecimal(0);
                }

                return Convert.ToDecimal(objectValue);
            }

            if (targetType == typeof(decimal?))
            {
                if (objectValue == null||string.IsNullOrWhiteSpace(objectValue.ToString()))
                {
                    return null;
                }

                return Convert.ToDecimal(objectValue);
            }


            if (targetType == typeof(float))
            {
                if (objectValue == null || string.IsNullOrWhiteSpace(objectValue.ToString()))
                {
                    return Convert.ToDecimal(0);
                }

                return Convert.ToDecimal(objectValue);
            }

            //bool型数据的类型处理
            if (targetType == typeof(bool))
            {
                if (objectValue == null || string.IsNullOrWhiteSpace(objectValue.ToString()))
                {
                    return false;
                }

                return ToBoolean(objectValue);
            }

            if (targetType == typeof(bool?))
            {
                if (objectValue == null || string.IsNullOrWhiteSpace(objectValue.ToString()))
                {
                    return null;
                }

                return ToBoolean(objectValue);
            }

            return objectValue;

        }
        /// <summary>
        /// 获取对象实例的大小，这里只是计算公共属性所占空间.
        /// 计算方法：一个变量占8个字节，具体的变量值按具体类型计算，注意，这里只统计一层，即属性
        /// 如果不是非值类型，则不统计;
        /// </summary>
        /// <param name="obj">对象</param>
        /// <returns></returns>
        public static int DataSize(this object obj)
        {
            if (obj == null)
            {
                return 0;
            }
            var theSize =0;
            var theType = obj.GetType();
            if (theType.IsValueType)
            {
                theSize += System.Runtime.InteropServices.Marshal.SizeOf(obj);
                return theSize;
            }
            if (theType == typeof(string))
            {
                return obj.ToString().LengthB();
            }
            System.Reflection.PropertyInfo[] pInfos = theType.GetProperties();
            if (pInfos != null)
            {
                foreach (var thePI in pInfos)
                {
                    theSize += 8;
                    if (thePI.CanRead == true)
                    {
                        var theVal = thePI.GetValue(obj, null);
                        if (obj != null && (thePI.DeclaringType.IsValueType || thePI.DeclaringType==typeof(string)))
                        {
                            if (thePI.DeclaringType == typeof(string))
                            {
                                theSize += theVal.ToString().LengthB();
                            }
                            else
                            {
                                theSize += System.Runtime.InteropServices.Marshal.SizeOf(theVal);
                            }
                        }
                    }
                }
            }
            return theSize;
        }

        public static void CopyFromObj(this object Obj, object ObjFrom)
        {
            var theObjType = Obj.GetType();
            var thePs = theObjType.GetProperties();
            foreach (var theP in thePs)
            {
                if (theP.CanWrite)
                {
                    var theV = ObjFrom.GetValueByPropertyName(theP.Name);
                    if (theV != null)
                    {
                        Obj.SetValueByPropertyName(theP.Name, theV);
                    }
                }
            }
        }
    }
}
