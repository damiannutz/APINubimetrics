using Entities;
using IServices;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;


namespace Services
{
    public class CountryService : ICountryService
    {
        private readonly IHttpClientFactory _clientFactory;

        public CountryService(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }
        public async Task<Country> GetCountry(string country)
        {
            var client = _clientFactory.CreateClient();
            try
            {
                HttpResponseMessage response = await client.GetAsync($"https://api.mercadolibre.com/classified_locations/countries/{country}");
                string contentJson = await response.Content.ReadAsStringAsync();
                Country result = JsonConvert.DeserializeObject<Country>(contentJson);
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
    }
}
