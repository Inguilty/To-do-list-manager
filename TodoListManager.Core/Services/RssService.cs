using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using TodoListManager.Core.Models;

namespace TodoListManager.Core.Services
{
    public class RssService : IRssService
    {
        private const string ApiBaseUrl = "https://newsapi.org/v2/top-headlines?sources=the-washington-post&apiKey=0a568ceab8f74d8c8e016ecf994daf79";

        public RssService()
        {
        }

        private async Task<string> GetRssJsonAsync()
        {
            var requestUri = new Uri($"{ApiBaseUrl}");
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
            var json = await GetRssJsonAsync();
            var jObject = (JObject)JsonConvert.DeserializeObject<object>(json);

            var items = jObject["articles"]
                .Select(j => new NewsModel
                {
                    Title = j["title"].ToString(),
                    Description = j["content"].ToString(),
                    Link = j["url"].ToString(),
                    PublicationDate = j["publishedAt"].ToString(),                   
                    Picture = j["urlToImage"].ToString()
                })
                .ToList();
            return items;
        }
    }
}
