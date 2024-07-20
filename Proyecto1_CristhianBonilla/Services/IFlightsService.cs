using Proyecto1_CristhianBonilla.Models;

namespace Proyecto1_CristhianBonilla.Services
{
    public interface IFlightsService
    {
        Task<Flights> AddFlight(Flights flight);
        Task<List<Flights>> AddFlights(List<Flights> flights);
    }
}
