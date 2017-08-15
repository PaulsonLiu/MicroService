using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Data;
using MicroService.Models;
using iFramework.Util;

namespace MicroService.Common
{
    public static partial class DateTimeHelper
    {
        private static Dictionary<Type, List<PropertyInfo>> _DTProperties { get; set; }
        static DateTimeHelper()
        {
            _DTProperties = new Dictionary<Type, List<PropertyInfo>>();
        }
        private static List<PropertyInfo> GetDateTimeProperties(Type type)
        {
            if (_DTProperties.ContainsKey(type))
            {
                return _DTProperties[type];
            }
            lock (type)
            {
                if (_DTProperties.ContainsKey(type))
                {
                    return _DTProperties[type];
                }
                var theProperties = type.GetProperties();
                var theDTProperties = theProperties.Where(m => m.MemberType == System.Reflection.MemberTypes.Property
                    && (m.PropertyType == typeof(DateTime) || m.PropertyType == typeof(DateTime?))).ToList();
                if (theDTProperties != null)
                {
                    _DTProperties.Add(type, theDTProperties);
                }
                return theDTProperties;
            }
        }
        public static void OToUTCDateTimeE(this System.Collections.IEnumerable Models, bool ViewAsUtc)
        {
            if (Models != null)
            {
                foreach (var item in Models)
                {
                    OToUTCDateTime(item, ViewAsUtc);
                }
            }
        }
        public static void OToViewDateTimeE(this System.Collections.IEnumerable Models)
        {
            if (Models != null)
            {
                foreach (var item in Models)
                {
                    OToViewDateTime(item);
                }
            }

        }
        public static void OToUTCDateTime(this object Model, bool ViewAsUtc)
        {
            if (Model == null)
            {
                return;
            }
            if (Model is System.Collections.IEnumerable)
            {
                ((System.Collections.IEnumerable)Model).OToUTCDateTimeE(ViewAsUtc);
            }
            if (Model is DataTable)
            {
                ((DataTable)Model).OToUTCDateTimeDT(ViewAsUtc);
            }
            var theSts = Model.GetValueByPropertyName("TimeSts");
            if (theSts != null)
            {
                var theTimeSts = (TimeZoneSts)theSts;
                if (theTimeSts == TimeZoneSts.UTC || theTimeSts == TimeZoneSts.IGNORE)
                {
                    return;
                }
            }
            var theType = Model.GetType();
            var theProperties = GetDateTimeProperties(theType);
            if (theProperties != null)
            {
                foreach (var theP in theProperties)
                {
                    var theValue = theP.GetValue(Model, null);
                    if (theValue != null)
                    {
                        if (theValue is DateTime)
                        {
                            var theNewValue = ((DateTime)theValue).ToUtcDateTime(ViewAsUtc);
                            theP.SetValue(Model, theNewValue, null);
                        }
                        else if (theValue is DateTime?)
                        {
                            var theNewValue = ((DateTime?)theValue).ToUtcDateTime(ViewAsUtc);
                            theP.SetValue(Model, theNewValue, null);
                        }
                    }
                }
            }
            if (theSts != null)
            {
                Model.SetValueByPropertyName("TimeSts", TimeZoneSts.UTC);
            }
        }
        public static void OToViewDateTime(this object Model)
        {
            if (Model == null)
            {
                return;
            }
            if (Model is System.Collections.IEnumerable)
            {
                ((System.Collections.IEnumerable)Model).OToViewDateTimeE();
            }
            if (Model is DataTable)
            {
                ((DataTable)Model).OToViewDateTimeDT();
            }

            var theSts = Model.GetValueByPropertyName("TimeSts");
            if (theSts != null)
            {
                var theTimeSts = (TimeZoneSts)theSts;
                if (theTimeSts == TimeZoneSts.VIEW || theTimeSts == TimeZoneSts.IGNORE)
                {
                    return;
                }
            }
            var theType = Model.GetType();
            var theProperties = GetDateTimeProperties(theType);
            if (theProperties != null)
            {
                foreach (var theP in theProperties)
                {
                    var theValue = theP.GetValue(Model, null);
                    if (theValue != null)
                    {
                        if (theValue is DateTime)
                        {
                            var theNewValue = ((DateTime)theValue).ToCurrentTimeZone();
                            if (theP.CanWrite)
                            {
                                theP.SetValue(Model, theNewValue, null);
                            }
                        }
                        else if (theValue is DateTime?)
                        {
                            var theNewValue = ((DateTime?)theValue).ToCurrentTimeZone();
                            if (theP.CanWrite)
                            {
                                theP.SetValue(Model, theNewValue, null);
                            }
                        }
                    }
                }
            }
            if (theSts != null)
            {
                Model.SetValueByPropertyName("TimeSts", TimeZoneSts.VIEW);
            }
        }
        //public static void OToUTCDateTimeDT(this DataTable DtData, bool ViewAsUtc)
        //{
        //    if (DtData == null)
        //    {
        //        return;
        //    }
        //    foreach (DataRow theRow in DtData.Rows)
        //    {
        //        for (int i = 0; i < DtData.Columns.Count; i++)
        //        {
        //            var theObj = theRow[i];
        //            if (theObj is DateTime)
        //            {
        //                theRow[i] = ((DateTime)theRow[i]).ToUtcDateTime(ViewAsUtc);

