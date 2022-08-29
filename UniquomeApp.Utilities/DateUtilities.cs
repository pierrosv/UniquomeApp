using System.Globalization;

// ReSharper disable MemberCanBePrivate.Global
// ReSharper disable UnusedMember.Global
// ReSharper disable UnusedType.Global

namespace UniquomeApp.Utilities;

public static class DateUtilities
{
    public static DateTime GetDateOrMin(string date)
    {
        try
        {
            return Convert.ToDateTime(date);
        }
        catch
        {
            return DateTime.MinValue;
        }
    }

    public static bool IsSameDate(DateTime dateA, DateTime dateB)
    {
        if (dateA == dateB) return true;

        return dateA.Year == dateB.Year && dateA.Month == dateB.Month && dateA.Day == dateB.Day;
    }

    public static DateTime GetEndOfMonth(int month, int year)
    {
        if (month != 12)
        {
            var tmpDate = new DateTime(year, month + 1, 1, 23, 59, 59);
            return tmpDate.AddDays(-1);
        }
        else
        {
            var tmpDate = new DateTime(year, 12, 31, 23, 59, 59);
            return tmpDate;
        }
    }

    public static DateTime GetNextBusinessDay(DateTime startingDate, int nextDays = 1)
    {
        var result = startingDate.AddDays(nextDays);
        if (result.DayOfWeek == DayOfWeek.Saturday) result = result.AddDays(2);
        if (result.DayOfWeek == DayOfWeek.Sunday) result = result.AddDays(1);
        return result;
    }

    public static DateTime GetPreviousBusinessDay(DateTime startingDate, int previousDays = 1)
    {
        var result = startingDate.AddDays(-previousDays);
        if (result.DayOfWeek == DayOfWeek.Saturday) result = result.AddDays(-1);
        if (result.DayOfWeek == DayOfWeek.Sunday) result = result.AddDays(-2);
        return result;
    }

    public static DateTime GetMonthLastBusinessDay(int year, int month)
    {
        var result = GetEndOfMonth(month, year);
        if (result.DayOfWeek == DayOfWeek.Saturday) result = result.AddDays(-1);
        if (result.DayOfWeek == DayOfWeek.Sunday) result = result.AddDays(-2);
        return result;
    }

    public static int GetWorkingDays(DateTime fromDate, DateTime toDate)
    {
        var workingDays = 0;
        var startDate = fromDate;
        while (startDate <= toDate)
        {
            if (startDate.DayOfWeek != DayOfWeek.Saturday && startDate.DayOfWeek != DayOfWeek.Sunday)
                workingDays++;
            startDate = startDate.AddDays(1);
        }
        return workingDays;
    }

    public static DateTime GetEndOfDate(DateTime dateTime)
    {
        return new DateTime(dateTime.Year, dateTime.Month, dateTime.Day, 23, 59, 59);
    }

    public static DateTime GetEndOfDate(DateTime? dateTime)
    {
        return dateTime != null ? GetEndOfDate(Convert.ToDateTime(dateTime)) : DateTime.MinValue;
    }

    public static DateTime GetStartOfDate(DateTime dateTime)
    {
        return new DateTime(dateTime.Year, dateTime.Month, dateTime.Day, 0, 0, 0);
    }

    public static DateTime GetStartOfTime(DateTime dateTime)
    {
        return new DateTime(dateTime.Year, dateTime.Month, dateTime.Day, dateTime.Hour, 0, 0);
    }

    public static DateTime GetEndOfTime(DateTime dateTime)
    {
        return new DateTime(dateTime.Year, dateTime.Month, dateTime.Day, dateTime.Hour, 59, 59);
    }

    public static DateTime GetStartOfDate(DateTime? dateTime)
    {
        return dateTime == null ? DateTime.MinValue : GetStartOfDate(Convert.ToDateTime(dateTime));
    }

    public static DateTime GetStartOfMonth(DateTime dateTime)
    {
        return new DateTime(dateTime.Year, dateTime.Month, 1, 0, 0, 0);
    }

    public static DateTime GetEndOfMonth(DateTime dateTime)
    {
        return GetEndOfMonth(dateTime.Month, dateTime.Year);
    }

    public static DateTime GetStartOfMonth(int month, int year)
    {
        return new DateTime(year, month, 1, 0, 0, 0);
    }

    public static bool IsLastBusinessDayOfMonth(DateTime dateTime, IList<DateTime> enforcedLastBusinessDays = null)
    {
        if (enforcedLastBusinessDays != null)
        {
            if (enforcedLastBusinessDays.Any(enforcedLastBusinessDay =>
                    IsSameDate(dateTime, enforcedLastBusinessDay)))
                return true;
        }

        if (dateTime.DayOfWeek != DayOfWeek.Friday) return false;
        var date = dateTime.AddDays(3);
        return date.Month != dateTime.Month;
    }

