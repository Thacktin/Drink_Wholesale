using AutoMapper;
using Drink_Wholesale.DTO;
using Drink_Wholesale.Persistence.Services;
using Microsoft.AspNetCore.Mvc;

namespace Drink_Wholesale.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase

    {
        private readonly IDrinkWholesaleService _service;
        private readonly IMapper _mapper;
        public ProductController(IDrinkWholesaleService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        [HttpGet("{Id}")]
        public ActionResult<ProductDto> GetSubCategory(int Id)
        {
            try
            {
                return _mapper.Map<ProductDto>(_service.GetProductById(Id));
            }
            catch (InvalidOperationException)
            {
                return NotFound();
            }

        }

        [HttpGet]
        public ActionResult<IEnumerable<ProductDto>> GetCategories()
        {
            return _service.GetAllProducts().Select(i => _mapper.Map<ProductDto>(i)).ToList();
        }
    }
}
