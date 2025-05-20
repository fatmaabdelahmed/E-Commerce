using AutoMapper;
using Ecom.Core.DTOs;
using Ecom.Core.Entities.Product;

namespace Ecom.API.Mapping
{
    public class ProductMapping:Profile
    {
        public ProductMapping() {
            CreateMap<Product, ProductDTO>().ForMember(x => x.CategoryName,
                op => op.MapFrom(src => src.Category.Name)).ReverseMap();   
            CreateMap<Photo,PhotoDTO>().ReverseMap();
            CreateMap<AddProductDTO,Product>()
                .ForMember(n=>n.Photos,op=>op.Ignore())
                .ReverseMap();    
        }
    }
}