    public static bool DatesInTheSameMonth(DateTime dateA, DateTime dateB)
    {
        return dateA.Month == dateB.Month && dateA.Year == dateB.Year;
    }

    public static bool DatesInThePreviousMonth(DateTime dateA, DateTime dateB)
    {
        if (dateA.Month == 12)
            return dateA.Year == dateB.Year - 1;
        if (dateA.Year != dateB.Year) return false;
        return dateA.Month == dateB.Month - 1;
    }

    public static bool DatesInPreviousMonth(DateTime dateA, DateTime dateB)
    {
        return dateA < dateB && !DatesInTheSameMonth(dateA, dateB);
    }

    public static string GetShortMonth(DateTime? date)
    {
        if (date == null)
            return "";
        var dd = Convert.ToDateTime(date);
        return dd.Month switch
        {
            1 => "JAN",
            2 => "FEB",
            3 => "MAR",
            4 => "APR",
            5 => "MAY",
            6 => "JUN",
            7 => "JUL",
            8 => "AUG",
            9 => "SEP",
            10 => "OCT",
            11 => "NOV",
            12 => "DEC",
            _ => ""
        };
    }

    public static string GetYear(DateTime? date)
    {
        if (date == null)
            return "";
        var dd = Convert.ToDateTime(date);

        return dd.Year.ToString();
    }

    public static bool IsBusinessDay(DateTime date)
    {
        return !(date.DayOfWeek == DayOfWeek.Sunday || date.DayOfWeek == DayOfWeek.Saturday);
    }

    public static DateTime? GetProperDateTimeOrNull(string text)
    {
        try
        {
            var date = Convert.ToDateTime(text);
            if (date >= new DateTime(1900, 1, 1, 23, 59, 59) || date <= new DateTime(2100, 1, 1, 23, 59, 59))
                return date;
            return null;
        }
        catch (Exception)
        {
            return null;
        }

    }

    public static DateTime GetProperDateTime(string text, bool throwError = false)
    {
        try
        {
            var date = Convert.ToDateTime(text);
            if (date >= new DateTime(1900, 1, 1, 23, 59, 59) || date <= new DateTime(2100, 1, 1, 23, 59, 59))
                return date;

            return DateTime.Now;
        }
        catch (Exception ex)
        {
            if (throwError)
                throw new Exception("Unable to get DateTime: " + ex.Message);
            return DateTime.Now;
        }
    }

    public static IList<DateTime> GetEndOfMonthDatesInDateRange(DateTime startDate, DateTime endDate)
    {
        var dates = new List<DateTime>();
        var currentDate = startDate;
        var previousDate = startDate;
        while (currentDate <= endDate)
        {
            currentDate = currentDate.AddDays(1);
            if (currentDate.Month != previousDate.Month)
                dates.Add(previousDate);
            previousDate = currentDate;
        }
        return dates;
    }

    public static int DaysBetween(DateTime fromDate, DateTime endDate)
    {
        return Math.Abs((fromDate - endDate).Days);
    }

    public static bool GetDateTime(object dateObject, out DateTime properDate)
    {
        try
        {
            var date = Convert.ToDateTime(dateObject);
            properDate = date;
        }
        catch
        {
            properDate = DateTime.MinValue;
            return false;
        }
        return true;
    }

    public static bool IsDateTime(object expression)
    {
        if (expression == null || expression is decimal || expression is double || expression is int)
            return false;

        try
        {
            return DateTime.TryParse(expression.ToString(), out _);
        }
        catch
        {
            return false;
        }
    }

    public static int BusinessDaysBetween(DateTime fromDate, DateTime endDate)
    {
        if (IsSameDate(fromDate, endDate)) return 0;
        //Get End of Date For both 
        var actualFromDate = GetEndOfDate(fromDate);
        var actualToDate = GetEndOfDate(endDate);

        var days = 0;
        if (actualFromDate < actualToDate)
        {
            var date = actualFromDate;
            while (date < actualToDate)
            {
                date = date.AddDays(1);
                if (date.DayOfWeek != DayOfWeek.Sunday && date.DayOfWeek != DayOfWeek.Saturday)
                    days++;
            }
        }
        else
        {
            var date = actualToDate;
            while (date < actualFromDate)
            {
                date = date.AddDays(1);
                if (date.DayOfWeek != DayOfWeek.Sunday && date.DayOfWeek != DayOfWeek.Saturday)
                    days++;
            }
        }

        if (actualFromDate < actualToDate)
            return days;
        return -days;
    }

