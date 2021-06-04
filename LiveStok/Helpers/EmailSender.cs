using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Options;
using SendGrid;
using SendGrid.Helpers.Mail;
using System.Threading.Tasks;

namespace LiveStok.Helpers
{
    public class EmailSender : IEmailSender
    {

        //Hay que añadir estas dos lineas en ConfiguracionServices antes del .MVC
        //services.AddTransient<IEmailSender, EmailSender>();
       
        const string SENDGRID_KEY = "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";

        public Task SendEmailAsync(string email, string subject, string message)
        {
            return Execute(SENDGRID_KEY, subject, message, email);
        }

        //public Task Execute(string apiKey, string subject, string message, string email)
        public Task Execute(string apiKey, string subject, string message, string email)
        {
            var client = new SendGridClient(apiKey);
            var msg = new SendGridMessage()
            {
                From = new EmailAddress("NoReply@LiveStock.com", "NoReply"),
                Subject = subject,
                PlainTextContent = message,
                HtmlContent = message
            };
            msg.AddTo(new EmailAddress(email));

            // Disable click tracking.
            // See https://sendgrid.com/docs/User_Guide/Settings/tracking.html
            msg.SetClickTracking(false, false);

            
            return client.SendEmailAsync(msg); ;
        }
    }
}