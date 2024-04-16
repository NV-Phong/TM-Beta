using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Task_Manager_Beta;

public class EmailService
{
    private readonly SmtpSettings _smtpSettings;

    public EmailService(IOptions<SmtpSettings> smtpSettings)
    {
        _smtpSettings = smtpSettings.Value;
    }

    public async Task SendInvitationEmailAsync(string recipientEmail, string subject, string body)
    {
        using var client = new SmtpClient(_smtpSettings.Host, _smtpSettings.Port)
        {
            Credentials = new NetworkCredential(_smtpSettings.Username, _smtpSettings.Password),
            EnableSsl = _smtpSettings.UseSSL
        };

        var mailMessage = new MailMessage
        {
            From = new MailAddress(_smtpSettings.FromEmail),
            Subject = subject,
            Body = body,
            IsBodyHtml = true
        };

        mailMessage.To.Add(recipientEmail);

        await client.SendMailAsync(mailMessage);
    }
}