        //            }
        //            else if (theObj is DateTime?)
        //            {
        //                theRow[i] = ((DateTime?)theRow[i]).ToUtcDateTime(ViewAsUtc);
        //            }
        //        }
        //    }
        //}
        //public static void OToViewDateTimeDT(this DataTable DtData)
        //{
        //    if (DtData == null)
        //    {
        //        return;
        //    }
        //    foreach (DataRow theRow in DtData.Rows)
        //    {
        //        for (int i = 0; i < DtData.Columns.Count; i++)
        //        {
        //            var theObj = theRow[i];
        //            if (theObj is DateTime)
        //            {
        //                theRow[i] = ((DateTime)theRow[i]).ToCurrentTimeZone();
        //            }
        //            else if (theObj is DateTime?)
        //            {
        //                theRow[i] = ((DateTime?)theRow[i]).ToCurrentTimeZone();
        //            }
        //        }
        //    }
        //}

        /// <summary>
        /// 格式成周+yyyy-MM-dd HH:mm
        /// </summary>
        /// <param name="Value"></param>
        /// <returns></returns>
        //public static string ToStrWeekAndLongDateTime(this DateTime Value)
        //{
        //    string theRet = "";
        //    int theWeek = (int)Value.DayOfWeek;
        //    string theWeekText = SysMasterFileMgmt.GetReferenceTextByValue("SS5065_WEEK", theWeek.ToString());
        //    string theDateTimeText = Value.ToString("yyyy-MM-dd HH:mm");
        //    theRet = theWeekText + "" + theDateTimeText;
        //    return theRet;
        //}
        /// <summary>
        /// 格式成周+yyyy-MM-dd HH:mm
        /// </summary>
        /// <param name="Value"></param>
        /// <returns></returns>
        public static string ToStrWeekAndLongDateTime(this DateTime? Value)
        {
            string theRet = "";
            if (Value != null)
            {
                theRet = Value.Value.ToStrWeekAndLongDateTime();
            }
            return theRet;
        }
        /// <summary>
        /// 格式成yyyy-MM-dd HH:mm
        /// </summary>
        /// <param name="Value"></param>
        /// <returns></returns>
        public static string ToStrLongDateTime(this DateTime Value)
        {
            return Value.ToString("yyyy-MM-dd HH:mm");
        }
        /// <summary>
        /// 格式成yyyy-MM-dd HH:mm
        /// </summary>
        /// <param name="Value"></param>
        /// <returns></returns>
        public static string ToStrLongDateTime(this DateTime? Value)
        {
            string theRet = "";
            if (Value != null)
            {
                theRet = Value.Value.ToStrLongDateTime();
            }
            return theRet;
        }
        /// <summary>
        /// 将两个日期格式化成日期时间格式，如果日期相同，则后面一个日期的的日期不显示yyyy-MM-dd HH:mm
        /// </summary>
        /// <param name="From"></param>
        /// <param name="To"></param>
        /// <returns></returns>
        public static string Format2DTFromTo(DateTime From, DateTime To)
        {
            var theRet = "";
            if (From.Date == To.Date)
            {
                theRet = From.ToString("yyyy-MM-dd HH:mm") + "-" + To.ToString("HH:mm");
            }
            else
            {
                theRet = From.ToString("yyyy-MM-dd HH:mm") + "-" + To.ToString("yyyy-MM-dd HH:mm");
            }
            return theRet;
        }
        /// <summary>
        /// 将两个日期格式化成日期时间格式，如果日期相同，则后面一个日期的的日期不显示yyyy-MM-dd HH:mm
        /// </summary>
        /// <param name="From"></param>
        /// <param name="To"></param>
        /// <returns></returns>
        public static string Format2DTFromTo(DateTime? From, DateTime? To)
        {
            var theRet = "";
            var theJoin = "-";
            if (From == null || To == null)
            {
                theJoin = "";
                theRet = From.ToStrLongDateTime() + To.ToStrLongDateTime();
            }
            else
            {
                if (From.Value.Date == To.Value.Date)
                {
                    theRet = From.Value.ToString("yyyy-MM-dd HH:mm") + theJoin + To.Value.ToString("HH:mm");
                }
                else
                {
                    theRet = From.Value.ToString("yyyy-MM-dd HH:mm") + theJoin + To.Value.ToString("yyyy-MM-dd HH:mm");
                }
            }
            return theRet;
        }
        /// <summary>
        /// 格式化From-To时间格式,
        /// </summary>
        /// <param name="From"></param>
        /// <param name="To"></param>
        /// <returns></returns>
        public static string Format2DTFromToWeekAndLongTime(DateTime? From, DateTime? To)
        {
            var theJoin = "-";
            if (From == null || To == null)
            {
                return From.ToStrWeekAndLongDateTime() + To.ToStrWeekAndLongDateTime();
            }
            if (From.Value.Date == To.Value.Date)
            {
                return From.ToStrWeekAndLongDateTime() + theJoin + To.Value.ToString("HH:mm");
            }
            else
            {
                return From.ToStrWeekAndLongDateTime() + theJoin + To.ToStrWeekAndLongDateTime();
            }
        }
        /// <summary>
        /// 联合日期和时间，如果为空则取当前时间
        /// </summary>
        /// <param name="Date">日期</param>
        /// <param name="Time">时间</param>
        /// <returns></returns>
        public static DateTime CombineDateAndTime(DateTime? Date, DateTime? Time)
        {
            var theDate = Date ?? HMTDateTime.Now;
            var theTime = Time ?? HMTDateTime.Now;
            return new DateTime(theDate.Year, theDate.Month, theDate.Day, theTime.Hour, theTime.Minute, theTime.Second);
        }
        /// <summary>
        /// 联合日期和时间，如果为空则取当前时间
        /// </summary>
        /// <param name="Date">日期</param>
        /// <param name="Time">时间</param>
        /// <returns></returns>
        public static DateTime CombineDateAndTime(DateTime Date, DateTime Time)
        {
            return new DateTime(Date.Year, Date.Month, Date.Day, Time.Hour, Time.Minute, Time.Second);
        }

        /// <summary>
        /// 获取工作日
        /// </summary>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        public static long GetWeekday(DateTime startDate, DateTime endDate)
        {
            TimeSpan ts = endDate.Subtract(startDate);//TimeSpan得到startDate和endDate的时间间隔  
            long countday = ts.Days;//获取两个日期间的总天数  
            long weekday = 0;//工作日  
                             //循环用来扣除总天数中的双休日  
            for (int i = 0; i < countday; i++)
            {
                DateTime tempdt = startDate.Date.AddDays(i + 1);
                if (tempdt.DayOfWeek != System.DayOfWeek.Saturday && tempdt.DayOfWeek != System.DayOfWeek.Sunday)
                {
                    weekday++;
                }
            }

            return weekday;
        }
    }
}