    public static string GetUtcDateRepresentation(DateTime date, bool useMillisecond = true)
    {
        var utcDate = date;
        var month = $"{utcDate.Month}".PadLeft(2, '0');
        var day = $"{utcDate.Day}".PadLeft(2, '0');
        var hour = $"{utcDate.Hour}".PadLeft(2, '0');
        var minute = $"{utcDate.Minute}".PadLeft(2, '0');
        var second = $"{utcDate.Second}".PadLeft(2, '0');
        var milliSecond = $"{utcDate.Millisecond}".PadLeft(3, '0');
        return useMillisecond ? $"{ utcDate.Year}-{month}-{day}T{hour}:{minute}:{second}.{milliSecond}" : $"{ utcDate.Year}-{month}-{day}T{hour}:{minute}:{second}";
    }

    public static bool IsAtLaterTime(DateTime source, DateTime target, bool ignoreDate = true)
    {
        //Source is greater than target
        if (ignoreDate)
            return source.TimeOfDay > target.TimeOfDay;
        return source > target;
    }

    public static Tuple<DateTime, DateTime> GetQuarterRange(DateTime date)
    {
        var month = date.Month;
        var fromDate = DateTime.MinValue;
        var toDate = DateTime.MaxValue;
        if (month >= 1 && month <= 3)
        {
            fromDate = GetStartOfMonth(1, date.Year);
            toDate = GetEndOfMonth(3, date.Year);
        }
        else if (month >= 4 && month <= 6)
        {
            fromDate = GetStartOfMonth(4, date.Year);
            toDate = GetEndOfMonth(6, date.Year);
        }
        else if (month >= 7 && month <= 9)
        {
            fromDate = GetStartOfMonth(7, date.Year);
            toDate = GetEndOfMonth(9, date.Year);
        }
        else if (month >= 10)
        {
            fromDate = GetStartOfMonth(10, date.Year);
            toDate = GetEndOfMonth(12, date.Year);
        }

        return new Tuple<DateTime, DateTime>(fromDate, toDate);
    }

    public static Tuple<DateTime, DateTime> GetQuarterRange(int quarter, int year)
    {
        DateTime fromDate;
        DateTime toDate;
        switch (quarter)
        {
            case 1:
                fromDate = GetStartOfMonth(1, year);
                toDate = GetEndOfMonth(3, year);
                break;
            case 2:
                fromDate = GetStartOfMonth(4, year);
                toDate = GetEndOfMonth(6, year);
                break;
            case 3:
                fromDate = GetStartOfMonth(7, year);
                toDate = GetEndOfMonth(9, year);
                break;
            default:
                fromDate = GetStartOfMonth(10, year);
                toDate = GetEndOfMonth(12, year);
                break;
        }
        return new Tuple<DateTime, DateTime>(fromDate, toDate);
    }

    public static int GetDateQuarter(DateTime date)
    {
        var month = date.Month;
        if (month >= 1 && month <= 3)
            return 1;
        if (month >= 4 && month <= 6)
            return 2;
        if (month >= 7 && month <= 9)
            return 3;
        return 4;
    }
    public static Tuple<DateTime, DateTime> GetWeekRange(DateTime date, DateTimeFormatInfo givenSpecificDateFormat = null)
    {
        var dfi = DateTimeFormatInfo.CurrentInfo;
        if (givenSpecificDateFormat != null)
            dfi = givenSpecificDateFormat;
        if (dfi == null) return null;
        var cal = dfi.Calendar;
        var weekNo = cal.GetWeekOfYear(date, dfi.CalendarWeekRule, dfi.FirstDayOfWeek);
        var firstDate = GetStartOfDate(FirstDateOfWeekIso8601(date.Year, weekNo));
        var lastDate = GetEndOfDate(firstDate.AddDays(6));
        return new Tuple<DateTime, DateTime>(firstDate, lastDate);
    }

    public static Tuple<DateTime, DateTime> GetMonthRange(DateTime date)
    {
        return new Tuple<DateTime, DateTime>(GetStartOfMonth(date), GetEndOfMonth(date));
    }

    public static Tuple<DateTime, DateTime> GetYearRange(DateTime date)
    {
        return new Tuple<DateTime, DateTime>(GetStartOfDate(new DateTime(date.Year, 1, 1)), GetEndOfDate(new DateTime(date.Year, 12, 31)));
    }

    public static Tuple<DateTime, DateTime> GetYearRange(int year)
    {
        return new Tuple<DateTime, DateTime>(GetStartOfDate(new DateTime(year, 1, 1)), GetEndOfDate(new DateTime(year, 12, 31)));
    }

