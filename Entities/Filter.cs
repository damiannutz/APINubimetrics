using System;
using System.Collections.Generic;
using System.Text;

namespace Entities
{
    public class Filter
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public List<FilterValue> Values { get; set; }
        
    }
}
