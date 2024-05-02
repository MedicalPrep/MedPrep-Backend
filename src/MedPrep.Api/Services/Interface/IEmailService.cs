namespace MedPrep.Api.Services.Interface;

public interface IEmailService
{
    Task SendEmailAsync(string email, string subject, string message, bool isHtml = true);
}
