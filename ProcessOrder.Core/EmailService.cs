using System.Configuration;
using System.Net.Mail;
using System.Threading;

namespace ProcessOrder.Core
{
    public class EmailService : IEmailService
    {
        public void SendEmail(MailMessage mailMessage)
        {
            try
            {
                // Should use this 'smtpClientUrl' from config file - var smptpClient = new smtpClient(smtpClientUrl) {.....}
                var smtpClientUrl = ConfigurationManager.AppSettings.Get("SmtpUrl");

                var smtpClient = new SmtpClient()
                {
                    Port = 25,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false
                };
                
                smtpClient.SendAsync(mailMessage, CancellationToken.None);
            }
            catch (SmtpException ex)
            {
                
            }
        }
    }
}