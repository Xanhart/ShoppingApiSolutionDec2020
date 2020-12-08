using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using ShoppingApi.Models.Products;
using ShoppingApi.Services;

namespace ShoppingApi.Controllers
{
    public class ProductsController : ControllerBase
    {
        private readonly ILookupProducts _productLookup;
        private readonly ICommandProducts _commandProducts;

        public ProductsController(ILookupProducts productLookup, ICommandProducts commandProducts)
        {
            _productLookup = productLookup;
            _commandProducts = commandProducts;
        }

        [HttpGet("/products/{id:int}", Name ="products#getproductbyid")]
        public async Task<ActionResult> GetProductsById(int Id)
        {
            GetProductDetailsResponse response = await _productLookup.GetProductById(Id);
            return this.Maybe(response);
        }

        [HttpPost("/products")]
        public async Task<ActionResult> AddAProduct([FromBody] PostProductRequest productToAdd)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            else
            {
                GetProductDetailsResponse response = await _commandProducts.Add(productToAdd);
                return CreatedAtRoute("products#getproductbyid", new { id = response.Id}, response);
            }
        }
    }
}
