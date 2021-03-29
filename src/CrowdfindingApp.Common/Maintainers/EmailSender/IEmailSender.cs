using System.Threading.Tasks;

namespace CrowdfindingApp.Common.Maintainers.EmailSender
{
    public interface IEmailSender
    {
        Task SendEmailConfirmationAsync(string email, string token );
        Task SendResetPasswordUrlAsync(string email, string token);
        Task SendResettedPasswordAsync(string email, string password);
    }
}
