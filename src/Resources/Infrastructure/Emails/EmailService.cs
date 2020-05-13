using System.IO;
using System.Security.Authentication;
using System.Threading.Tasks;
using Infrastructure.Configs;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Options;
using MimeKit;

namespace Infrastructure.Emails
{
    public class EmailService : IEmailService
    {
        private readonly EmailConfig emailConfig;

        public EmailService(IOptions<EmailConfig> emailConfig)
        {
            this.emailConfig = emailConfig.Value;
        }

        public Task<bool> SendEmailAsync1(string emailTo, string subject, string body)
        {
            //var message = new MailMessage();

            //message.To.Add(new MailAddress(emailTo));
            //message.Subject = subject;
            //message.Body = body;
            //message.IsBodyHtml = true;

            //using (var client = new SmtpClient())
            //{
            //    client.SendMailAsync(message);
            //}

            return Task.FromResult(true);
        }

        public async Task SendEmailAsync(string nameTo, string emailTo, string subject, BodyBuilder body)
        {
            var email = new MimeMessage();
            email.From.Add(new MailboxAddress(emailConfig.MailServerName, emailConfig.MailServerAddress));

#if DEBUG
            email.To.Add(new MailboxAddress("Email Debug", emailConfig.MailToDebug));
#else
            email.To.Add(new MailboxAddress(nameTo, emailTo));
#endif

            email.Subject = subject;
            email.Body = body.ToMessageBody();


            var port = int.Parse(emailConfig.MailServerPort);

            var password = Utilities.CryptographyHelper.DecryptUsingSymmetricAlgorithm(emailConfig.MailServePassword);
            using (var client = new SmtpClient())
            {
                try
                {
                    //https://stackoverflow.com/questions/59026301/sslhandshakeexception-an-error-occurred-while-attempting-to-establish-an-ssl-or
                    // Allow SSLv3.0 and all versions of TLS

                #if DEBUG
                    client.CheckCertificateRevocation = false;
                #endif

                    client.AuthenticationMechanisms.Remove("XOAUTH2");
                    await client.ConnectAsync(emailConfig.MailServerDomain, port, false).ConfigureAwait(false);
                    await client.AuthenticateAsync(emailConfig.MailServerAddress, password).ConfigureAwait(false);
                    await client.SendAsync(email).ConfigureAwait(false);
                    await client.DisconnectAsync(true).ConfigureAwait(false);
                }
                catch (System.Exception ex)
                {

                    throw ex;
                }
            }
        }
    }
}
