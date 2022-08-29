using NodaTime;

namespace UniquomeApp.Utilities;
public static class InstantUtilities
{
    public static Instant GetDateOrMin(string date)
    {
        try
        {
            var inDatetime = Convert.ToDateTime(date);
            return Instant.FromUtc(inDatetime.Year, inDatetime.Month, inDatetime.Day, inDatetime.Hour, inDatetime.Minute, inDatetime.Second);
        }
        catch
        {
            return Instant.MinValue;
        }
    }

    public static Instant GetFromDateTime(DateTime inDatetime)
    {
        try
        {
            return Instant.FromUtc(inDatetime.Year, inDatetime.Month, inDatetime.Day, inDatetime.Hour, inDatetime.Minute, inDatetime.Second);
        }
        catch
        {
            return Instant.MinValue;
        }
    }

    public static bool IsSameDate(Instant dateA, Instant dateB)
    {
        if (dateA == dateB) return true;

        return dateA.InUtc().Year == dateB.InUtc().Year && dateA.InUtc().Month == dateB.InUtc().Month && dateA.InUtc().Day == dateB.InUtc().Day;
    }

    public static Instant GetEndOfMonth(int month, int year)
    {
        if (month != 12)
        {
            var tmpDate = Instant.FromUtc(year, month + 1, 1, 23, 59, 59);
            return tmpDate.Minus(Duration.FromDays(1));
        }
        else
        {
            var tmpDate = Instant.FromUtc(year, 12, 31, 23, 59, 59);
            return tmpDate;
        }
    }

    public static Instant GetNextBusinessDay(Instant startingDate, int nextDays = 1)
    {
        var result = startingDate.Plus(Duration.FromDays(nextDays));
        if (result.InUtc().DayOfWeek == IsoDayOfWeek.Saturday) result = result.Plus(Duration.FromDays(2));
        if (result.InUtc().DayOfWeek == IsoDayOfWeek.Sunday)  result = result.Plus(Duration.FromDays(1));
        return result;
    }

    public static Instant GetPreviousBusinessDay(Instant startingDate, int previousDays = 1)
    {
        var result = startingDate.Minus(Duration.FromDays(previousDays));
        if (result.InUtc().DayOfWeek == IsoDayOfWeek.Saturday) result = result.Minus(Duration.FromDays(1));
        if (result.InUtc().DayOfWeek == IsoDayOfWeek.Sunday) result = result.Minus(Duration.FromDays(2));
        return result;
    }

    public static Instant GetMonthLastBusinessDay(int year, int month)
    {
        var result = GetEndOfMonth(month, year);
        if (result.InUtc().DayOfWeek == IsoDayOfWeek.Saturday) result = result.Minus(Duration.FromDays(1));
        if (result.InUtc().DayOfWeek == IsoDayOfWeek.Sunday) result = result.Minus(Duration.FromDays(2));
        return result;
    }

    public static int GetWorkingDays(Instant fromDate, Instant toDate)
    {
        var workingDays = 0;
        var startDate = fromDate;
        while (startDate <= toDate)
        {
            if (startDate.InUtc().DayOfWeek != IsoDayOfWeek.Saturday && startDate.InUtc().DayOfWeek != IsoDayOfWeek.Sunday)
                workingDays++;
            startDate = startDate.Plus(Duration.FromDays(1));
        }
        return workingDays;
    }

    public static Instant GetEndOfDate(Instant instant)
    {
        return Instant.FromUtc(instant.InUtc().Year, instant.InUtc().Month, instant.InUtc().Day, 23, 59, 59);
    }
        
    public static Instant GetEndOfDate(Instant? instant)
    {
        return instant != null ? GetEndOfDate(instant) : Instant.MinValue;
    }

    public static Instant GetStartOfDate(Instant instant)
    {
        return Instant.FromUtc(instant.InUtc().Year, instant.InUtc().Month, instant.InUtc().Day, 0, 0, 0);
    }
        
    public static Instant GetStartOfTime(Instant instant)
    {
        return Instant.FromUtc(instant.InUtc().Year, instant.InUtc().Month, instant.InUtc().Day, instant.InUtc().Hour, 0, 0);
    }
        
    public static Instant GetEndOfTime(Instant instant)
    {
        return Instant.FromUtc(instant.InUtc().Year, instant.InUtc().Month, instant.InUtc().Day, instant.InUtc().Hour, 59, 59);
    }
        
    public static Instant GetStartOfDate(Instant? instant)
    {
        return instant == null ? Instant.MinValue : GetStartOfDate(instant.Value);
    }

    public static Instant GetStartOfMonth(Instant instant)
    {
        return Instant.FromUtc(instant.InUtc().Year, instant.InUtc().Month, 1, 0, 0, 0);
    }
        
