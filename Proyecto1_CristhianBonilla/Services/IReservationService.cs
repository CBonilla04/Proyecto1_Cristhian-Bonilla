using Proyecto1_CristhianBonilla.Models;
using Proyecto1_CristhianBonilla.ViewModels;

namespace Proyecto1_CristhianBonilla.Services
{
    public interface IReservationService
    {
        Task<Users> AddReservations(List<FlightOffer> flights, int userId);
        Task<List<Reservations>> GetReservationByUser(int id);
        Task<Reservations> insert(Reservations reservation);
    }
}
