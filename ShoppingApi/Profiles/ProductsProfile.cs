using AutoMapper;
using ShoppingApi.Data;
using ShoppingApi.Models.Products;

namespace ShoppingApi.Profiles
{
    public class ProductsProfile : Profile
    {
        public ProductsProfile()
        {
            CreateMap<Product, GetProductDetailsResponse>()
                .ForMember(dest => dest.Category, opt => opt.MapFrom(src => src.Category.Name));
        }
    }
}
