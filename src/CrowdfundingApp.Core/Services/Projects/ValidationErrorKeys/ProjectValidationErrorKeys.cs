
namespace CrowdfundingApp.Core.Services.Projects.ValidationErrorKeys
{
    public static class ProjectValidationErrorKeys
    {
        public static string MissingTitle => $"{nameof(ProjectValidationErrorKeys)}_{nameof(MissingTitle)}";
        public static string MissingCategory => $"{nameof(ProjectValidationErrorKeys)}_{nameof(MissingCategory)}";
        public static string MissingShortDescription => $"{nameof(ProjectValidationErrorKeys)}_{nameof(MissingShortDescription)}";
        public static string ToLargeShortdescriptionLength => $"{nameof(ProjectValidationErrorKeys)}_{nameof(ToLargeShortdescriptionLength)}";
        public static string MissingFullDescription => $"{nameof(ProjectValidationErrorKeys)}_{nameof(MissingFullDescription)}";
        public static string MissingLocation => $"{nameof(ProjectValidationErrorKeys)}_{nameof(MissingLocation)}";
        public static string MissingDuration => $"{nameof(ProjectValidationErrorKeys)}_{nameof(MissingDuration)}";
        public static string MissingBudget => $"{nameof(ProjectValidationErrorKeys)}_{nameof(MissingBudget)}";
        public static string MissingAuthorSurname => $"{nameof(ProjectValidationErrorKeys)}_{nameof(MissingAuthorSurname)}";
        public static string MissingAuthorName => $"{nameof(ProjectValidationErrorKeys)}_{nameof(MissingAuthorName)}";
        public static string MissingAuthorDateOfBirth => $"{nameof(ProjectValidationErrorKeys)}_{nameof(MissingAuthorDateOfBirth)}";
        public static string MissingAuthorMiddleName => $"{nameof(ProjectValidationErrorKeys)}_{nameof(MissingAuthorMiddleName)}";
        public static string MissingAuthorPersonalNumber => $"{nameof(ProjectValidationErrorKeys)}_{nameof(MissingAuthorPersonalNumber)}";
        public static string MissingWhomGivenDocument => $"{nameof(ProjectValidationErrorKeys)}_{nameof(MissingWhomGivenDocument)}";
        public static string MissingWhenGivenDocument => $"{nameof(ProjectValidationErrorKeys)}_{nameof(MissingWhenGivenDocument)}";
        public static string MissingAuthorAddress => $"{nameof(ProjectValidationErrorKeys)}_{nameof(MissingAuthorAddress)}";
        public static string MissingAuthorPhone => $"{nameof(ProjectValidationErrorKeys)}_{nameof(MissingAuthorPhone)}";
        public static string MissingRewards => $"{nameof(ProjectValidationErrorKeys)}_{nameof(MissingRewards)}";
        public static string MissingQuestions => $"{nameof(ProjectValidationErrorKeys)}_{nameof(MissingQuestions)}";
        public static string InvalidPassportNo => $"{nameof(ProjectValidationErrorKeys)}_{nameof(InvalidPassportNo)}";
        public static string InvalidIdentificationPassportNo => $"{nameof(ProjectValidationErrorKeys)}_{nameof(InvalidIdentificationPassportNo)}";
        public static string MissingAuthorIdentificationNumber => $"{nameof(ProjectValidationErrorKeys)}_{nameof(MissingAuthorIdentificationNumber)}";
        public static string DurationLessThanZero => $"{nameof(ProjectValidationErrorKeys)}_{nameof(DurationLessThanZero)}";
        public static string InvalidPhoneNumber => $"{nameof(ProjectValidationErrorKeys)}_{nameof(InvalidPhoneNumber)}";
        public static string WrongAuthorDateOfBirth => $"{nameof(ProjectValidationErrorKeys)}_{nameof(WrongAuthorDateOfBirth)}";
        public static string WrongDocumentIssuedDate => $"{nameof(ProjectValidationErrorKeys)}_{nameof(WrongDocumentIssuedDate)}";
        public static string MissingProject => $"{nameof(ProjectValidationErrorKeys)}_{nameof(MissingProject)}";
    }
}
