using System.Globalization;

// ReSharper disable MemberCanBePrivate.Global
// ReSharper disable UnusedMember.Global
// ReSharper disable UnusedType.Global

namespace UniquomeApp.Utilities;

public static class NumericUtilities
{
    public static bool IsNumeric(object expression)
    {
        switch (expression)
        {
            case null:
            case DateTime _:
                return false;
            case short _:
            case int _:
            case long _:
            case decimal _:
            case float _:
            case double _:
                return true;
            default:
                try
                {
                    return double.TryParse(expression.ToString(), out _);
                }
                catch
                {
                    return false;
                }
        }
    }

    public static bool IsDecimal(object expression)
    {
        switch (expression)
        {
            case null:
            case DateTime _:
                return false;
            case decimal _:
                return true;
            default:
                try
                {
                    return decimal.TryParse(expression.ToString(), out _);
                }
                catch
                {
                    return false;
                }
        }
    }

    public static bool IsInteger(object expression)
    {
        switch (expression)
        {
            case null:
            case DateTime _:
                return false;
            case short _:
            case int _:
                return true;
            default:
                try
                {
                    return int.TryParse(expression.ToString(), out _);
                }
                catch
                {
                    return false;
                }
        }
    }

    public static decimal GetDecimalOrZero(string s)
    {
        //Number is first converted to double and then to decimal to cover cases when string contains E+
        var nfi = (NumberFormatInfo) CultureInfo.InvariantCulture.NumberFormat.Clone();
        nfi.NumberGroupSeparator = ",";
        nfi.NumberDecimalSeparator = ".";
        return IsNumeric(s) ? Convert.ToDecimal(Convert.ToDouble(s, nfi)) : 0;
    }
    public static double GetDoubleOrZero(string s)
    {
        //Number is first converted to double and then to decimal to cover cases when string contains E+
        var nfi = (NumberFormatInfo)CultureInfo.InvariantCulture.NumberFormat.Clone();
        nfi.NumberGroupSeparator = ",";
        nfi.NumberDecimalSeparator = ".";
        if (IsNumeric(s))
            return Convert.ToDouble(s, nfi);
        return 0;
    }

    public static int GetIntegerOrZero(string s)
    {
        //Number is first converted to double and then to decimal to cover cases when string contains E+
        var nfi = (NumberFormatInfo) CultureInfo.InvariantCulture.NumberFormat.Clone();
        nfi.NumberGroupSeparator = ",";
        nfi.NumberDecimalSeparator = ".";
        return IsNumeric(s) ? Convert.ToInt32(Convert.ToDouble(s, nfi)) : 0;
    }

    public static long GetLongOrZero(string s)
    {
        //Number is first converted to double and then to decimal to cover cases when string contains E+
        var nfi = (NumberFormatInfo) CultureInfo.InvariantCulture.NumberFormat.Clone();
        nfi.NumberGroupSeparator = ",";
        nfi.NumberDecimalSeparator = ".";
        return IsNumeric(s) ? Convert.ToInt64(Convert.ToDouble(s, nfi)) : 0;
    }
}