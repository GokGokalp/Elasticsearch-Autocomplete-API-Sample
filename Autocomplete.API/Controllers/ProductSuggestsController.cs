using System.Threading.Tasks;
using Autocomplete.Business;
using Autocomplete.Business.Objects;
using Microsoft.AspNetCore.Mvc;

namespace Autocomplete.API.Controllers
{
    [Route("api/product-suggests")]
    public class ProductSuggestsController : Controller
    {
        readonly IAutocompleteService _autocompleteService;
        const string PRODUCT_SUGGEST_INDEX = "product_suggest";

        public ProductSuggestsController(IAutocompleteService autocompleteService)
        {
            _autocompleteService = autocompleteService;
        }

        [HttpGet]
        public async Task<ProductSuggestResponse> Get(string keyword)
        {
            return await _autocompleteService.SuggestAsync(PRODUCT_SUGGEST_INDEX, keyword);
        }
    }
}