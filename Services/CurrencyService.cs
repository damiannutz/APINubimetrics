using Entities;
using IServices;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class CurrencyService : ICurrencyService
    {
        private readonly IHttpClientFactory _clientFactory;
        private readonly IConfiguration _config;

        public CurrencyService(IHttpClientFactory clientFactory, IConfiguration config)
        {
            _clientFactory = clientFactory;
            _config = config;
        }
        public async Task<List<Currency>> GetSave()
        {
            var client = _clientFactory.CreateClient();
            try
            {
                HttpResponseMessage response = await client.GetAsync($"https://api.mercadolibre.com/currencies/");
                string contentJson = await response.Content.ReadAsStringAsync();
                List<Currency> currencyResult = JsonConvert.DeserializeObject<List<Currency>>(contentJson);

                foreach (Currency currencyItem in currencyResult)
                {
                    currencyItem.ToDolar = await GetCurrencyConvertionToDolar(client, currencyItem.Id);
                }
                Task tJson = new Task(() => WriteJson(currencyResult));
                tJson.Start();
                Task tCsv = new Task(() => WriteCSV(currencyResult));
                tCsv.Start();
                tJson.Wait();
                tCsv.Wait();

                return currencyResult;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        private async Task<CurrencyConvertion> GetCurrencyConvertionToDolar(HttpClient client, string currencyId)
        {
            HttpResponseMessage response = await client.GetAsync($"https://api.mercadolibre.com/currency_conversions/search?from={currencyId}&to=USD");
            string contentJson = await response.Content.ReadAsStringAsync();
            CurrencyConvertion currencyConvertionResult = JsonConvert.DeserializeObject<CurrencyConvertion>(contentJson);
            
            return currencyConvertionResult;
        }

        private void WriteJson(List<Currency> currencies)
        {
            try
            {
                string path = System.Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                path = Path.Combine(path, "currencies.JSON");

                using (StreamWriter file = File.CreateText(path))
                {
                    JsonSerializer serializer = new JsonSerializer();
                    serializer.Serialize(file, currencies);
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
        private void WriteCSV(List<Currency> currencies)
        {
            try
            {
                string path = System.Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                path = Path.Combine(path, "ratios.csv");

                using (StreamWriter file = File.CreateText(path))
                {
                    currencies.ForEach(c =>
                    {
                        file.Write($"{c.ToDolar.Ratio.ToString().Replace(",",".")},");
                    });
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
