using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities
{
    public class Result
    {
        public string Id { get; set; }
        [JsonProperty("site_id")]
        public string SiteId { get; set; }
        public string Title { get; set; }
        public decimal Price { get; set; }

        public int SellerId { get; set; }
        public string Permalink { get; set; }
        
    }
}
