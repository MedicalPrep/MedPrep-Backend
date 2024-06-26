namespace MedPrep.Api.Services;

using System.Net;
using System.Net.Mail;
using MedPrep.Api.Config;
using MedPrep.Api.Services.Common;
using Microsoft.Extensions.Options;

public class EmailService(IOptions<EmailConfig> emailConfig) : IEmailService
{
    private readonly EmailConfig emailConfig = emailConfig.Value;

    public Task SendEmailAsync(string email, string subject, string message, bool isHtml = true)
    {
        var client = new SmtpClient(this.emailConfig.Host, this.emailConfig.Port)
        {
            Credentials = new NetworkCredential(
                this.emailConfig.Username,
                this.emailConfig.Password
            ),
            UseDefaultCredentials = false,
            DeliveryMethod = SmtpDeliveryMethod.Network,
            EnableSsl = true,
        };

        var mailMessage = new MailMessage(this.emailConfig.From, email, subject, message)
        {
            IsBodyHtml = isHtml
        };
        return client.SendMailAsync(mailMessage);
    }
}
