using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace iFramework.Util
{
    public class HMTimeZoneInfo
    {
        public string ID { get; set; }
        public string HMCode { get; set; }
        public decimal TimeToUtc { get; set; }
        public string Name { get; set; }
        public string STDName { get; set; }
    }
    /// <summary>
    /// 华旻时间管理类
    /// </summary>
    public static class HMTDateTime
    {
        public static Func<TimeZoneInfo> GetCurrentTimeZone { get; set; }
        public static DateTime Now
        {
            get
            {
                return DateTime.UtcNow.ToCurrentTimeZone();
            }
        }
        
        private static TimeZoneInfo GetCurrentTimeZoneEx()
        {
            TimeZoneInfo theZoneInfo = null;
            if (GetCurrentTimeZone != null)
            {
                theZoneInfo = GetCurrentTimeZone();
            }
            if (theZoneInfo == null)
            {
                theZoneInfo = TimeZoneInfo.Local;
            }
            return theZoneInfo;
        }
        public static DateTime ToCurrentTimeZone(this DateTime Now)
        {
            if (Now.Kind == DateTimeKind.Utc || Now.Kind== DateTimeKind.Unspecified)
            {
                var theZoneInfo = GetCurrentTimeZoneEx();
                return TimeZoneInfo.ConvertTime(Now, theZoneInfo);
            }
            return Now;
        }

        public static DateTime ToCurrentTimeZone(this DateTime Now,TimeZoneInfo info)
        {
            if (Now.Kind == DateTimeKind.Utc || Now.Kind == DateTimeKind.Unspecified)
            {
                return TimeZoneInfo.ConvertTime(Now, info);
            }
            return Now;
        }

        public static DateTime? ToCurrentTimeZone(this DateTime? Now)
        {
            if (Now == null)
            {
                return null;
            }
            return Now.Value.ToCurrentTimeZone();
        }
        public static DateTime ToUtcDateTime(this DateTime Now,bool ViewAsUtc)
        {
            if (Now.Kind != DateTimeKind.Utc)
            {
                if (ViewAsUtc)
                {
                    var theUtcDateTime = new DateTime(Now.Ticks, DateTimeKind.Utc);
                    return theUtcDateTime;
                }
                else
                {
                    var theSourceTimeZoneInfo = GetCurrentTimeZoneEx();
                    return TimeZoneInfo.ConvertTime(Now, theSourceTimeZoneInfo);
                }
            }
            return Now;
        }
        public static DateTime? ToUtcDateTime(this DateTime? Now, bool ViewAsUtc)
        {
            if (Now == null)
            {
                return null;
            }
            return Now.Value.ToUtcDateTime(ViewAsUtc);
        }

        public static DateTime From2ToDateTime(this DateTime Now, TimeZoneInfo FromTimeZone, TimeZoneInfo DestTimezone)
        {
            return TimeZoneInfo.ConvertTime(Now, FromTimeZone, DestTimezone);
        }
        public static DateTime From2ToDateTime(this DateTime? Now, TimeZoneInfo FromTimeZone, TimeZoneInfo DestTimezone)
        {
            return Now.Value.From2ToDateTime(FromTimeZone, DestTimezone);
        }
    }
}
