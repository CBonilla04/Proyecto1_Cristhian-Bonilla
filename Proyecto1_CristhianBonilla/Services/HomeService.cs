using Azure.Core;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Net.Http;
using Proyecto1_CristhianBonilla.ViewModels;

namespace Proyecto1_CristhianBonilla.Services
{
    public class HomeService : IHomeService
    {
        private readonly HttpClient _httpClient;
        private string _accessToken;


        public HomeService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task AuthenticateAsync(string clientId, string clientSecret)
        {
            var request = new HttpRequestMessage(HttpMethod.Post, "https://test.api.amadeus.com/v1/security/oauth2/token");
            request.Content = new StringContent($"grant_type=client_credentials&client_id={clientId}&client_secret={clientSecret}", System.Text.Encoding.UTF8, "application/x-www-form-urlencoded");

            var response = await _httpClient.SendAsync(request);
            response.EnsureSuccessStatusCode();

            var responseContent = await response.Content.ReadAsStringAsync();
            var tokenResponse = JsonConvert.DeserializeObject<TokenResponse>(responseContent);
            _accessToken = tokenResponse.access_token;
        }

        public async Task<List<OriginOptions>> GetAirportsAsync(string keyword)
        {
            try
            {
                var request = new HttpRequestMessage(HttpMethod.Get, $"https://test.api.amadeus.com/v1/reference-data/locations?subType=AIRPORT&keyword={keyword}");
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", _accessToken);

                var response = await _httpClient.SendAsync(request);
                response.EnsureSuccessStatusCode();

                var responseContent = await response.Content.ReadAsStringAsync();
                var locationResponse = JsonConvert.DeserializeObject<OriginData>(responseContent);

                var locations = locationResponse.Data.Select(option => new OriginOptions
                {
                    IataCode = option.IataCode,
                    Name = option.Name,
                    Address = new Address(
                        option.Address.CityName,
                        option.Address.CityCode,
                        option.Address.CountryName,
                        option.Address.CountryCode,
                        option.Address.RegionCode
                    )
                }).ToList();
                return locations;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }
    }
}
