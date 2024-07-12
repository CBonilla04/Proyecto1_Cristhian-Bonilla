using Proyecto1_CristhianBonilla.ViewModels;

namespace Proyecto1_CristhianBonilla.Services
{
    public interface IHomeService
    {
        Task AuthenticateAsync(string clientId, string clientSecret);
        Task<List<OriginOptions>> GetAirportsAsync(string keyword);
    }
}
