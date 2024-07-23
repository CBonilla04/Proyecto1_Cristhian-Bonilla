using Proyecto1_CristhianBonilla.Models;
using Proyecto1_CristhianBonilla.Utils;

namespace Proyecto1_CristhianBonilla.Services
{
    public class FligthsService : IFlightsService
    {

        private readonly AppDbContext _flightContext;

        public FligthsService(AppDbContext flightContext)
        {
            _flightContext = flightContext;
        }
        // Method to add a new flight
        public async Task<Flights> AddFlight(Flights flight)
        {
            try
            {
                await _flightContext.Flights.AddAsync(flight);
                await _flightContext.SaveChangesAsync();

                return flight;
            }
            catch (Exception ex)
            {

                return null;
            }
        }

        // Method to add a list of new flights
        public async Task<List<Flights>> AddFlights(List<Flights> flights)
        {
            try
            {
                await _flightContext.Flights.AddRangeAsync(flights);
                await _flightContext.SaveChangesAsync();
                return flights;
            }
            catch (Exception ex)
            {

                return null;
            }
        }
    }
}
