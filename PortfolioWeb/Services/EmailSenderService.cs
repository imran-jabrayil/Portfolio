using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Options;
using MimeKit;
using MimeKit.Text;
using PortfolioWeb.Services.Settings;

namespace PortfolioWeb.Services;

public class EmailSenderService : IEmailSender {
    private readonly EmailSenderServiceSettings _settings;
    private readonly ILogger<EmailSenderService> _logger;

    public EmailSenderService(IOptions<EmailSenderServiceSettings> settings, ILogger<EmailSenderService> logger) {
        _settings = settings.Value;
        
        _logger = logger;
        _logger.LogInformation("{ServiceName} configured. Settings: {@Object}", 
            nameof(EmailSenderService), 
            new { _settings.Host, _settings.Port, _settings.NoReplyEmail });
    }

    public async Task SendEmailAsync(string email, string subject, string htmlMessage) {
        var message = new MimeMessage();
        message.From.Add(new MailboxAddress("No-Reply", _settings.NoReplyEmail));
        message.To.Add(new MailboxAddress(email, email));
        message.Subject = subject;

        message.Body = new TextPart(TextFormat.Html) {
            Text = htmlMessage
        };

        using var client = new SmtpClient();
        await client.ConnectAsync(_settings.Host, _settings.Port, SecureSocketOptions.StartTls);
        
        await client.AuthenticateAsync(_settings.Email, _settings.Password);

        await client.SendAsync(message);
        await client.DisconnectAsync(true);
        _logger.LogInformation("Sent registration confirmation email to {Email}", email);
    }
}
