using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MailKit.Net.Smtp;
using MimeKit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Primitives;

namespace Titan.MailKit.DataAccess
{
    public class SMTPAccess : ISMTPAccess
    {
        private readonly IConfiguration _config;

        public SMTPAccess(IConfiguration config)
        {
            _config = config;
        }

        public void SendEmail(string To)
        {
            // Create a new MimeMessage
            var message = new MimeMessage();

            // Set the sender and recipient addresses
            message.From.Add(new MailboxAddress("Sender Name", "sender@example.com"));
            message.To.Add(new MailboxAddress("Recipient Name", To));

            // Set the subject and body of the email
            message.Subject = "Hello from MailKit";
            message.Body = new TextPart("plain")
            {
                Text = "This is the body of the email."
            };

            // Create a new SmtpClient and configure it with your email server settings
            using (var client = new SmtpClient())
            {
                var smtpSettings = _config.GetSection("SmtpSettings");

                string server = smtpSettings["Server"];
                int port = int.Parse(smtpSettings["Port"]);
                bool useSsl = bool.Parse(smtpSettings["UseSsl"]);
                string username = smtpSettings["Username"];
                string password = smtpSettings["Password"];

                client.Connect(server, port, useSsl);

                // If authentication is required, uncomment the following line
                // client.Authenticate(username, password);

                // Send the email
                client.Send(message);
                client.Disconnect(true);
            }
        }
    }
}