using AutoMapper;
using BookStore.Busines.Configuration;
using BookStore.Common.Dtos.Discount;
using BookStore.Common.Models;
using BookStore.Common.Models.Interfaces.IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace BookStore.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DiscountController : ControllerBase
    {
        private readonly IDiscountService _service;

        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public DiscountController(IDiscountService service, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _service = service;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("Discounts")]
        [ProducesResponseType(typeof(Discount), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetDiscounts()
        {
            var discounts = await _service.GetDiscountsAsync();
            if (discounts.IsNullOrEmpty())
            {
                return NoContent();
            }
            return Ok(discounts);
        }

        [HttpGet]
        [Route("GetDiscountById/{Id}")]
        [ProducesResponseType(typeof(Discount), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetDiscountById(string Id)
        {
            var discount = await _service.GetDiscountByIdAsync(Id);
            return discount == null ? NotFound() : Ok(discount);
        }

        [HttpGet]
        [Route("FilterDiscounts")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetDiscountsByFilter([FromQuery] DiscountsGetByFilter discountsGetByFilter)
        {
            var discounts = await _service.GetDiscountsByFilter(discountsGetByFilter);
            return Ok(discounts);
        }


        [HttpPost]
        [Route("CreateDiscount")]
        [ProducesResponseType(typeof(Discount), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateDiscout(DiscountCreate discountCreate)
        {
            var discountId = await _service.CreatDiscountAsync(discountCreate);

            return discountId != null ? Ok(discountId) : NotFound();
        }

        [HttpPut]
        [Route("EditDiscount")]
        [ProducesResponseType(typeof(Discount), StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> EditDiscount(DiscountUpdate discountUpdate)
        {
            await _service.UpdateDiscountAsync(discountUpdate);
            return Ok();
        }

        [HttpDelete]
        [Route("DeleteDiscount")]
        [ProducesResponseType(typeof(Discount), StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteDiscount(DiscountDelete discountDelete)
        {
            await _service.DeleteDiscountAsync(discountDelete);
            return Ok();
        }


    }
}
