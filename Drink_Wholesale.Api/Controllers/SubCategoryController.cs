using AutoMapper;
using Drink_Wholesale.DTO;
using Drink_Wholesale.Models;
using Drink_Wholesale.Persistence.Services;
using Microsoft.AspNetCore.Mvc;

namespace Drink_Wholesale.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubCategoryController : ControllerBase

    {
        private readonly IDrinkWholesaleService _service;
        private readonly IMapper _mapper;
        public SubCategoryController(IDrinkWholesaleService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        [HttpGet("{Id}")]
        public ActionResult<SubCategoryDto> GetSubCategory(int Id)
        {
            try
            {
                return _mapper.Map<SubCategoryDto>(_service.GetSubCategoryById(Id));
            }
            catch (InvalidOperationException)
            {
                return NotFound();
            }

        }

        [HttpGet]
        public ActionResult<IEnumerable<SubCategoryDto>> GetCategories()
        {
            return _service.GetSubCategories().Select(i => _mapper.Map<SubCategoryDto>(i)).ToList();
        }
    }
}