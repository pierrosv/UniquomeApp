

// ReSharper disable MemberCanBePrivate.Global
// ReSharper disable UnusedMember.Global
// ReSharper disable UnusedType.Global

namespace UniquomeApp.Utilities;

public static class ExceptionUtilities
{
    public static string GetExceptionMessage(Exception exception, bool getInnerException = true)
    {
        var message = exception.Message;
        if (getInnerException && exception.InnerException != null)
            message += $"{Environment.NewLine}Inner Exception : {Environment.NewLine}{exception.InnerException.Message}";
        return message;
    }
}