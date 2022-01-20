using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

using System.Text.Json.Serialization;

namespace Entities
{
    public class Value
    {
        public string Id { get; set; }
        public string Name { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public int? Results { get; set; }

        [JsonProperty("path_from_root", NullValueHandling = NullValueHandling.Ignore)]
        public List<Value> PathFromRoot { get; set; }

    }
}
