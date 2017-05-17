using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity;
using System.Configuration;
using System.Net.Mail;

namespace Memberships.Extensions
{
    public static class EmailExtensions
    {
        public static void send(this IdentityMessage message)
        {
            try
            {
                // Read settings
                var password = ConfigurationManager.AppSettings["password"];
                var from = ConfigurationManager.AppSettings["from"];
                var host = ConfigurationManager.AppSettings["host"];
                var port = Int32.Parse(ConfigurationManager.AppSettings["port"]);

                // create the email to send
                var email = new MailMessage(from, message.Destination, message.Subject, message.Body);
                email.IsBodyHtml = true;

                // create the smtpClient that will send the email
                var client = new SmtpClient(host, port);
                client.EnableSsl = true;
                client.Credentials = new System.Net.NetworkCredential(from, password);

                // send email
                client.Send(email);
            }
            catch (Exception ex) { }
        }
    }
}