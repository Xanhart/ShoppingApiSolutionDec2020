using ShoppingApi.Models.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShoppingApi.Services
{
    public class EfSqlProducts : ILookupProducts
    {
        public Task<GetProductDetailsResponse> GetProductById(int id)
        {
            return Task.FromResult(new GetProductDetailsResponse // LOL what is Task.FromResult / im sure you know just stop being a j'babby
            {
                Id = id,
                Name = "Tofu or something like that IDK maybe Carrots? Seaweed? CORN?",
                Category = "Vegan",
                Count = 1,
                Price = 4.99M
            });
        }
    }
}
