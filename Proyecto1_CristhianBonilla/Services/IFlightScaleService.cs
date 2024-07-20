using Proyecto1_CristhianBonilla.Models;

namespace Proyecto1_CristhianBonilla.Services
{
    public interface IFlightScaleService
    {

        Task<FlightScales> insert(FlightScales flightScale);
    }
}
