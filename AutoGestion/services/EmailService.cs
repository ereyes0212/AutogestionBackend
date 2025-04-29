using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using AutoGestion.interfaces.IEmailService;

public class SmtpEmailService : IEmailService
{
    private readonly SmtpClient _smtpClient;
    private readonly string _from;

    public SmtpEmailService(string host, int port, string username, string password, string from)
    {
        _from = from;
        _smtpClient = new SmtpClient(host, port)
        {
            Credentials = new NetworkCredential(username, password),
            EnableSsl = true
        };
    }

    public async Task SendEmailAsync(string to, string subject, string body)
    {
        var mailMessage = new MailMessage(_from, to, subject, body)
        {
            IsBodyHtml = true
        };

        await _smtpClient.SendMailAsync(mailMessage);
    }
}
