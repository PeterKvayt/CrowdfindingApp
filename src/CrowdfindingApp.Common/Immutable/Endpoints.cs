
namespace CrowdfindingApp.Common.Immutable
{
    public static class Endpoints
    {
        public static class User
        {
            public const string ForgotPassword = "forgot-password";
            public const string Token = "token";
            public const string Register = "register";
            public const string ResetPassword = "reset-password";
            public const string UserInfo = "user-info";
            public const string UpdateUser = "user-info";
            public const string ChangePassword = "change-password";
            public const string EmailConfirmation = "confirm-email";
        }

        public static class Project
        {
            public const string SaveDraft = "save-draft";
            public const string Search = "search";
            public const string Countries = "countries";
            public const string Cities = "cities";
            public const string Categories = nameof(Categories);
            public const string Moderate = nameof(Moderate);

        }
    }
}
