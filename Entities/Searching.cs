using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities
{
    public class Searching
    {
        [JsonProperty("site_id")]
        public string SiteId { get; set; }

        [JsonProperty("country_default_time_zone")]
        public string CountryDefaultTimeZone { get; set; }
        public string Query { get; set; }
        public Paging Paging { get; set; }
        public List<Result> Results { get; set; }
        public Value Sort { get; set; }
        [JsonProperty("available_sorts")] 
        public List<Value> AvailableSorts { get; set; }
        public List<Filter> Filters { get; set; }
        [JsonProperty("available_filters")]
        public List<Filter> AvailableFilters { get; set; }
        
    }
}
