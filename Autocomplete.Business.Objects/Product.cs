using Nest;

namespace Autocomplete.Business.Objects
{
    public class Product
    {
        public string Name { get; set; }
        public CompletionField Suggest {get;set;}
    }
}