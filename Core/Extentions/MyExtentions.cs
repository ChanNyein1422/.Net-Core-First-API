using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Extensions
{
    public class MyExtension
    {
        public static DateTime getLocalTime()
        {
            DateTime utc = DateTime.UtcNow;
            return TimeZoneInfo.ConvertTimeFromUtc(utc, TimeZoneInfo.FindSystemTimeZoneById("Myanmar Standard Time"));
        }
        public static string getUniqueCode()
        {
            byte[] buffer = Guid.NewGuid().ToByteArray();
            return BitConverter.ToUInt32(buffer, 12).ToString();
        }


    }
}
