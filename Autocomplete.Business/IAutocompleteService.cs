using System.Collections.Generic;
using System.Threading.Tasks;
using Autocomplete.Business.Objects;

namespace Autocomplete.Business
{
    public interface IAutocompleteService
    {
        Task<bool> CreateIndexAsync(string indexName);
        Task IndexAsync(string indexName, List<Product> products);
        Task<ProductSuggestResponse> SuggestAsync(string indexName, string keyword);
    }
}