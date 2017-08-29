using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace iFramework.Util
{
    public static class DateConverter
    {
        /// <summary>
        /// UTC时间转北京时间
        /// </summary>
        /// <param name="utc"></param>
        /// <returns></returns>
        public static DateTime UTCToLocalTime(DateTime utc)
        {
            double utcDouble = ConvertDateTimeInt(utc);
            return ConvertIntDatetime(utcDouble);
        }

        public static double ConvertDateTimeInt(System.DateTime time)
        {
            double intResult = 0;
            System.DateTime startTime = new System.DateTime(1970, 1, 1).ToLocalTime();
            intResult = (time - startTime).TotalSeconds;
            return intResult;
        }
        public static DateTime ConvertIntDatetime(double utc)
        {
            System.DateTime startTime = new System.DateTime(1970, 1, 1).ToLocalTime();
            startTime = startTime.AddSeconds(utc);
            startTime = startTime.AddHours(8);//转化为北京时间(北京时间=UTC时间+8小时 )
            return startTime;
        }

        /// <summary>
        /// 将UTC时间转换成系统时间
        /// </summary>
        /// <param name="device"></param>
        public static void ConventUTCToLocalTime<T>(T device)
        {
            var type = device.GetType();

            var Properties = type.GetProperties();

            foreach (var Propertie in Properties)
            {
                if (Propertie.CanRead && Propertie.CanWrite)
                {
                    if (Propertie.PropertyType == typeof(DateTime?))
                    {
                        Propertie.SetValue(device, ((DateTime?)Propertie.GetValue(device, null))?.ToLocalTime(), null);
                    }
                }
            }
        }

        /// <summary>
        /// 将系统时间转换成UTC时间
        /// </summary>
        /// <param name="device"></param>
        public static void ConventLocalToUTCTime<T>(T device)
        {
            var type = device.GetType();
            var Properties = type.GetProperties();
            foreach (var Propertie in Properties)
            {
                if (Propertie.CanRead && Propertie.CanWrite)
                {
                    if (Propertie.PropertyType == typeof(DateTime?))
                    {
                        Propertie.SetValue(device, ((DateTime?)Propertie.GetValue(device, null))?.ToUniversalTime(), null);
                    }
                }
            }
        }
    }
}