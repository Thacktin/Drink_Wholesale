using AutoMapper;
using Drink_Wholesale.DTO;
using Drink_Wholesale.Models;

namespace Drink_Wholesale.WebApi.MappingConfigurations
{
    public class EntityProfiles
    {
        public class CategoryProfile : Profile
        {
            public CategoryProfile()
            {
                CreateMap<Category, CategoryDto>();
            }
        }

        public class SubCategoryProfile : Profile
        {
            public SubCategoryProfile()
            {
                CreateMap<SubCategory, SubCategoryDto>();
            }
        }

        public class SubCategoryDtoProfile : Profile
        {
            public SubCategoryDtoProfile()
            {
                CreateMap<SubCategoryDto, SubCategory>();
            }
        }

        public class ProductProfile : Profile
        {
            public ProductProfile()
            {
                CreateMap<Product, ProductDto>();
            }
        }
        public class ProductDtoProfile : Profile
        {
            public ProductDtoProfile()
            {
                CreateMap<ProductDto, Product>();
            }
        }
    }
}
