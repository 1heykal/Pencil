using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;
using Pencil.ContentManagement.Application.Contracts.Infrastructure;
using Pencil.ContentManagement.Application.Models.Mail;

namespace Pencil.ContentManagement.Infrastructure.Mail;

public class GmailEmailService : IMailService
{
    private readonly EmailSettings _emailSettings;
    private readonly GmailSettings _gmailSettings;


    public GmailEmailService(IOptions<EmailSettings> emailSettings, IOptions<GmailSettings> gmailSettings)
    {
        _emailSettings = emailSettings.Value ?? throw new ArgumentNullException(nameof(emailSettings));
        _gmailSettings = gmailSettings.Value ?? throw new ArgumentNullException(nameof(gmailSettings));
    }

    public async Task SendAsync(Email email)
    {
        var message = new MimeMessage();
        
        message.From.Add(new MailboxAddress(_emailSettings.FromName, _emailSettings.FromAddress));
        message.To.Add(new MailboxAddress(email.ToName, email.ToAddrees));
        message.Subject = email.Subject;

        var bodyBuilder = new BodyBuilder
        {
            HtmlBody = email.Body
        };

        message.Body = bodyBuilder.ToMessageBody();

        var client = new SmtpClient();
        await client.ConnectAsync(_gmailSettings.Host, _gmailSettings.Port, SecureSocketOptions.StartTls);
        await client.AuthenticateAsync(_gmailSettings.GmailUsername, _gmailSettings.GmailPassword);

        await client.SendAsync(message);
        await client.DisconnectAsync(true);
    }
}