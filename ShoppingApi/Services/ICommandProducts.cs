using System.Threading.Tasks;
using ShoppingApi.Models.Products;

namespace ShoppingApi.Services
{
    public interface ICommandProducts
    {
        Task<GetProductDetailsResponse> Add(PostProductRequest productToAdd);
    }
}
