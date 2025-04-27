using AiVectorSearch.Application.Interfaces;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Text;

namespace AiVectorSearch.Infrastructure.Services
{
    public class HuggingFaceService : IHuggingFaceService
    {
        private readonly string? _apiKey;
        private readonly string? _apiUrl;
        private readonly HttpClient _httpClient;

        public HuggingFaceService(IConfiguration configuration)
        {
            _apiUrl = configuration["HuggingFace:ApiUrl"];
            _apiKey = configuration["HuggingFace:ApiKey"];
            _httpClient = new HttpClient();

            if (_httpClient.DefaultRequestHeaders.Authorization == null)
            {
                _httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {_apiKey}");
            }
        }

        public async Task<List<float>> GetEmbeddingAsync(string text)
        {
            var requestBody = new
            {
                inputs = text
            };

            var jsonRequestBody = JsonConvert.SerializeObject(requestBody);
            var content = new StringContent(jsonRequestBody, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync(_apiUrl, content);

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"API Error: {response.ReasonPhrase}");
            }

            var responseContent = await response.Content.ReadAsStringAsync();
            var embedding = JsonConvert.DeserializeObject<List<float>>(responseContent);
            return embedding;

        }
    }
}
