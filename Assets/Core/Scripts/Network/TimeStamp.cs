using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tuwan
{
    public class TimeStamp
    {

        public static long getTimeStamp_Diff(long seconds)
        {
            var epoch = (DateTime.Now.AddSeconds(seconds).ToUniversalTime().Ticks - 621355968000000000) / 10000000;

            return epoch;
        }

        public static long getTimeStamp()
        {
            var epoch = (DateTime.Now.ToUniversalTime().Ticks - 621355968000000000) / 10000;

            return epoch;
        }

        public static long getTimeStamp(DateTime datetime)
        {
            var epoch = (datetime.ToUniversalTime().Ticks - 621355968000000000) / 10000;

            return epoch;
        }

        public static DateTime GetTime(string timeStamp)
        {
            DateTime dtStart = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
            long lTime = long.Parse(timeStamp + "0000000");
            TimeSpan toNow = new TimeSpan(lTime); return dtStart.Add(toNow);
        }


        public static DateTime GetTime(long timeStamp)
        {
            DateTime dtStart = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
            long lTime = timeStamp * 10000000;
            TimeSpan toNow = new TimeSpan(lTime); return dtStart.Add(toNow);
        }

    }
}
