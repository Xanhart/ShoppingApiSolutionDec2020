using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using ShoppingApi.Data;
using ShoppingApi.Models.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShoppingApi.Services
{
    public class EfSqlProducts : ILookupProducts, ICommandProducts
    {
        private readonly ShoppingDataContext _context;
        private readonly IMapper _mapper;
        private readonly MapperConfiguration _mapperConfig;
        private readonly IOptions<ConfigurationForPricing> _configurationForPricing;

        public EfSqlProducts(ShoppingDataContext context, IMapper mapper, MapperConfiguration mapperConfig, IOptions<ConfigurationForPricing> configurationForPricing)
        {
            _context = context;
            _mapper = mapper;
            _mapperConfig = mapperConfig;
            _configurationForPricing = configurationForPricing;
        }

        public async Task<GetProductDetailsResponse> Add(PostProductRequest productToAdd)
        {
            //
            var product = _mapper.Map<Product>(productToAdd);

            //product.Category.Name = productToAdd.Category;
            var category = await _context.Categories.SingleOrDefaultAsync(c => c.Name == productToAdd.Category);
            if(category == null)
            {
                var newCategory = new Category { Name = productToAdd.Category };
                _context.Categories.Add(newCategory);
                product.Category = newCategory;
            }
            else
            {
                product.Category = category;
            }

            product.Price = productToAdd.Cost.Value * _configurationForPricing.Value.Markup; // get this from config

            _context.Products.Add(product);
            await _context.SaveChangesAsync();

            return _mapper.Map<GetProductDetailsResponse>(product);
        }

        public async Task<GetProductDetailsResponse> GetProductById(int id)
        { 
            var response = await _context.Products
                .Where(p => p.Id == id && p.RemovedFromInventory == false)
                .ProjectTo<GetProductDetailsResponse>(_mapperConfig)
                .SingleOrDefaultAsync();
            return response;
        }
    }
}
