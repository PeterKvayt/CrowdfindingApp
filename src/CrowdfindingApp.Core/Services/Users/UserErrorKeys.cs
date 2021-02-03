
namespace CrowdfindingApp.Core.Services.Users
{
    public class UserErrorKeys
    {
        public const string UniqueEmail = nameof(UniqueEmail);
        public const string InvalidPasswordLength = nameof(InvalidPasswordLength);
        public const string InvalidToken = nameof(InvalidToken);
        public const string PasswordConfirmationFail = nameof(PasswordConfirmationFail);
        public const string InvalidUserId = nameof(InvalidUserId);
        public const string UserIdIsNullOrEmpty = nameof(UserIdIsNullOrEmpty);
        public const string EmptyEmail = nameof(EmptyEmail);
        public const string EmptyPassword = nameof(EmptyPassword);
        public const string EmptyName = nameof(EmptyName);
        public const string EmptySurname = nameof(EmptySurname);
    }
}
