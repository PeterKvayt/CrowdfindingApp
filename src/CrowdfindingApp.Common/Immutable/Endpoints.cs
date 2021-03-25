
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
            public const string EditRole = nameof(EditRole);
        }

        public static class Project
        {
            public const string SetStatus = "set-status";
            public const string ProjectView = nameof(ProjectView);
            public const string OwnerProjects = nameof(OwnerProjects);
            public const string OpenedProjects = nameof(OpenedProjects);
            public const string Search = nameof(Search);
            public const string SaveDraft = "save-draft";
            //public const string Cards = nameof(Cards);
            public const string Countries = "countries";
            public const string Cities = "cities";
            public const string Categories = nameof(Categories);
            public const string Moderate = nameof(Moderate);
            public const string MySupportedProjects = "mySupportedProjects";

        }

        public static class Order
        {
            public const string Search = nameof(Search);

        }

        public static class Reward
        {
            public const string GetByProjectId = nameof(GetByProjectId);

        }

        public static class Files
        {
            public const string SaveImage = "save-image";

        }
    }
}
