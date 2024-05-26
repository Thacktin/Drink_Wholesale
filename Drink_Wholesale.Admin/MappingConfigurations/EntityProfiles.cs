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
        public class ProductViewModelProfile : Profile
        {
            public ProductViewModelProfile()
            {
                CreateMap<ProductViewModel, ProductDto>();
            }
        }


        public class OrderViewModelProfile : Profile
        {
            public OrderViewModelProfile()
            {
                CreateMap<OrderViewModel, OrderDto>();
            }
        }

        public class OrderDtoProfile : Profile
        {
            public OrderDtoProfile()
            {
                CreateMap<OrderDto, OrderViewModel>();
            }
        }


        public class CartItemViewModelProfile : Profile
        {
            public CartItemViewModelProfile()
            {
                CreateMap<CartItemViewModel, CartItemDto>();
            }
        }

        public class CartItemDtoProfile : Profile
        {
            public CartItemDtoProfile()
            {
                CreateMap<CartItemDto, CartItemViewModel>();
            }
        }
    }
}