    public static Tuple<DateTime, DateTime> GetBackwardRollingMonth(DateTime date, int months)
    {
        return new Tuple<DateTime, DateTime>(GetStartOfDate(date.AddMonths(-months)), GetEndOfDate(date));
    }

    public static Tuple<DateTime, DateTime> GetForwardRollingMonth(DateTime date, int months)
    {
        return new Tuple<DateTime, DateTime>(GetStartOfDate(date.AddMonths(months)), GetEndOfDate(date));
    }

    public static Tuple<DateTime, DateTime> GetBackwardRollingDays(DateTime date, int days)
    {
        return new Tuple<DateTime, DateTime>(GetStartOfDate(date.AddDays(-days)), GetEndOfDate(date));
    }

    public static Tuple<DateTime, DateTime> GetForwardRollingDays(DateTime date, int days)
    {
        return new Tuple<DateTime, DateTime>(GetStartOfDate(date.AddDays(days)), GetEndOfDate(date));
    }

    public static Tuple<DateTime, DateTime> GetBackwardRollingHours(DateTime date, int hours)
    {
        return new Tuple<DateTime, DateTime>(date.AddHours(-hours), date);
    }

    public static Tuple<DateTime, DateTime> GetForwardRollingHours(DateTime date, int hours)
    {
        return new Tuple<DateTime, DateTime>(date.AddHours(hours), date);
    }

    public static Tuple<DateTime, DateTime> GetBackwardRollingYear(DateTime date, int years)
    {
        return new Tuple<DateTime, DateTime>(GetStartOfDate(date.AddYears(-years)), GetEndOfDate(date));
    }

    public static Tuple<DateTime, DateTime> GetForwardRollingYear(DateTime date, int years)
    {
        return new Tuple<DateTime, DateTime>(GetStartOfDate(date.AddYears(years)), GetEndOfDate(date));
    }

    public static Tuple<DateTime, DateTime> GetBackwardRollingQuarter(DateTime date, int quarters)
    {
        var landingQuarter = GetDateQuarter(date);
        var landingYear = date.Year;
        for (var i = 0; i < quarters; i++)
        {
            landingQuarter--;
            if (landingQuarter != 0) continue;
            landingQuarter = 4;
            landingYear -= 1;
        }
        return GetQuarterRange(landingQuarter, landingYear);
    }

    public static string GetStringRepresentationForFilename(DateTime date, bool includeTime = true)
    {
        var year = date.Year;
        var month = $"{date.Month}";
        if (date.Month < 10)
            month = "0" + date.Month;
        var day = $"{date.Day}";
        if (date.Day < 10)
            day = "0" + date.Day;

        var dateInString = $"{year}_{month}_{day}";
        if (!includeTime) return dateInString;
        var hour = $"{date.Hour}";
        if (date.Hour < 10)
            hour = "0" + date.Hour;
        var minute = $"{date.Minute}";
        if (date.Minute < 10)
            minute = "0" + date.Minute;
        var sec = $"{date.Second}";
        if (date.Second < 10)
            sec = "0" + date.Second;
        dateInString += $"_{hour}_{minute}_{sec}";

        return dateInString;
    }

    public static int GetIso8601WeekOfYear(DateTime time)
    {
        // Seriously cheat.  If its Monday, Tuesday or Wednesday, then it'll 
        // be the same week# as whatever Thursday, Friday or Saturday are,
        // and we always get those right
        var day = CultureInfo.InvariantCulture.Calendar.GetDayOfWeek(time);
        if (day >= DayOfWeek.Monday && day <= DayOfWeek.Wednesday)
        {
            time = time.AddDays(3);
        }
        // Return the week of our adjusted day
        return CultureInfo.InvariantCulture.Calendar.GetWeekOfYear(time, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);
    }

    public static DateTime FirstDateOfWeekIso8601(int year, int weekOfYear)
    {
        var jan1 = new DateTime(year, 1, 1);
        var daysOffset = DayOfWeek.Thursday - jan1.DayOfWeek;

        // Use first Thursday in January to get first week of the year as
        // it will never be in Week 52/53
        var firstThursday = jan1.AddDays(daysOffset);
        var cal = CultureInfo.CurrentCulture.Calendar;
        var firstWeek = cal.GetWeekOfYear(firstThursday, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);

        var weekNum = weekOfYear;
        // As we're adding days to a date in Week 1,
        // we need to subtract 1 in order to get the right date for week #1
        if (firstWeek == 1)
        {
            weekNum -= 1;
        }
        // Using the first Thursday as starting week ensures that we are starting in the right year
        // then we add number of weeks multiplied with days
        var result = firstThursday.AddDays(weekNum * 7);

        // Subtract 3 days from Thursday to get Monday, which is the first weekday in ISO8601
        return result.AddDays(-3);
    }
}