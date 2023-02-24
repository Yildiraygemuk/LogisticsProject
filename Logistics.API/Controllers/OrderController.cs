using Logistics.Business;
using Logistics.Core.Entities.Exceptions;
using Logistics.Entity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Logistics.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }
        [ProducesResponseType(typeof(OrderVm), 200)]
        [ProducesResponseType(typeof(object), 403)]
        [ProducesResponseType(typeof(object), 401)]
        [HttpGet("GetList")]
        public IActionResult Get()
        {
            var result = _orderService.GetListQueryableOdata();
            if (result.Success)
            {
                return Ok(result.Data);
            }
            return BadRequest(result.Message);
        }
        [ProducesResponseType(typeof(OrderVm), 200)]
        [ProducesResponseType(typeof(object), 403)]
        [ProducesResponseType(typeof(object), 401)]
        [HttpGet("GetById/{id}")]
        public IActionResult GetById(Guid id)
        {
            var result = _orderService.GetById(id);

            if (result.Data == null)
            {
                throw new NotFoundException(id);
            }
            if (result.Success)
            {
                return Ok(result.Data);
            }
            return BadRequest(result.Message);
        }
        [HttpPost]
        public IActionResult Post(OrderDto orderDto)
        {
            var result = _orderService.Post(orderDto);
            return StatusCode(result.StatusCode, result);
        }
        [HttpPut]
        public IActionResult Put(OrderDto orderDto)
        {
            var result = _orderService.Update(orderDto);
            return StatusCode(result.StatusCode, result);
        }
        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            var result = _orderService.Delete(id);
            return StatusCode(result.StatusCode, result);
        }
    }
}
