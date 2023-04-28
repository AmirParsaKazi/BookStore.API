using AutoMapper;
using BookStore.Busines.Configuration;
using BookStore.Common.Dtos.Customer;
using BookStore.Common.Models;
using BookStore.Common.Models.Interfaces.IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace BookStore.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerService _service;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public CustomerController(ICustomerService service, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _service = service;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("Customers")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetCustomers()
        {
            var customers = await _service.GetCustomersAsync();
            if (customers.IsNullOrEmpty())
            {
                return NoContent();
            }
            return Ok(customers);
        }

        [HttpPost]
        [Route("CreateCustomer")]
        [ProducesResponseType(typeof(Customer), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateCustomer(CustomerCreate customerCreate)
        {
            var customerId = await _service.CreatCustomerAsync(customerCreate);

            return customerId != null ? Ok(customerId) : NotFound();
        }

        [HttpDelete]
        [Route("DeleteCustomer")]
        [ProducesResponseType(typeof(Customer), StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteCustomer(CustomerDelete customerDelete)
        {
            await _service.DeleteCustomerAsync(customerDelete);
            return Ok();
        }

        [HttpPut]
        [Route("EditCustomer")]
        [ProducesResponseType(typeof(Customer), StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> EditCustomer(CustomerUpdate customerUpdate)
        {
            await _service.UpdateCustomerAsync(customerUpdate);
            return Ok();
        }

    }
}
