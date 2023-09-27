using Microsoft.Extensions.Configuration;
using System;
using TimeZoneConverter;

namespace Data.Utils
{
    public interface ILocalTimeHelper
    {
        DateTime GetLocalDateTime();
        DateTimeOffset GetLocalDateTimeOffset();
    }

    public class LocalTimeHelper : ILocalTimeHelper
    {
        private readonly TimeZoneInfo _tzi;

        public LocalTimeHelper()
        {
            //Local Standard Time
            _tzi = TZConvert.GetTimeZoneInfo("India Standard Time");
        }


        public DateTime GetLocalDateTime()
        {
            return TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, _tzi);
        }

        public DateTimeOffset GetLocalDateTimeOffset()
        {
            var datetime = GetLocalDateTime();
            return new DateTimeOffset(datetime, _tzi.GetUtcOffset(datetime));
        }
    }
}