using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Azure.Core;
using Newtonsoft.Json;
using Proyecto1_CristhianBonilla.ViewModels;

namespace Proyecto1_CristhianBonilla.Services
{
    public class AmadeusApiService : IAmadeusApiService
    {
        private readonly HttpClient _httpClient;
        private string _accessToken;

        public AmadeusApiService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        //Clase para deserializar la respuesta de la api de amadeus
        public async Task AuthenticateAsync(string clientId, string clientSecret)
        {
            try
            {           
                var request = new HttpRequestMessage(HttpMethod.Post, "https://test.api.amadeus.com/v1/security/oauth2/token");
                request.Content = new StringContent($"grant_type=client_credentials&client_id={clientId}&client_secret={clientSecret}", System.Text.Encoding.UTF8, "application/x-www-form-urlencoded");

                var response = await _httpClient.SendAsync(request);
                response.EnsureSuccessStatusCode();

                var responseContent = await response.Content.ReadAsStringAsync();
                var tokenResponse = JsonConvert.DeserializeObject<TokenResponse>(responseContent);
                _accessToken = tokenResponse.access_token;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        //Obtiene la lista de vuelos
        public async Task<FlightOffersResponse> GetFlightOffersAsync(string origin, string destination, string departureDate, string returnDate, string adults, string children, string infants, string travelClass, string notStop, string maxPrice)
        {
            var queryParameters = new List<string>();
            //agrega los parámetros a la url
            if (!string.IsNullOrEmpty(origin))
            {
                queryParameters.Add($"originLocationCode={origin}");
            }

            if (!string.IsNullOrEmpty(destination))
            {
                queryParameters.Add($"destinationLocationCode={destination}");
            }

            if (!string.IsNullOrEmpty(departureDate))
            {
                queryParameters.Add($"departureDate={departureDate}");
            }

            if (!string.IsNullOrEmpty(returnDate))
            {
                queryParameters.Add($"returnDate={returnDate}");
            }

            if (!string.IsNullOrEmpty(adults))
            {
                queryParameters.Add($"adults={adults}");
            }

            if (!string.IsNullOrEmpty(children))
            {
                queryParameters.Add($"children={children}");
            }

            if (!string.IsNullOrEmpty(infants))
            {
                queryParameters.Add($"infants={infants}");
            }

            if (!string.IsNullOrEmpty(travelClass))
            {
                queryParameters.Add($"travelClass={travelClass}");
            }
            if (!string.IsNullOrEmpty(notStop))
            {
                queryParameters.Add($"nonStop={notStop}");
            }
            if (!string.IsNullOrEmpty(maxPrice))
            {
                queryParameters.Add($"maxPrice={maxPrice}");
            }

            var queryString = string.Join("&", queryParameters);
            try
            {
                //realiza la petición a la api de amadeus
                var request = new HttpRequestMessage(HttpMethod.Get, $"https://test.api.amadeus.com/v2/shopping/flight-offers?{queryString}&currencyCode=USD&max=50");

                //agrega el token de autenticación
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", _accessToken);

                //envía la petición
                var response = await _httpClient.SendAsync(request);
                response.EnsureSuccessStatusCode();
                //obtiene la respuesta
                var responseContent = await response.Content.ReadAsStringAsync();
                //deserializa la respuesta
                var data = JsonConvert.DeserializeObject<FlightOffersResponse>(responseContent);

                return data;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }
    }

    
}
