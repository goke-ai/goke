using Goke.Web.Shared.Models.Markets;
using Goke.Web.UI.Identity.Models;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;

namespace Goke.Web.UI.Services
{
    public class BuyPinService
    {
        private readonly IHttpClientFactory httpClientFactory;
        private readonly HttpClient httpClient;

        public BuyPinService(IHttpClientFactory httpClientFactory)
        {
            this.httpClientFactory = httpClientFactory;
            httpClient = httpClientFactory.CreateClient("Auth");
        }

        public async Task<FormResult> BuyAsync(string email, OrderItem order)
        {
            //return new FormResult { Succeeded = true };

            string[] defaultDetail = ["An unknown error prevented buying from succeeding."];

            try
            {
                // make the request
                var result = await httpClient.PostAsJsonAsync(
                    "api/buypin", new
                    {
                        email,
                        order
                    });

                // successful?
                if (result.IsSuccessStatusCode)
                {
                    return new FormResult { Succeeded = true };
                }

                // body should contain details about why it failed
                var details = await result.Content.ReadAsStringAsync();
                var problemDetails = JsonDocument.Parse(details);
                var errors = new List<string>();
                var errorList = problemDetails.RootElement.GetProperty("errors");

                foreach (var errorEntry in errorList.EnumerateObject())
                {
                    if (errorEntry.Value.ValueKind == JsonValueKind.String)
                    {
                        errors.Add(errorEntry.Value.GetString()!);
                    }
                    else if (errorEntry.Value.ValueKind == JsonValueKind.Array)
                    {
                        errors.AddRange(
                            errorEntry.Value.EnumerateArray().Select(
                                e => e.GetString() ?? string.Empty)
                            .Where(e => !string.IsNullOrEmpty(e)));
                    }
                }

                // return the error list
                return new FormResult
                {
                    Succeeded = false,
                    ErrorList = problemDetails == null ? defaultDetail : [.. errors]
                };
            }
            catch { }

            // unknown error
            return new FormResult
            {
                Succeeded = false,
                ErrorList = defaultDetail
            };
        }

    }
}
