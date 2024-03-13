using MailKit.Net.Smtp;
using Microsoft.Extensions.Configuration;
using MimeKit;
using MimeKit.Text;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Consumer.API.Helper
{
    public class EMailHelper : IEmailHelper
    {
        private readonly string _host;
        private readonly int _port;
        private readonly string _password;

        private readonly string _from;
        private string _recipients;

        private readonly SmtpClient _smtpClient;

        public EMailHelper(IConfiguration configuration)
        {
            _host = configuration["Smtp:Host"];
            _port = int.Parse(configuration["Smtp:Port"]);
            _password = configuration["Smtp:Password"];

            _from = configuration["Smtp:From"];
            _recipients = configuration["Smtp:Recipients"];

            _smtpClient = new SmtpClient();
        }

        public async Task SendMailAsync(string subject, string body, string? recipients = null, Stream? stream = null, string? ccRecipients = null)
        {
            try
            {
                await _smtpClient.ConnectAsync(host: _host, port: _port, useSsl: false);

                await _smtpClient.AuthenticateAsync(_from, _password);

                if (string.IsNullOrEmpty(recipients))
                {
                    recipients = _recipients;
                }

                var multipart = new Multipart();

                if (!string.IsNullOrEmpty(body))
                {
                    multipart.Add(new TextPart(TextFormat.Plain) { Text = body });
                }

                if (stream != null)
                {
                    var attachment = new MimePart("application/octet-stream")
                    {
                        Content = new MimeContent(stream),
                        ContentDisposition = new ContentDisposition(ContentDisposition.Attachment),
                        ContentTransferEncoding = ContentEncoding.Base64,
                        FileName = $"rapor_{DateTime.Today:d}.xlsx"
                    };

                    multipart.Add(attachment);
                }

                var email = new MimeMessage();

                email.From.Add(MailboxAddress.Parse(_from));

                email.To.AddRange(InternetAddressList.Parse(recipients));

                if (!string.IsNullOrEmpty(ccRecipients))
                {
                    email.Cc.AddRange(InternetAddressList.Parse(ccRecipients));
                }

                email.Subject = subject;
                email.Body = multipart;

                await _smtpClient.SendAsync(email);
            }
            catch (Exception)
            {
            }
            finally
            {
                if (_smtpClient.IsConnected)
                {
                    await _smtpClient.DisconnectAsync(true);
                }
            }
        }
    }
}
