using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities
{
    public class Paging
    {
        public int Total { get; set; }
        [JsonProperty("primary_results")]
        public int PrimaryResults { get; set; }
        public int Offset { get; set; }
        public int Limit { get; set; }

    }
}
