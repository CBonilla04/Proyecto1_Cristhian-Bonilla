using Proyecto1_CristhianBonilla.Models;

namespace Proyecto1_CristhianBonilla.Utils
{
    public interface IEmailSender
    {
        Task SendEmailLogin(string subject, Users user);
        Task SendEmailReservation(string name, string email, string amount, string reservationsQuantity, string subject);
    }
}
