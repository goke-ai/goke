using System.Net.Http.Json;
using static System.Net.WebRequestMethods;

namespace GokeWebApp.Client.Services
{
    public class ClientDataProcessingService(HttpClient http) : IDataProcessingService
    {
        private readonly HttpClient http = http;

        public async Task<string> ProcessData(string endpoint, FormModel model)
        {
            //var client = ClientFactory.CreateClient("Auth");

            var response = await http.PostAsJsonAsync<FormModel>(endpoint, model);

            if (response.IsSuccessStatusCode)
            {
                return $"The data was processed by the server! The server indicates that the message is {await response.Content.ReadAsStringAsync()} long.";
            }
            else
            {
                return $"The server responded with an unsuccessful status code ({response.StatusCode}).";
            }
        }

       
    }
}
