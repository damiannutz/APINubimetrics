using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Entities
{
    public class Country
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string Locale { get; set; }

        [JsonProperty("currency_id")]
        public string CurrencyId { get; set; }

        [JsonProperty("decimal_separator")]
        public string DecimalSeparator { get; set; }

        [JsonProperty("thousands_separator")]
        public string ThousandsSeparator { get; set; }

        [JsonProperty("time_zone")]
        public string TimeZone { get; set; }

        [JsonProperty("geo_information")]
        public GeoInformation GeoInformation { get; set; }

        public List<State> States { get; set; }

    }
}
