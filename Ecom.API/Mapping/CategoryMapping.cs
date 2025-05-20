using AutoMapper;
using Ecom.Core.DTOs;
using Ecom.Core.Entities.Product;

namespace Ecom.API.Mapping
{
    public class CategoryMapping:Profile 
    {
        public CategoryMapping() {
            CreateMap<CategoryDTO,Category>().ReverseMap();
            CreateMap<UpdateCategoryDTO,Category>().ReverseMap();
        }
    }
}
