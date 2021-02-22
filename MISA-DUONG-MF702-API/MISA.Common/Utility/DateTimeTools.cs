using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace MISA.Common.Utility
{
    public static class DateTimeTools
    {
        public static DateTime GetFirstDayOfMonth(int iMonth, int iYear)
        {
            DateTime dtFrom = new DateTime(iYear, iMonth, 1);
            dtFrom = dtFrom.AddDays(-(dtFrom.Day - 1));

            return dtFrom;
        }

        public static DateTime GetFirstDayOfMonth(int iMonth)
        {
            DateTime dtFrom = new DateTime(DateTime.Now.Year, iMonth, 1);
            dtFrom = dtFrom.AddDays(-(dtFrom.Day - 1));

            return dtFrom;
        }

        public static DateTime GetLastDayOfMonth(DateTime dtDate)
        {
            DateTime dtTo = dtDate;

            dtTo = dtTo.AddMonths(1);
            dtTo = dtTo.AddDays(-(dtTo.Day));

            return dtTo;
        }

        public static DateTime GetFirstDateOfWeek(DateTime dayInWeek, DayOfWeek firstDay)
        {
            DateTime firstDayInWeek = dayInWeek.Date;
            while (firstDayInWeek.DayOfWeek != firstDay)
                firstDayInWeek = firstDayInWeek.AddDays(-1);

            return firstDayInWeek;
        }

        public static DateTime GetLastDateOfWeek(DateTime dayInWeek, DayOfWeek firstDay)
        {
            DateTime lastDayInWeek = dayInWeek.Date;
            while (lastDayInWeek.DayOfWeek != firstDay)
                lastDayInWeek = lastDayInWeek.AddDays(1);

            return lastDayInWeek;
        }

        public static DateTime EndOfDay(this DateTime date)
        {
            return new DateTime(date.Year, date.Month, date.Day, 23, 59, 59, 999);
        }

        public static DateTime StartOfDay(this DateTime date)
        {
            return new DateTime(date.Year, date.Month, date.Day, 0, 0, 0, 0);
        }

        public static DateTime ConvertToDatetime(string value)
        {
            if (string.IsNullOrEmpty(value))
                return DateTime.Now;
            try
            {
                value = value.Trim().Replace("-", "/");
                string[] arr = value.Split('/');
                return new DateTime(int.Parse(arr[2]), int.Parse(arr[1]), int.Parse(arr[0]));
            }
            catch
            {
                return DateTime.Now;
            }

        }

        public static DateTime? CFToDateTime(string value)
        {
            if (string.IsNullOrEmpty(value))
                return null;
            else
            {
                try
                {
                    value = value.Trim().Replace("-", "/");
                    string[] arr = value.Split('/');
                    if (arr.Length != 3)
                        return null;
                    else
                    {
                        return new DateTime(int.Parse(arr[2]), int.Parse(arr[1]), int.Parse(arr[0]));
                    }
                }
                catch
                {
                    return null;
                }
            }
        }

        public static DateTime? ConvertCftoDateTime(string value, string fomart)
        {
            if (string.IsNullOrEmpty(value))
                return null;
            else
            {
                try
                {
                    return DateTime.ParseExact(value, fomart, CultureInfo.InvariantCulture);
                }
                catch
                {
                    return null;
                }
            }
        }

        public static DateTime? ConvertDateTimeToTimeSpan(string value)
        {
            if (string.IsNullOrEmpty(value))
                return null;
            else
            {
                try
                {
                    double timestamp = Convert.ToDouble(value);

                    // Format our new DateTime object to start at the UNIX Epoch
                    var dateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0);

                    // Add the timestamp (number of seconds since the Epoch) to be converted
                    return dateTime.AddSeconds(timestamp);
                }
                catch
                {
                    return null;
                }
            }
        }
    }
}
