using Proyecto1_CristhianBonilla.ViewModels;

namespace Proyecto1_CristhianBonilla.Services
{
    public interface IAmadeusApiService
        {
            Task AuthenticateAsync(string clientId, string clientSecret);
        Task<FlightOffersResponse> GetFlightOffersAsync(string origin, string destination, string departureDate, string returnDate, string adults, string children, string infants, string travelClass);
        }
}
