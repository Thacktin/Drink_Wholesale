using AutoMapper;
using Drink_Wholesale.DTO;
using Drink_Wholesale.Admin.ViewModel;

namespace Drink_Wholesale.WebApi.MappingConfigurations
{
    public class EntityProfiles
    {
        public class CategoryDtoProfile : Profile
        {
            public CategoryDtoProfile()
            {
                CreateMap<CategoryDto, CategoryViewModel>();
            }
        }



        public class SubCategoryDtoProfile : Profile
        {
            public SubCategoryDtoProfile()
            {
                CreateMap<SubCategoryDto, SubCategoryViewModel>();
            }
        }

        public class SubCategoryViewModelProfile : Profile
        {
            public SubCategoryViewModelProfile()
            {
                CreateMap<SubCategoryViewModel,SubCategoryDto>();
            }
        }

        public class ProductDtoProfile : Profile
        {
            public ProductDtoProfile()
            {
                CreateMap<ProductDto, ProductViewModel>();
            }
        }
    }
}