    public static Instant GetEndOfMonth(Instant instant)
    {
        return GetEndOfMonth(instant.InUtc().Month, instant.InUtc().Year);
    }

    public static Instant GetStartOfMonth(int month, int year)
    {
        return Instant.FromUtc(year, month, 1, 0, 0, 0);
    }

    public static bool IsLastBusinessDayOfMonth(Instant instant, IList<Instant> enforcedLastBusinessDays = null)
    {
        if (enforcedLastBusinessDays != null)
        {
            if (enforcedLastBusinessDays.Any(enforcedLastBusinessDay =>
                    IsSameDate(instant, enforcedLastBusinessDay)))
                return true;
        }

        if (instant.InUtc().DayOfWeek != IsoDayOfWeek.Friday) return false;
        var date = instant.InUtc().Plus(Duration.FromDays(3));
        return date.Month != instant.InUtc().Month;
    }

    public static bool DatesInTheSameMonth(Instant dateA, Instant dateB)
    {
        return dateA.InUtc().Month == dateB.InUtc().Month && dateA.InUtc().Year == dateB.InUtc().Year;
    }

    public static bool DatesInThePreviousMonth(Instant dateA, Instant dateB)
    {
        if (dateA.InUtc().Month == 12)
            return dateA.InUtc().Year == dateB.InUtc().Year - 1;
        if (dateA.InUtc().Year != dateB.InUtc().Year) return false;
        return dateA.InUtc().Month == dateB.InUtc().Month - 1;
    }

    public static bool DatesInPreviousMonth(Instant dateA, Instant dateB)
    {
        return dateA < dateB && !DatesInTheSameMonth(dateA, dateB);
    }

