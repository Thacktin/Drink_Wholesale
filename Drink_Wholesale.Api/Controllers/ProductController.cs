using AutoMapper;
using Drink_Wholesale.DTO;
using Drink_Wholesale.Models;
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
        public ActionResult<ProductDto> GetProduct(int Id)
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
        public ActionResult<IEnumerable<ProductDto>> GetProducts(int subCategoryId)
        {
            try
            {
                return _service.GetSubCategoryById(subCategoryId).Products.Select(i => _mapper.Map<ProductDto>(i)).ToList();
            }
            catch (Exception)
            {
                return NotFound();
            }
        }
    }
}
