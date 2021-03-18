using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrowdfindingApp.Core.Services.Projects.ValidationErrorKeys
{
    public static class QuestionValidationErrorKeys
    {
        public static string MissingAnswer => $"{nameof(QuestionValidationErrorKeys)}_{nameof(MissingAnswer)}";
        public static string MissingQuestion => $"{nameof(QuestionValidationErrorKeys)}_{nameof(MissingQuestion)}";
    }
}
