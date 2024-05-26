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
        public ActionResult<IEnumerable<ProductDto>> GetProducts(int subCatId)
        {
            try
            {
                return _service.GetSubCategoryById(subCatId).Products.Select(i => _mapper.Map<ProductDto>(i)).ToList();
            }
            catch (Exception)
            {
                return NotFound();
            }
        }

        [HttpPost]
        public ActionResult<SubCategoryDto> PostProduct(ProductDto productDto)
        {
            var subCategory = _service.AddSubcategory(_mapper.Map<SubCategory>(productDto));
            if (subCategory is null)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            else
            {
                return CreatedAtAction(nameof(GetProduct), new { Id = subCategory.Id },
                    _mapper.Map<SubCategoryDto>(subCategory));
            }
        }

        [HttpPut("{id}")]
        public IActionResult PutProduct(int id,ProductDto productDto)
        {
            if (id != productDto.Id)
            {
                return BadRequest();
            }
            if(_service.UpdateProduct(_mapper.Map<Product>(productDto)))
            {
                return Ok();
            }
            else
            {
                return StatusCode(500);
            }
        }
    }
}
