using AutoMapper;
using Drink_Wholesale.DTO;
using Drink_Wholesale.Models;
using Drink_Wholesale.Persistence.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Drink_Wholesale.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : Controller
    {
        private readonly IDrinkWholesaleService _service;
        private readonly IMapper _mapper;
        public OrderController(IDrinkWholesaleService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        [HttpGet("{Id}")]
        public ActionResult<OrderDto> GetOrder(int Id)
        {
            try
            {
                return _mapper.Map<OrderDto>(_service.GetOrderById(Id));
            }
            catch (InvalidOperationException)
            {
                return NotFound();
            }

        }

        [HttpGet]
        public ActionResult<IEnumerable<OrderDto>> GetOrders()
        {
            try
            {
                return _service.GetAllOrders().Select(i => _mapper.Map<OrderDto>(i)).ToList();
            }
            catch (Exception ex)
            {
                return NotFound();
            }
        }

        [HttpPost]
        [Authorize]
        public ActionResult<OrderDto> PostOrder(OrderDto orderDto)
        {
            var order = _service.AddOrder(_mapper.Map<Order>(orderDto));
            if (order is null)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            else
            {
                return CreatedAtAction(nameof(GetOrder), new { Id = order.Id },
                    _mapper.Map<SubCategoryDto>(order));
            }
        }

        [HttpPut("{id}")]
        [Authorize]
        public IActionResult PutOrder(int id, OrderDto orderDto)
        {
            if (id != orderDto.Id)
            {
                return BadRequest();
            }
            if (_service.UpdateOrder(_mapper.Map<Order>(orderDto)))
            {
                return Ok();
            }
            else
            {
                return StatusCode(500);
            }
        }

        [HttpPatch("{id}")]
        [Authorize]
        public IActionResult PatchOrder(int id, OrderDto orderDto)
        {
            if (id != orderDto.Id)
            {
                return BadRequest();
            }
            if (_service.ChangeOrderState(_mapper.Map<Order>(orderDto)))
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
