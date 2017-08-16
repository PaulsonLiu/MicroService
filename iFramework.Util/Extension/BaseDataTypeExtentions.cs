using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace iFramework.Util
{
    /// <summary>
    /// 基本类型扩展功能集合
    /// </summary>
    public static class BaseDataTypeExtentions
    {
        public static bool IsNumber(this string obj)
        {
            Int64 theNum = 0;
            var theRet = Int64.TryParse(obj, out theNum);
            return theRet;
        }
        //private static object ConvertToT<T>(object Source)
        //{
        //    object theRet = Source;
        //    if (Source is string)
        //    {
        //        return (Source as string).ConvertToTargetType(typeof(T), Source);
        //    }
        //    return theRet;
        //}
        /// <summary>
        /// 将值转换成指定类型的值.
        /// </summary>
        /// <param name="Source">源</param>
        /// <param name="type">指定类型</param>
        /// <param name="DefaultValue">没转换成功返回的值</param>
        /// <returns>指定类型的值,转换不成功返回default值.</returns>
        public static object ConvertToTargetType(this string Source, Type type, object DefaultValue)
        {
            if (type == typeof(string))
            {
                return Source;
            }
            if (type == typeof(Boolean))
            {
                Boolean theRet = Boolean.Parse(Source);
                return theRet;
            }
            if (type == typeof(byte))
            {
                Byte theRet = Byte.Parse(Source);
                return theRet;
            }
            if (type == typeof(Char))
            {
                Char theRet = Char.Parse(Source);
                return theRet;
            }
            if (type == typeof(DateTime))
            {
                DateTime theRet = DateTime.Parse(Source);
                return theRet;
            }
            if (type == typeof(Decimal))
            {
                Decimal theRet = Decimal.Parse(Source);
                return theRet;
            }
            if (type == typeof(Double))
            {
                Double theRet = Double.Parse(Source);
                return theRet;
            }
            if (type == typeof(Int16))
            {
                Int16 theRet = Int16.Parse(Source);
                return theRet;
            }

            if (type == typeof(Int32))
            {
                Int32 theRet = Int32.Parse(Source);
                return theRet;
            }
            if (type == typeof(SByte))
            {
                SByte theRet = SByte.Parse(Source);
                return theRet;
            }
            if (type == typeof(int))
            {
                int theRet = int.Parse(Source);
                return theRet;
            }
            if (type == typeof(Single))
            {
                Single theRet = Single.Parse(Source);
                return theRet;
            }

            if (type == typeof(String))
            {
                String theRet = Source;
                return theRet;
            }
            if (type == typeof(UInt16))
            {
                UInt16 theRet = UInt16.Parse(Source);
                return theRet;
            }

            if (type == typeof(UInt32))
            {
                UInt32 theRet = UInt32.Parse(Source);
                return theRet;
            }
            if (type == typeof(UInt64))
            {
                UInt64 theRet = UInt64.Parse(Source);
                return theRet;
            }
            //可空基本类型处理.
            if (type == typeof(Boolean?))
            {
                Boolean? theRet = (Source != null && Source != "" && Source != string.Empty) ? (Boolean?)bool.Parse(Source) : null;
                return theRet;
            }
            if (type == typeof(byte?))
            {
                Byte? theRet = (Source != null && Source != "" && Source != string.Empty) ? (Byte?)Byte.Parse(Source) : null;
                return theRet;
            }
            if (type == typeof(Char?))
            {
                Char? theRet = (Source != null && Source != "" && Source != string.Empty) ? (Char?)Char.Parse(Source) : null;
                return theRet;
            }
            if (type == typeof(DateTime?))
            {
                DateTime? theRet = (Source != null && Source != "" && Source != string.Empty) ? (DateTime?)DateTime.Parse(Source) : null;
                return theRet;
            }
            if (type == typeof(Decimal?))
            {
                Decimal? theRet = (Source != null && Source != "" && Source != string.Empty) ? (Decimal?)Decimal.Parse(Source) : null;
                return theRet;
            }
            if (type == typeof(Double?))
            {
                Double? theRet = (Source != null && Source != "" && Source != string.Empty) ? (Double?)Double.Parse(Source) : null;
                return theRet;
            }
            if (type == typeof(Int16?))
            {
                Int16? theRet = (Source != null && Source != "" && Source != string.Empty) ? (Int16?)Int16.Parse(Source) : null;
                return theRet;
            }

            if (type == typeof(Int32?))
            {
                Int32? theRet = (Source != null && Source != "" && Source != string.Empty) ? (Int32?)Int32.Parse(Source) : null;
                return theRet;
            }
            if (type == typeof(Int64?))
            {
                Int64? theRet = (Source != null && Source != "" && Source != string.Empty) ? (Int64?)Int64.Parse(Source) : null;
                return theRet;
            }
            if (type == typeof(SByte?))
            {
                SByte? theRet = (Source != null && Source != "" && Source != string.Empty) ? (SByte?)SByte.Parse(Source) : null;
                return theRet;
            }
            if (type == typeof(int?))
            {
                int? theRet = (Source != null && Source != "" && Source != string.Empty) ? (int?)int.Parse(Source) : null;
                return theRet;
            }
            if (type == typeof(Single?))
            {
                Single? theRet = (Source != null && Source != "" && Source != string.Empty) ? (Single?)Single.Parse(Source) : null;
                return theRet;
            }


            if (type == typeof(UInt16?))
            {
                UInt16? theRet = (Source != null && Source != "" && Source != string.Empty) ? (UInt16?)UInt16.Parse(Source) : null;
                return theRet;
            }

            if (type == typeof(UInt32?))
            {
                UInt32? theRet = (Source != null && Source != "" && Source != string.Empty) ? (UInt32?)UInt32.Parse(Source) : null;
                return theRet;
            }
            if (type == typeof(UInt64?))
            {
                UInt64? theRet = (Source != null && Source != "" && Source != string.Empty) ? (UInt64?)UInt64.Parse(Source) : null;
                return theRet;
            }
            return DefaultValue;
        }
        
        ///// <summary>
        ///// 将当前对象赋值到目标对象，如果转换不成功，则返回缺省值。
        ///// </summary>
        ///// <typeparam name="T">类型参数</typeparam>
        ///// <param name="obj">对象实例</param>
        ///// <param name="Target">目标实例</param>
        ///// <param name="DefaultValue">转换不成功的情况下的缺省值</param>
        //public static void EvlToNumeric<T>(this object obj,ref T Target, T DefaultValue)
        //{

        //    try
        //    {
        //        T theRet = (T)ConvertToT<T>(obj);
        //        Target = theRet;
        //    }
        //    catch
        //    {
        //        try
        //        {
        //            T theRet = (T)ConvertToT<T>(obj.ToString());
        //        }
        //        catch
        //        {
        //            Target = DefaultValue;
        //        }

        //    }
        //}
        ///// <summary>
        ///// 将当前对象赋值到目标对象，如果转换不成功，则什么都不做。
        ///// </summary>
        ///// <typeparam name="T">类型参数</typeparam>
        ///// <param name="obj">对象实例</param>
        ///// <param name="Target">目标实例</param>
        //public static void EvlToNumeric<T>(this object obj, ref T Target)
        //{
        //    try
        //    {
        //        T theRet = (T)ConvertToT<T>(obj);
        //        Target = theRet;
        //    }
        //    catch
        //    {
        //        try
        //        {
        //            T theRet = (T)ConvertToT<T>(obj.ToString());
        //        }
        //        catch
        //        {
        //        }
        //    }
        //}

        ///// <summary>
        ///// 将当前对象赋值到目标对象，如果转换不成功，则返回缺省值。
        ///// </summary>
        ///// <typeparam name="T">类型参数</typeparam>
        ///// <param name="obj">对象实例</param>
        ///// <param name="DefaultValue">转换不成功的情况下的缺省值</param>
        ///// <returns>返回值</returns>
        //public static T AsNumeric<T>(this object obj, T DefaultValue)
        //{
        //    try
        //    {
        //        T theRet = (T)obj;
        //        return theRet;
        //    }
        //    catch
        //    {
        //        try
        //        {
        //            T theRet = (T)ConvertToT<T>(obj.ToString());
        //            return theRet;
        //        }
        //        catch
        //        {
        //            return DefaultValue;
        //        }
               
        //    }
        //}
        ///// <summary>
        ///// 将当前对象赋值到目标对象，如果转换不成功，则返回缺省值。
        ///// </summary>
        ///// <typeparam name="T">类型参数</typeparam>
        ///// <param name="obj">对象实例</param>
        ///// <returns>返回值</returns>
        //public static T AsNumeric<T>(this object obj)
        //{
        //    try
        //    {
        //        T theRet = (T)obj;
        //        return theRet;
        //    }
        //    catch
        //    {
        //        try
        //        {
        //            T theRet = (T)ConvertToT<T>(obj.ToString());
        //            return theRet;
        //        }
        //        catch
        //        {
        //            return default(T);
        //        }

        //    }
        //}
        
        /// <summary>
        /// 对象转换成int32
        /// </summary>
        /// <param name="obj">对象实例</param>
        /// <returns></returns>
        public static Int32 AsInt32(this object obj)
        {
            return Convert.ToInt32(obj);
        }
        /// <summary>
        /// 对象转换成int32,未成功返回缺省值
        /// </summary>
        /// <param name="obj">对象实例</param>
        /// <returns></returns>
        public static Int32 AsInt32(this object obj,Int32 DefaultValue)
        {
            try
            {
                return Convert.ToInt32(obj);
            }
            catch
            {
                return DefaultValue;
            }
        }

        /// <summary>
        /// 对象转换成int32?
        /// </summary>
        /// <param name="obj">对象实例</param>
        /// <returns></returns>
        public static Int32? AsNullableInt32(this object obj)
        {
            try
            {
                if (IsNullObj(obj) == true)
                {
                    return null;
                }
                return (Int32?)(Convert.ToInt32(obj));
            }
            catch
            {
                return null;
            }
        }
        /// <summary>
        /// 对象转换成int32?,未成功返回缺省值
        /// </summary>
        /// <param name="obj">对象实例</param>
        /// <returns></returns>
        public static Int32? AsNullableInt32(this object obj, Int32? DefaultValue)
        {
            try
            {
                if (IsNullObj(obj) == true)
                {
                    return null;
                }
                return (Int32?)(Convert.ToInt32(obj));
            }
            catch
            {
                return DefaultValue;
            }
        }


        /// <summary>
        /// 对象转换成Boolean
        /// </summary>
        /// <param name="obj">对象实例</param>
        /// <returns></returns>
        public static Boolean AsBoolean(this object obj)
        {
            return Convert.ToBoolean(obj);
        }
        /// <summary>
        /// 对象转换成Boolean,未成功返回缺省值
        /// </summary>
        /// <param name="obj">对象实例</param>
        /// <returns></returns>
        public static Boolean AsBoolean(this object obj, Boolean DefaultValue)
        {
            try
            {
                return Convert.ToBoolean(obj);
            }
            catch
            {
                return DefaultValue;
            }
        }
        /// <summary>
        /// 根据系统定义的字符串转换成布尔类型扩展函数
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static Boolean AsBooleanEx(this string obj)
        {
            switch (obj.ToUpper())
            {
                case "0":
                    return false;
                case "1":
                    return true;
                case "Y":
                    return true;
                case "N":
                    return false;
                case "BD0201":
                    return true;
                case "BD0202":
                    return false;
                default:
                    return Boolean.Parse(obj.ToString());
            }
        }
        /// <summary>
        /// 根据系统定义的字符串转换成布尔类型扩展函数
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="DefaultValue">未转换成功后的缺省值.</param>
        /// <returns></returns>
        public static Boolean AsBooleanEx(this string obj,Boolean DefaultValue)
        {
            try
            {
                switch (obj.ToUpper())
                {
                    case "0":
                        return false;
                    case "1":
                        return true;
                    case "Y":
                        return true;
                    case "N":
                        return false;
                    case "YES":
                        return true;
                    case "NO":
                        return false;
                    case "TRUE":
                        return true;
                    case "FALSE":
                        return false;
                    case "BD0201":
                        return true;
                    case "BD0202":
                        return false;
                    default:
                        return Boolean.Parse(obj.ToString());
                }
            }
            catch
            {
                return DefaultValue;
            }
        }
        /// <summary>
        /// 对象转换成Boolean?
        /// </summary>
        /// <param name="obj">对象实例</param>
        /// <returns></returns>
        public static Boolean? AsNullableBoolean(this object obj)
        {
            try
            {
                if (IsNullObj(obj) == true)
                {
                    return null;
                }
                return (Boolean?)Convert.ToBoolean(obj);
            }
            catch
            {
                return null;
            }
        }
        /// <summary>
        /// 对象转换成Boolean?,未成功返回缺省值
        /// </summary>
        /// <param name="obj">对象实例</param>
        /// <returns></returns>
        public static Boolean? AsNullableBoolean(this object obj, Boolean? DefaultValue)
        {
            try
            {
                if (IsNullObj(obj) == true)
                {
                    return null;
                }
                return (Boolean?)Convert.ToBoolean(obj);
            }
            catch
            {
                return DefaultValue;
            }
        }

        /// <summary>
        /// 对象转换成byte
        /// </summary>
        /// <param name="obj">对象实例</param>
        /// <returns></returns>
        public static byte AsByte(this object obj)
        {
            return Convert.ToByte(obj);
        }
        /// <summary>
        /// 对象转换成byte,未成功返回缺省值
        /// </summary>
        /// <param name="obj">对象实例</param>
        /// <returns></returns>
        public static byte AsByte(this object obj, byte DefaultValue)
        {
            try
            {
                return Convert.ToByte(obj);
            }
            catch
            {
                return DefaultValue;
            }
        }

        /// <summary>
        /// 对象转换成DateTime
        /// </summary>
        /// <param name="obj">对象实例</param>
        /// <returns></returns>
        public static DateTime AsDateTime(this object obj)
        {
            return Convert.ToDateTime(obj);
        }
        /// <summary>
        /// 对象转换成DateTime,未成功返回缺省值
        /// </summary>
        /// <param name="obj">对象实例</param>
        /// <returns></returns>
        public static DateTime AsDateTime(this object obj, DateTime DefaultValue)
        {
            try
            {
                return Convert.ToDateTime(obj);
            }
            catch
            {
                return DefaultValue;
            }
        }

        /// <summary>
        /// 对象转换成DateTime?
        /// </summary>
        /// <param name="obj">对象实例</param>
        /// <returns></returns>
        public static DateTime? AsNullableDateTime(this object obj)
        {
            try
            {
                if (IsNullObj(obj) == true)
                {
                    return null;
                }
                return (DateTime?)(Convert.ToDateTime(obj));
            }
            catch
            {
                return null;
            }
        }
        /// <summary>
        /// 对象转换成DateTime?,未成功返回缺省值
        /// </summary>
        /// <param name="obj">对象实例</param>
        /// <returns></returns>
        public static DateTime? AsNullableDateTime(this object obj, DateTime? DefaultValue)
        {
            try
            {
                if (IsNullObj(obj) == true)
                {
                    return null;
                }
                return (DateTime?)(Convert.ToDateTime(obj));
            }
            catch
            {
                return DefaultValue;
            }
        }

        /// <summary>
        /// 对象转换成Decimal
        /// </summary>
        /// <param name="obj">对象实例</param>
        /// <returns></returns>
        public static Decimal AsDecimal(this object obj)
        {
            return Convert.ToDecimal(obj);
        }
        /// <summary>
        /// 对象转换成DateTime,未成功返回缺省值
        /// </summary>
        /// <param name="obj">对象实例</param>
        /// <returns></returns>
        public static Decimal AsDecimal(this object obj, Decimal DefaultValue)
        {
            try
            {
                return Convert.ToDecimal(obj);
            }
            catch
            {
                return DefaultValue;
            }
        }

        /// <summary>
        /// 对象转换成Decimal?
        /// </summary>
        /// <param name="obj">对象实例</param>
        /// <returns></returns>
        public static Decimal? AsNullableDecimal(this object obj)
        {
            try
            {
                if (IsNullObj(obj) == true)
                {
                    return null;
                }
                return (Decimal?)(Convert.ToDecimal(obj));
            }
            catch
            {
                return null;
            }
        }
        /// <summary>
        /// 对象转换成DateTime,未成功返回缺省值
        /// </summary>
        /// <param name="obj">对象实例</param>
        /// <returns></returns>
        public static Decimal? AsNullableDecimal(this object obj, Decimal? DefaultValue)
        {
            try
            {
                if (IsNullObj(obj) == true)
                {
                    return null;
                }
                return (Decimal?)(Convert.ToDecimal(obj));
            }
            catch
            {
                return DefaultValue;
            }
        }

        /// <summary>
        /// 对象转换成Double
        /// </summary>
        /// <param name="obj">对象实例</param>
        /// <returns></returns>
        public static Double AsDouble(this object obj)
        {
            return Convert.ToDouble(obj);
        }
        /// <summary>
        /// 对象转换成Double,未成功返回缺省值
        /// </summary>
        /// <param name="obj">对象实例</param>
        /// <returns></returns>
        public static Double AsDouble(this object obj, Double DefaultValue)
        {
            try
            {
                return Convert.ToDouble(obj);
            }
            catch
            {
                return DefaultValue;
            }
        }

        /// <summary>
        /// 对象转换成Char
        /// </summary>
        /// <param name="obj">对象实例</param>
        /// <returns></returns>
        public static Char AsChar(this object obj)
        {
            return Convert.ToChar(obj);
        }
        /// <summary>
        /// 对象转换成Char,未成功返回缺省值
        /// </summary>
        /// <param name="obj">对象实例</param>
        /// <returns></returns>
        public static Char AsChar(this object obj, Char DefaultValue)
        {
            try
            {
                return Convert.ToChar(obj);
            }
            catch
            {
                return DefaultValue;
            }
        }

        /// <summary>
        /// 对象转换成Int16
        /// </summary>
        /// <param name="obj">对象实例</param>
        /// <returns></returns>
        public static Int16 AsInt16(this object obj)
        {
            return Convert.ToInt16(obj);
        }
        /// <summary>
        /// 对象转换成Int16,未成功返回缺省值
        /// </summary>
        /// <param name="obj">对象实例</param>
        /// <returns></returns>
        public static Int16 AsInt16(this object obj, Int16 DefaultValue)
        {
            try
            {
                return Convert.ToInt16(obj);
            }
            catch
            {
                return DefaultValue;
            }
        }

        /// <summary>
        /// 对象转换成SByte
        /// </summary>
        /// <param name="obj">对象实例</param>
        /// <returns></returns>
        public static SByte AsSByte(this object obj)
        {
            return Convert.ToSByte(obj);
        }
        /// <summary>
        /// 对象转换成SByte,未成功返回缺省值
        /// </summary>
        /// <param name="obj">对象实例</param>
        /// <returns></returns>
        public static SByte AsSByte(this object obj, SByte DefaultValue)
        {
            try
            {
                return Convert.ToSByte(obj);
            }
            catch
            {
                return DefaultValue;
            }
        }

        /// <summary>
        /// 对象转换成Single
        /// </summary>
        /// <param name="obj">对象实例</param>
        /// <returns></returns>
        public static Single AsSingle(this object obj)
        {
            return Convert.ToSingle(obj);
        }
        /// <summary>
        /// 对象转换成Single,未成功返回缺省值
        /// </summary>
        /// <param name="obj">对象实例</param>
        /// <returns></returns>
        public static Single AsSingle(this object obj, Single DefaultValue)
        {
            try
            {
                return Convert.ToSingle(obj);
            }
            catch
            {
                return DefaultValue;
            }
        }

        /// <summary>
        /// 对象转换成UInt16
        /// </summary>
        /// <param name="obj">对象实例</param>
        /// <returns></returns>
        public static UInt16 AsUInt16(this object obj)
        {
            return Convert.ToUInt16(obj);
        }
        /// <summary>
        /// 对象转换成UInt16,未成功返回缺省值
        /// </summary>
        /// <param name="obj">对象实例</param>
        /// <returns></returns>
        public static UInt16 AsUInt16(this object obj, UInt16 DefaultValue)
        {
            try
            {
                return Convert.ToUInt16(obj);
            }
            catch
            {
                return DefaultValue;
            }
        }

        /// <summary>
        /// 对象转换成UInt32
        /// </summary>
        /// <param name="obj">对象实例</param>
        /// <returns></returns>
        public static UInt32 AsUInt32(this object obj)
        {
            return Convert.ToUInt32(obj);
        }
        /// <summary>
        /// 对象转换成UInt32,未成功返回缺省值
        /// </summary>
        /// <param name="obj">对象实例</param>
        /// <returns></returns>
        public static UInt32 AsUInt32(this object obj, UInt32 DefaultValue)
        {
            try
            {
               return Convert.ToUInt32(obj);
            }
            catch
            {
                return DefaultValue;
            }
        }


        /// <summary>
        /// 对象转换成UInt64
        /// </summary>
        /// <param name="obj">对象实例</param>
        /// <returns></returns>
        public static UInt64 AsUInt64(this object obj)
        {
            return Convert.ToUInt64(obj);
        }
        /// <summary>
        /// 对象转换成UInt64,未成功返回缺省值
        /// </summary>
        /// <param name="obj">对象实例</param>
        /// <returns></returns>
        public static UInt64 AsUInt64(this object obj, UInt64 DefaultValue)
        {
            try
            {
                return Convert.ToUInt64(obj);
            }
            catch
            {
                return DefaultValue;
            }
        }


        /// <summary>
        /// 对象转换成Int64
        /// </summary>
        /// <param name="obj">对象实例</param>
        /// <returns></returns>
        public static Int64 AsInt64(this object obj)
        {
            return Convert.ToInt64(obj);
        }
        /// <summary>
        /// 对象转换成Int64,未成功返回缺省值
        /// </summary>
        /// <param name="obj">对象实例</param>
        /// <returns></returns>
        public static Int64 AsInt64(this object obj, Int64 DefaultValue)
        {
            try
            {
                return Convert.ToInt64(obj);
            }
            catch
            {
                return DefaultValue;
            }
        }

        /// <summary>
        /// 对象转换成Int64?
        /// </summary>
        /// <param name="obj">对象实例</param>
        /// <returns></returns>
        public static Int64? AsNullableInt64(this object obj)
        {
            try
            {
                if (IsNullObj(obj) == true)
                {
                    return null;
                }
                return (Int64?)(Convert.ToInt64(obj));
            }
            catch
            {
                return null;
            }
        }
        /// <summary>
        /// 对象转换成Int64?,未成功返回缺省值
        /// </summary>
        /// <param name="obj">对象实例</param>
        /// <returns></returns>
        public static Int64? AsNullableInt64(this object obj, Int64? DefaultValue)
        {
            try
            {
                if (IsNullObj(obj) == true)
                {
                    return null;
                }
                return (Int64?)(Convert.ToInt64(obj));
            }
            catch
            {
                return DefaultValue;
            }
        }
        private static bool IsNullObj(object obj)
        {
            if (obj == null)
            {
                return true;
            }
            if (obj.ToString() == "" || obj.ToString() == string.Empty)
            {
                return true;
            }
            return false;
        }
        /// <summary>
        /// 布尔值的数据库表示字符串
        /// </summary>
        /// <param name="obj">实例</param>
        /// <param name="TargetType">目标类型</param>
        /// <returns>布尔数据库表达字符串</returns>
        public static string ToStringValue(this Boolean obj, BooleanType TargetType)
        {
            switch (TargetType)
            {
                case BooleanType.B01:
                    return ((obj == true) ? "1" : "0");
                case BooleanType.BBaseDict:
                    return ((obj == true) ? "BD0201" : "BD0202");
                case BooleanType.BOther:
                    return ((obj == true) ? "BD0201" : "BD0202");
                case BooleanType.BTrueFalse:
                    return ((obj == true) ? "TRUE" : "FALSE");
                case BooleanType.BYesNo:
                    return ((obj == true) ? "YES" : "NO");
                case BooleanType.BYN:
                    return ((obj == true) ? "Y" : "N");
                default:
                    return ((obj == true) ? "BD0201" : "BD0202");
            }
        }

    }
}
