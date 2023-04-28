using AutoMapper;
using BookStore.Busines.Configuration;
using BookStore.Common.Dtos.OrderStatus;
using BookStore.Common.Dtos.Province;
using BookStore.Common.Models;
using BookStore.Common.Models.Interfaces.IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace BookStore.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderStatusController : ControllerBase
    {
        private readonly IOrderStatusService _service;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public OrderStatusController(IOrderStatusService service, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _service = service;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("GetOrderStatusById/{Id}")]
        [ProducesResponseType(typeof(OrderStatus), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetOrderStatusById(string Id)
        {
            var orderStatus = await _service.GetOrderStatusByIdAsync(Id);
            return orderStatus == null ? NotFound() : Ok(orderStatus);
        }

        [HttpGet]
        [Route("GetOrderStatusByIdWithoutOrdersInfo/{Id}")]
        [ProducesResponseType(typeof(OrderStatus), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetOrderStatusByIdWithoutOrdersInfoAsync(string Id)
        {
            var orderStatus = await _service.GetOrderStatusByIdWithoutOrdersInfoAsync(Id);
            return orderStatus == null ? NotFound() : Ok(orderStatus);
        }

        [HttpGet]
        [Route("OrderStatuses")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetOrderStatuses()
        {
            var orderStatuses = await _service.GetOrderStatusesAsync();
            if (orderStatuses.IsNullOrEmpty())
            {
                return NoContent();
            }
            return Ok(orderStatuses);
        }


        [HttpGet]
        [Route("FilterOrderStatuses")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetOrderStatusesByFilter([FromQuery] OrderStatusesGetByFilter orderStatusesGetByFilter)
        {
            var orderStatuses = await _service.GetOrderStatusesByFilter(orderStatusesGetByFilter);
            return Ok(orderStatuses);
        }


        [HttpPost]
        [Route("CreateOrderStatus")]
        [ProducesResponseType(typeof(OrderStatus), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateOrderStatus(OrderStatusCreate orderStatusCreate)
        {
            var orderStatusId = await _service.CreatOrderStatusAsync(orderStatusCreate);

            return orderStatusId != null ? Ok(orderStatusId) : NotFound();
        }

        [HttpDelete]
        [Route("DeleteOrderStatus")]
        [ProducesResponseType(typeof(OrderStatus), StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteOrderStatus(OrderStatusDelete orderStatusDelete)
        {
            await _service.DeleteOrderStatusAsync(orderStatusDelete);
            return Ok();
        }

        [HttpPut]
        [Route("EditOrderStatus")]
        [ProducesResponseType(typeof(OrderStatus), StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> EditProvince(OrderStatusUpdate orderStatusUpdate)
        {
            await _service.UpdateOrderStatusAsync(orderStatusUpdate);
            return Ok();
        }
    }
}
