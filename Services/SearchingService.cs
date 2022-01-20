using Entities;
using Entities.Converters;
using IServices;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;


namespace Services
{
    public class SearchingService : ISearchingService
    {
        private readonly IHttpClientFactory _clientFactory;

        public SearchingService(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }
        public async Task<Searching> GetSearching(string query)
        {
            var client = _clientFactory.CreateClient();
            try
            {
                HttpResponseMessage response = await client.GetAsync($"https://api.mercadolibre.com/sites/MLA/search?q={query}");
                string contentJson = await response.Content.ReadAsStringAsync();
                Searching searchingResult = JsonConvert.DeserializeObject<Searching>(contentJson, new ResultConverter());
                return searchingResult;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

      
    }
}
