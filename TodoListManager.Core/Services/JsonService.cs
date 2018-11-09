using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using TodoListManager.Core.Models;

namespace TodoListManager.Core.Services
{
    public class JsonService : IJsonService
    {
        private readonly string _apiBaseUrl;

        public JsonService()
        {
            _apiBaseUrl = Constants.ApiBaseUrl;
        }

        private async Task<string> GetJsonAsync()
        {
            var requestUri = new Uri($"{_apiBaseUrl}");
            using (var client = new HttpClient())
            {
                var response = await client.GetAsync(requestUri);
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();
                return responseBody;
            }
        }

        public async Task<IEnumerable<NewsModel>> GetFeedsAsync()
        {
            var json = await GetJsonAsync();
            var jObject = (JObject)JsonConvert.DeserializeObject<object>(json);

            var items = jObject["articles"]
                .Select(j => new NewsModel
                {
                    Title = j["title"].ToString(),
                    Description = j["content"].ToString(),
                    Link = j["url"].ToString(),
                    PublicationDate = j["publishedAt"].ToString(),                   
                    Picture = j["urlToImage"].ToString()
                }).ToList();
            return items;
        }
    }
}
