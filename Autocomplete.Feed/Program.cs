using System;
using System.Collections.Generic;
using Autocomplete.Business;
using Autocomplete.Business.Objects;
using Nest;

namespace Autocomplete.Feed
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Product> products = new List<Product>();

            products.Add(new Product()
            {
                Name = "Samsung Galaxy Note 8",
                Suggest = new CompletionField()
                {
                    Input = new[] { "Samsung Galaxy Note 8", "Galaxy Note 8", "Note 8" }
                }
            });

            products.Add(new Product()
            {
                Name = "Samsung Galaxy S8",
                Suggest = new CompletionField()
                {
                    Input = new[] { "Samsung Galaxy S8", "Galaxy S8", "S8" }
                }
            });

            products.Add(new Product()
            {
                Name = "Apple Iphone 8",
                Suggest = new CompletionField()
                {
                    Input = new[] { "Apple Iphone 8", "Iphone 8" }
                }
            });

            products.Add(new Product()
            {
                Name = "Apple Iphone X",
                Suggest = new CompletionField()
                {
                    Input = new[] { "Apple Iphone X", "Iphone X" }
                }
            });

            products.Add(new Product()
            {
                Name = "Apple iPad Pro",
                Suggest = new CompletionField()
                {
                    Input = new[] { "Apple iPad Pro", "iPad Pro" }
                }
            });

            var connectionSettings = new ConnectionSettings(new Uri("http://localhost:9200"));
            IAutocompleteService autocompleteService = new AutocompleteService(connectionSettings);
            string productSuggestIndex = "product_suggest";

            bool isCreated = autocompleteService.CreateIndexAsync(productSuggestIndex).Result;

            if (isCreated)
            {
                autocompleteService.IndexAsync(productSuggestIndex, products).Wait();
            }
        }
    }
}