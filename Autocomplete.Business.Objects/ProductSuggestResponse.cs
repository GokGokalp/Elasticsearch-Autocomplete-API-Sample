using System.Collections.Generic;

namespace Autocomplete.Business.Objects
{
    public class ProductSuggestResponse
    {
        public IEnumerable<ProductSuggest> Suggests { get; set; }
    }

    public class ProductSuggest
    {
        public string Name { get; set; }
        public double Score { get; set; }  
    }
}