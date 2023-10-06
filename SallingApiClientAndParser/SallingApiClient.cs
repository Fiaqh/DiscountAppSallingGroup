namespace SallingApiClientAndParser
{
    public class SallingApiClient
    {
        private readonly HttpClient httpClient;

        //apiUrl and accesToken for Salling group API. This should technically be hidden in some file...
        private string apiUrl = "https://api.sallinggroup.com/v1/food-waste/?zip=";
        private string accessToken = "c1a9dcbb-ef9e-4e4c-acf9-07631d147dd8";

        public string zipCode;

        public SallingApiClient()
        {
            httpClient = new HttpClient();
        }

        public async Task<string> GetApiResponseAsync(string zipCode)
        {
            try
            {

                // Set the Bearer token
                httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", accessToken);

                // Make the GET request avoiding any deadlock
                var response = await httpClient.GetAsync(apiUrl + zipCode).ConfigureAwait(false);

                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadAsStringAsync();
                }
                else
                {
                    Console.WriteLine($"Error: {response.StatusCode} - {response.ReasonPhrase}");
                    // Handle the error (e.g., log or throw an exception)
                    return null;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                // Handle the exception
                return null;
            }
        }

    }
}
