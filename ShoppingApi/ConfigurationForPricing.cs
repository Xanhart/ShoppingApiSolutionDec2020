using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShoppingApi
{
    public class ConfigurationForPricing
    {
        public string SectionName = "ProductPricing";
        public decimal Markup { get; set; }
    }
}
