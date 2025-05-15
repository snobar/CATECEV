using CATECEV.CORE.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Net.Http.Headers;

namespace CATECEV.CORE.Wrapper
{
    public class ApiService
    {
        private readonly HttpClient _httpClient;

        public ApiService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<T>> GetAllPaginatedDataAsync<T>(string initialUrl, string token)
        {
            var allData = new List<T>();
            string currentUrl = initialUrl;

            // Set the bearer token once
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            while (!string.IsNullOrEmpty(currentUrl))
            {
                var response = await _httpClient.GetAsync(currentUrl);
                if (!response.IsSuccessStatusCode)
                {
                    throw new Exception($"API request failed: {response.StatusCode} - {await response.Content.ReadAsStringAsync()}");
                }

                var content = await response.Content.ReadAsStringAsync();
                var page = JsonConvert.DeserializeObject<PaginatedResponse<T>>(content);

                if (page?.Data != null)
                    allData.AddRange(page.Data);

                currentUrl = page?.Links?.Next;
            }

            return allData;
        }
    }



}