    public static string GetShortMonth(Instant? date)
    {
        if (date == null)
            return "";
        var dd = date.Value.InUtc().Month;
        return dd switch
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

    public static string GetYear(Instant? date)
    {
        if (date == null)
            return "";
        var dd = date.Value.InUtc().Year;

        return dd.ToString();
    }

    public static bool IsBusinessDay(Instant date)
    {
        return !(date.InUtc().DayOfWeek == IsoDayOfWeek.Sunday || date.InUtc().DayOfWeek == IsoDayOfWeek.Saturday);
    }

    // public static Instant? GetProperInstantOrNull(string text)
    // {
    //     try
    //     {
    //         var date = Convert.ToInstant(text);
    //         if (date >= new Instant(1900, 1, 1, 23, 59, 59) || date <= new Instant(2100, 1, 1, 23, 59, 59))
    //             return date;
    //         return null;
    //     }
    //     catch (Exception)
    //     {
    //         return null;
    //     }
    //
    // }
    //
    // public static Instant GetProperInstant(string text, bool throwError = false)
    // {
    //     try
    //     {
    //         var date = Convert.ToInstant(text);
    //         if (date >= new Instant(1900, 1, 1, 23, 59, 59) || date <= new Instant(2100, 1, 1, 23, 59, 59))
    //             return date;
    //
    //         return Instant.Now;
    //     }
    //     catch (Exception ex)
    //     {
    //         if (throwError)
    //             throw new Exception("Unable to get Instant: " + ex.Message);
    //         return Instant.Now;
    //     }
    // }

    public static IList<Instant> GetEndOfMonthDatesInDateRange(Instant startDate, Instant endDate)
    {
        var dates = new List<Instant>();
        var currentDate = startDate;
        var previousDate = startDate;
        while (currentDate <= endDate)
        {
            currentDate = currentDate.Plus(Duration.FromDays(1));
            if (currentDate.InUtc().Month != previousDate.InUtc().Month)
                dates.Add(previousDate);
            previousDate = currentDate;
        }
        return dates;
    }

    public static int DaysBetween(Instant fromDate, Instant endDate)
    {
        return Math.Abs((fromDate - endDate).Days);
    }

    // public static bool GetInstant(object dateObject, out Instant properDate)
    // {
    //     try
    //     {
    //         var date = Convert.ToInstant(dateObject);
    //         properDate = date;
    //     }
    //     catch
    //     {
    //         properDate = Instant.MinValue;
    //         return false;
    //     }
    //     return true;
    // }

    public static bool IsInstant(object expression)
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

    public static int BusinessDaysBetween(Instant fromDate, Instant endDate)
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
                date = date.Plus(Duration.FromDays(1));
                if (date.InUtc().DayOfWeek != IsoDayOfWeek.Sunday && date.InUtc().DayOfWeek != IsoDayOfWeek.Saturday)
                    days++;
            }
        }
        else
        {
            var date = actualToDate;
            while (date < actualFromDate)
            {
                date = date.Plus(Duration.FromDays(1));
                if (date.InUtc().DayOfWeek != IsoDayOfWeek.Sunday && date.InUtc().DayOfWeek != IsoDayOfWeek.Saturday)
                    days++;
            }
        }

        if (actualFromDate < actualToDate)
            return days;
        return -days;
    }

    public static string GetUtcDateRepresentation(Instant date, bool useMillisecond = true)
    {
        var utcDate = date;
        var month = $"{utcDate.InUtc().Month}".PadLeft(2, '0');
        var day = $"{utcDate.InUtc().Day}".PadLeft(2, '0');
        var hour = $"{utcDate.InUtc().Hour}".PadLeft(2, '0');
        var minute = $"{utcDate.InUtc().Minute}".PadLeft(2, '0');
        var second = $"{utcDate.InUtc().Second}".PadLeft(2, '0');
        var milliSecond = $"{utcDate.InUtc().Millisecond}".PadLeft(3, '0');
        return useMillisecond ? $"{ utcDate.InUtc().Year}-{month}-{day}T{hour}:{minute}:{second}.{milliSecond}" : $"{ utcDate.InUtc().Year}-{month}-{day}T{hour}:{minute}:{second}";
    }

    public static bool IsAtLaterTime(Instant source, Instant target, bool ignoreDate = true)
    {
        //Source is greater than target
        if (ignoreDate)
            return source.InUtc().TimeOfDay > target.InUtc().TimeOfDay;
        return source > target;
    }

    public static Tuple<Instant, Instant> GetQuarterRange(Instant date)
    {
        var month = date.InUtc().Month;
        var fromDate = Instant.MinValue;
        var toDate = Instant.MaxValue;
        if (month >= 1 && month <= 3)
        {
            fromDate = GetStartOfMonth(1, date.InUtc().Year);
            toDate = GetEndOfMonth(3, date.InUtc().Year);
        }
        else if (month >= 4 && month <= 6)
        {
            fromDate = GetStartOfMonth(4, date.InUtc().Year);
            toDate = GetEndOfMonth(6, date.InUtc().Year);
        }
        else if (month >= 7 && month <= 9)
        {
            fromDate = GetStartOfMonth(7, date.InUtc().Year);
            toDate = GetEndOfMonth(9, date.InUtc().Year);
        }
        else if (month >= 10)
        {
            fromDate = GetStartOfMonth(10, date.InUtc().Year);
            toDate = GetEndOfMonth(12, date.InUtc().Year);
        }

        return new Tuple<Instant, Instant>(fromDate, toDate);
    }

    public static Tuple<Instant, Instant> GetQuarterRange(int quarter, int year)
    {
        Instant fromDate;
        Instant toDate;
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
        return new Tuple<Instant, Instant>(fromDate, toDate);
    }

    public static int GetDateQuarter(Instant date)
    {
        var month = date.InUtc().Month;
        if (month >= 1 && month <= 3)
            return 1;
        if (month >= 4 && month <= 6)
            return 2;
        if (month >= 7 && month <= 9)
            return 3;
        return 4;
    }
    // public static Tuple<Instant, Instant> GetWeekRange(Instant date, InstantFormatInfo givenSpecificDateFormat = null)
    // {
    //     var dfi = InstantFormatInfo.CurrentInfo;
    //     if (givenSpecificDateFormat != null)
    //         dfi = givenSpecificDateFormat;
    //     if (dfi == null) return null;
    //     var cal = dfi.Calendar;
    //     var weekNo = cal.GetWeekOfYear(date, dfi.CalendarWeekRule, dfi.FirstDayOfWeek);
    //     var firstDate = GetStartOfDate(FirstDateOfWeekIso8601(date.Year, weekNo));
    //     var lastDate = GetEndOfDate(firstDate.AddDays(6));
    //     return new Tuple<Instant, Instant>(firstDate, lastDate);
    // }
        
    public static Tuple<Instant, Instant> GetMonthRange(Instant date)
    {
        return new Tuple<Instant, Instant>(GetStartOfMonth(date), GetEndOfMonth(date));
    }
        
    // public static Tuple<Instant, Instant> GetYearRange(Instant date)
    // {
    //     return new Tuple<Instant, Instant>(GetStartOfDate(new Instant(date.InUtc().Year, 1, 1)), GetEndOfDate(Instant(date.InUtc().Year, 12, 31)));
    // }
    //     
    // public static Tuple<Instant, Instant> GetYearRange(int year)
    // {
    //     return new Tuple<Instant, Instant>(GetStartOfDate(new Instant(year, 1, 1)), GetEndOfDate(new Instant(year, 12, 31)));
    // }
    //     
    // public static Tuple<Instant, Instant> GetBackwardRollingMonth(Instant date, int months)
    // {
    //     return new Tuple<Instant, Instant>(GetStartOfDate(date.AddMonths(-months)), GetEndOfDate(date));
    // }
    //     
    // public static Tuple<Instant, Instant> GetForwardRollingMonth(Instant date, int months)
    // {
    //     return new Tuple<Instant, Instant>(GetStartOfDate(date.AddMonths(months)), GetEndOfDate(date));
    // }
        
    public static Tuple<Instant, Instant> GetBackwardRollingDays(Instant date, int days)
    {
        return new Tuple<Instant, Instant>(GetStartOfDate(date.Minus(Duration.FromDays(days))), GetEndOfDate(date));
    }
        
    public static Tuple<Instant, Instant> GetForwardRollingDays(Instant date, int days)
    {
        return new Tuple<Instant, Instant>(GetStartOfDate(date.Plus(Duration.FromDays(days))), GetEndOfDate(date));
    }
        
    public static Tuple<Instant, Instant> GetBackwardRollingHours(Instant date, int hours)
    {
        return new Tuple<Instant, Instant>(date.Minus(Duration.FromHours(hours)), date);
    }
        
    public static Tuple<Instant, Instant> GetForwardRollingHours(Instant date, int hours)
    {
        return new Tuple<Instant, Instant>(date.Plus(Duration.FromHours(hours)), date);
    }
        
    // public static Tuple<Instant, Instant> GetBackwardRollingYear(Instant date, int years)
    // {
    //     return new Tuple<Instant, Instant>(GetStartOfDate(date.Minus(Duration.FromHours(hours))), GetEndOfDate(date));
    // }
    //     
    // public static Tuple<Instant, Instant> GetForwardRollingYear(Instant date, int years)
    // {
    //     return new Tuple<Instant, Instant>(GetStartOfDate(date.AddYears(years)), GetEndOfDate(date));
    // }
        
    public static Tuple<Instant, Instant> GetBackwardRollingQuarter(Instant date, int quarters)
    {
        var landingQuarter = GetDateQuarter(date);
        var landingYear = date.InUtc().Year;
        for (var i = 0; i < quarters; i++)
        {
            landingQuarter--;
            if (landingQuarter != 0) continue;
            landingQuarter = 4;
            landingYear -= 1;
        }
        return GetQuarterRange(landingQuarter, landingYear);
    }
        
    public static string GetStringRepresentationForFilename(Instant date, bool includeTime = true)
    {
        var year = date.InUtc().Year;
        var month = $"{date.InUtc().Month}";
        if (date.InUtc().Month < 10)
            month = "0" + date.InUtc().Month;
        var day = $"{date.InUtc().Day}";
        if (date.InUtc().Day < 10)
            day = "0" + date.InUtc().Day;

        var dateInString = $"{year}_{month}_{day}";
        if (!includeTime) return dateInString;
        var hour = $"{date.InUtc().Hour}";
        if (date.InUtc().Hour < 10)
            hour = "0" + date.InUtc().Hour;
        var minute = $"{date.InUtc().Minute}";
        if (date.InUtc().Minute < 10)
            minute = "0" + date.InUtc().Minute;
        var sec = $"{date.InUtc().Second}";
        if (date.InUtc().Second < 10)
            sec = "0" + date.InUtc().Second;
        dateInString += $"_{hour}_{minute}_{sec}";

        return dateInString;
    }
    /*
    public static int GetIso8601WeekOfYear(Instant time)
    {
        // Seriously cheat.  If its Monday, Tuesday or Wednesday, then it'll 
        // be the same week# as whatever Thursday, Friday or Saturday are,
        // and we always get those right
        var day = CultureInfo.InvariantCulture.Calendar.GetDayOfWeek(time);
        if (day >= DayOfWeek.Monday && day <= DayOfWeek.Wednesday)
        {
            time = time.Plus(Duration.FromDays(3));
        }
        // Return the week of our adjusted day
        return CultureInfo.InvariantCulture.Calendar.GetWeekOfYear(time, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);
    }

    public static Instant FirstDateOfWeekIso8601(int year, int weekOfYear)
    {
        var jan1 = Instant.FromUtc(year, 1, 1, 0, 0, 0);
        var daysOffset = IsoDayOfWeek.Thursday - jan1.InUtc().DayOfWeek;

        // Use first Thursday in January to get first week of the year as
        // it will never be in Week 52/53
        var firstThursday = jan1.Plus(Duration.FromDays(daysOffset));
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
        var result = firstThursday.Plus(Duration.FromDays(weekNum * 7));

        // Subtract 3 days from Thursday to get Monday, which is the first weekday in ISO8601
        return result.Minus(Duration.FromDays(3));
    }
    */
}