namespace UniquomeApp.Infrastructure.Security;

public class AuthorizationConstants
{
    //TODO: Move to Application Later
    public static class Roles
    {
        public const string Administrators = "Administrator";
        public const string ApplicationUser = "ApplicationUser";
        public const string Guest = "Guest";
    }

    public const string DEFAULT_PASSWORD = "P@ssw0rd1";
}