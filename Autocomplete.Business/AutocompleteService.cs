using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Autocomplete.Business.Objects;
using Nest;

namespace Autocomplete.Business
{
    public class AutocompleteService : IAutocompleteService
    {
        readonly ElasticClient _elasticClient;

        public AutocompleteService(ConnectionSettings connectionSettings)
        {
            _elasticClient = new ElasticClient(connectionSettings);
        }

        public async Task<bool> CreateIndexAsync(string indexName)
        {
            var createIndexDescriptor = new CreateIndexDescriptor(indexName)
                .Mappings(ms => ms
                          .Map<Product>(m => m
                                .AutoMap()
                                .Properties(ps => ps
                                    .Completion(c => c
                                        .Name(p => p.Suggest))))

                         );

            if (_elasticClient.IndexExists(indexName.ToLowerInvariant()).Exists)
            {
                _elasticClient.DeleteIndex(indexName.ToLowerInvariant());
            }

            ICreateIndexResponse createIndexResponse = await _elasticClient.CreateIndexAsync(createIndexDescriptor);

            return createIndexResponse.IsValid;
        }

        public async Task IndexAsync(string indexName, List<Product> products)
        {
            await _elasticClient.IndexManyAsync(products, indexName);
        }

        public async Task<ProductSuggestResponse> SuggestAsync(string indexName, string keyword)
        {
            ISearchResponse<Product> searchResponse = await _elasticClient.SearchAsync<Product>(s => s
                                     .Index(indexName)
                                     .Suggest(su => su
                                          .Completion("suggestions", c => c
                                               .Field(f => f.Suggest)
                                               .Prefix(keyword)
                                               .Fuzzy(f => f
                                                   .Fuzziness(Fuzziness.Auto)
                                               )
                                               .Size(5))
                                             ));

            var suggests = from suggest in searchResponse.Suggest["suggestions"]
                              from option in suggest.Options
                              select new ProductSuggest
                              {
                                    Name = option.Text,
                                    Score = option.Score
                              };

            return new ProductSuggestResponse
            {
                Suggests = suggests
            };
        }
    }
}