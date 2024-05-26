using AutoMapper;
using Drink_Wholesale.DTO;
using Drink_Wholesale.Models;
using Drink_Wholesale.Persistence.Services;
using Microsoft.AspNetCore.Mvc;

namespace Drink_Wholesale.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    
    {
        private readonly IDrinkWholesaleService _service;
        private readonly IMapper _mapper;
        public CategoryController(IDrinkWholesaleService service,IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        [HttpGet("{Id}")]
        public ActionResult<CategoryDto> GetCategory(int Id)
        {
            try
            {
                return _mapper.Map<CategoryDto>(_service.GetCategoryById(Id));
            }
            catch (InvalidOperationException)
            {
                return NotFound();
            }

        }

        [HttpGet]
        public ActionResult<IEnumerable<CategoryDto>> GetCategories()
        {
            return _service.GetCategories().Select(i=> _mapper.Map<CategoryDto>(i)).ToList();
        }
    }
}
