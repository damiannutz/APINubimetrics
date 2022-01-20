using System;
using System.Collections.Generic;
using System.Text;

namespace Entities
{
    public class FilterValue : Value
    {
        public string Type { get; set; }
        public List<Value> Values { get; set; }
    }
}
