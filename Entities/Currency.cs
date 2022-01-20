using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities
{
    public class Currency
    {
        public string Id { get; set; }
        public string Symbol { get; set; }
        public string Description { get; set; }
        [JsonProperty("decimal_places")]
        public int DecimalPlaces { get; set; }

        public CurrencyConvertion ToDolar { get; set; }
    }
}
