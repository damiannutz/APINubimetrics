using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities
{
    public class CurrencyConvertion
    {
        [JsonProperty("currency_base")]
        public string CurrencyBase { get; set; }
        [JsonProperty("currency_quote")]
        public string CurrencyQuote { get; set; }
        public decimal Ratio { get; set; }
        public decimal Rate{ get; set; }
        [JsonProperty("inv_rate")]
        public decimal InvRate { get; set; }
        [JsonProperty("creation_date")]
        public string CreationDate{ get; set; }
        [JsonProperty("valid_until")]
        public string ValidUntil { get; set; }
    }
}
