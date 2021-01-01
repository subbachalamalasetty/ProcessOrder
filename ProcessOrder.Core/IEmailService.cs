using System.Net.Mail;

namespace ProcessOrder.Core
{
    public interface IEmailService
    {
        void SendEmail(MailMessage mailMessage);
    }
}