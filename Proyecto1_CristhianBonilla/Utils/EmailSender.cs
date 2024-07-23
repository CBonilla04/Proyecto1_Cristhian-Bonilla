
using MailKit.Net.Smtp;
using MimeKit;
using Proyecto1_CristhianBonilla.Models;
using System.Reflection.Metadata;

namespace Proyecto1_CristhianBonilla.Utils
{
    public class EmailSender : IEmailSender
    {
        private readonly EmailConfiguration _emailConfiguration;
        private readonly IWebHostEnvironment _env;

        public EmailSender(EmailConfiguration emailConfiguration, IWebHostEnvironment env)
        {
            _emailConfiguration = emailConfiguration;
            _env = env;
        }


        //Envia correo de login
        public Task SendEmailLogin(string subject, Users user)
        {   
            //url para ir a buscar el template
            var templateUrl = Path.Combine(_env.ContentRootPath, "Utils", "EmailTemplates", "LogInTemplate.html");
            var emailToSend = new MimeMessage();

            //agrega información para envio
            emailToSend.From.Add(MailboxAddress.Parse(_emailConfiguration.From));
            emailToSend.To.Add(MailboxAddress.Parse(user.Email));
            emailToSend.Subject = subject;

            var templateContent = File.ReadAllText(templateUrl);
            var emailBody = templateContent
                .Replace("{{Name}}", user.Name)
                .Replace("{{RegistrationDate}}", DateTime.Now.ToString("MMMM dd, yyyy"));

            //convierte el template
            emailToSend.Body = new TextPart(MimeKit.Text.TextFormat.Html) { Text = emailBody };

            //envia el correo
            using (var smtp = new SmtpClient())
            {
                smtp.Connect(_emailConfiguration.SmtpServer, _emailConfiguration.Port, MailKit.Security.SecureSocketOptions.StartTls);
                smtp.Authenticate(_emailConfiguration.Username, _emailConfiguration.Password);
                smtp.Send(emailToSend);
                smtp.Disconnect(true);
            }
            return Task.CompletedTask;
        }

        //Envia correo de reserva
        public Task SendEmailReservation(string name, string email, string amount, string reservationsQuantity, string subject)
        {
            var templateUrl = Path.Combine(_env.ContentRootPath, "Utils", "EmailTemplates", "ReservationsTemplate.html");
            var emailToSend = new MimeMessage();
            //agrega información para envio
            emailToSend.From.Add(MailboxAddress.Parse(_emailConfiguration.From));
            emailToSend.To.Add(MailboxAddress.Parse(email));
            emailToSend.Subject = subject;
            //carga el template
            var templateContent = File.ReadAllText(templateUrl);
            //reemplaza los valores del template
            var emailBody = templateContent
                .Replace("{{Name}}", name)
                .Replace("{{reservationsQuantity}}", reservationsQuantity)
                .Replace("{{totalPrice}}", amount)
                .Replace("{{reservationDate}}", DateTime.Now.ToString("MMMM dd, yyyy"));
            //convierte el template
            emailToSend.Body = new TextPart(MimeKit.Text.TextFormat.Html) { Text = emailBody };
            //envia el correo
            using (var smtp = new SmtpClient())
            {
                smtp.Connect(_emailConfiguration.SmtpServer, _emailConfiguration.Port, MailKit.Security.SecureSocketOptions.StartTls);
                smtp.Authenticate(_emailConfiguration.Username, _emailConfiguration.Password);
                smtp.Send(emailToSend);
                smtp.Disconnect(true);
            }
            return Task.CompletedTask;
        }
    }
}
