using Proyecto1_CristhianBonilla.Models;
using Proyecto1_CristhianBonilla.Utils;

namespace Proyecto1_CristhianBonilla.Services
{
    public class FlightScaleService : IFlightScaleService
    {

        private readonly AppDbContext _flyScale;

        public FlightScaleService(AppDbContext flyScale)
        {
            _flyScale = flyScale;
        }

        // Method to insert a new flight scale
        public async Task<FlightScales> insert(FlightScales flightScale)
        {
            try
                {
                await _flyScale.FlightScales.AddAsync(flightScale);
                await _flyScale.SaveChangesAsync();

                return flightScale;
                
            }
            catch (Exception e)
            {
                return null;
            }
        }
    }
}
