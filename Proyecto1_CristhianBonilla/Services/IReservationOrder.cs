using Proyecto1_CristhianBonilla.ViewModels;

namespace Proyecto1_CristhianBonilla.Services
{
    public interface IReservationOrder
    {
        Task<ReservationService> IntegrateReservation(List<FlightOffer> flights, int userId);
    }
}
