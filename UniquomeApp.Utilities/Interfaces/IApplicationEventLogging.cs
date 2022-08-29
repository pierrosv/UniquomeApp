

// ReSharper disable MemberCanBePrivate.Global
// ReSharper disable UnusedMember.Global
// ReSharper disable UnusedType.Global

namespace UniquomeApp.Utilities.Interfaces;

public interface IApplicationEventLogging
{
    string LogSomeError(string source, string message, IDictionary<string, string> errors);
}