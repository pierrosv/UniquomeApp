namespace UniquomeApp.WebApi;

public static class ApiRoutesV1
{
    public const string Root = "api";
    public const string ApiVersion = "v1";
    public const string Base = Root + "/" + ApiVersion;

    public static class Initialization
    {
        private const string Local = "init";
        public const string CreateInitialData = Base + "/" + Local + "/initial-data";
        public const string InitializeSecurity = Base + "/" + Local + "/initial-security";
        public const string CreateTimeIntervals = Base + "/" + Local + "/create-time-intervals";
    }

    public static class System
    {
        private const string Local = "system";
        public const string ReleaseVersion = Base + "/" + Local + "/version";
        public const string SeedIdentity = Base + "/" + Local + "/seed-identity";
        public const string ChangeHangfireJob = Base + "/" + Local + "/change-hg-job";
    }
    public static class Identity
    {
        private const string Local = "identity";
        public const string Login = Base + "/" + Local + "/login";
        public const string RefreshToken = Base + "/" + Local + "/refresh-token";
        public const string RevokeToken = Base + "/" + Local + "/revoke-token";
        public const string VerifyEmail = Base + "/" + Local + "/verify-email";
        public const string ForgotPassword = Base + "/" + Local + "/forgot-password/{email}";
        public const string ChangeLostPassword = Base + "/" + Local + "/change-lost-password";
        public const string ChangePassword = Base + "/" + Local + "/change-password";
        public const string ValidateResetToken = Base + "/" + Local + "/validate-reset-token";
        public const string RegisterAdmin = Base + "/" + Local + "/register-admin";
        public const string RegisterAppUser = Base + "/" + Local + "/register-app-user";
    }

    public static class Aux
    {
        private const string Local = "aux";
        public const string ImportAxsStuff = Base + "/" + Local + "/import-axs-stuff";
        public const string ImportAxsPorts = Base + "/" + Local + "/import-axs-ports";
    }

    public static class Country
    {
        private const string Local = "country";
        public const string GetAll = Base + "/" + Local;
        public const string Create = Base + "/" + Local;
        public const string Get = Base + "/" + Local + "/{id}";
        public const string Update = Base + "/" + Local;
        public const string Delete = Base + "/" + Local + "/{id}";
        public const string Merge = Base + "/" + Local + "/merge";
    }
}