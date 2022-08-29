namespace UniquomeApp.Infrastructure.Security
{
    public class AuthorizationConstants
    {
        //TODO: Move to Application Later
        public static class Roles
        {
            public const string Administrators = "Administrator";
            public const string Secretariat = "Secretariat";
            public const string AcademicDirectors = "AcademicDirector";
            public const string ScientificDirectors = "ScientificDirector";
            public const string CommitteeUser = "CommitteeUser";
            public const string Cooperators = "Cooperator";
            public const string Guest = "Guest";
        }

        public const string DEFAULT_PASSWORD = "P@ssw0rd1";
    }
}