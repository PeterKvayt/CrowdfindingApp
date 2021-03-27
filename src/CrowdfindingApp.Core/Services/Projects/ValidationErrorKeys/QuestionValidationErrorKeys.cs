
namespace CrowdfindingApp.Core.Services.Projects.ValidationErrorKeys
{
    public static class QuestionValidationErrorKeys
    {
        public static string MissingAnswer => $"{nameof(QuestionValidationErrorKeys)}_{nameof(MissingAnswer)}";
        public static string MissingQuestion => $"{nameof(QuestionValidationErrorKeys)}_{nameof(MissingQuestion)}";
    }
}
