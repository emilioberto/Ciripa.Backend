using System;
using System.Globalization;

namespace Ciripa.Domain
{
    public readonly struct Date
    {
        private readonly DateTime _dateTime;

        public int Year => _dateTime.Year;
        public int Month => _dateTime.Month;
        public int Day => _dateTime.Day;

        public Date(int year, int month, int day) =>
            _dateTime = new DateTime(year, month, day, 0, 0, 0, DateTimeKind.Utc);

        public Date(DateTime dateTime)
        {
            if (dateTime.Kind == DateTimeKind.Local)
            {
                dateTime = dateTime.ToUniversalTime();
            }

            _dateTime = dateTime.Date;
        }

        public DateTime AsDateTime() =>
            _dateTime;

        public static bool operator ==(Date left, Date right) =>
            left._dateTime == right._dateTime;

        public static bool operator !=(Date left, Date right) =>
            left._dateTime != right._dateTime;

        public static bool operator >(Date left, Date right) =>
            left._dateTime > right._dateTime;

        public static bool operator <(Date left, Date right) =>
            left._dateTime < right._dateTime;

        public static bool operator >=(Date left, Date right) =>
            left._dateTime >= right._dateTime;

        public static bool operator <=(Date left, Date right) =>
            left._dateTime <= right._dateTime;

        public static implicit operator DateTime(Date source) =>
            source._dateTime;

        public static implicit operator Date(DateTime source) =>
            new Date(source);

        public override bool Equals(object obj) =>
            obj is Date date && date == this;

        public override int GetHashCode() =>
            _dateTime.GetHashCode();

        public override string ToString() =>
            _dateTime.ToString(CultureInfo.InvariantCulture);

        public static readonly Date MaxValue = new Date(DateTime.MaxValue);
        public static readonly Date MinValue = new Date(DateTime.MinValue);
        public static Date Now => DateTime.UtcNow;
    }
}