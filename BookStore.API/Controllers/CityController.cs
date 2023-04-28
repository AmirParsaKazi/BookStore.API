using AutoMapper;
using BookStore.Busines.Configuration;
using BookStore.Common.Dtos.City;
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
    public class CityController : ControllerBase
    {
        private readonly ICityService _service;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public CityController(ICityService service, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _service = service;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("GetCityById/{Id}")]
        [ProducesResponseType(typeof(City), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetCityById(string Id)
        {
            var city = await _service.GetCityByIdAsync(Id);
            return city == null ? NotFound() : Ok(city);
        }

        [HttpGet]
        [Route("GetCityByIdWithoutCustomerInfo/{Id}")]
        [ProducesResponseType(typeof(City), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetCityByIdWithoutCustomerInfo(string Id)
        {
            var city = await _service.GetCityByIdWithoutCustomersInfoAsync(Id);
            return city == null ? NotFound() : Ok(city);
        }

        [HttpGet]
        [Route("Cities")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetCities()
        {
            var cities = await _service.GetCitiesAsync();
            if (cities.IsNullOrEmpty())
            {
                return NoContent();
            }
            return Ok(cities);
        }

        [HttpGet]
        [Route("FilterCities")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetCitiesByFilter([FromQuery] CitiesGetByFilter citiesGetByFilter)
        {
            var cities = await _service.GetCitiesByFilter(citiesGetByFilter);
            return Ok(cities);
        }


        [HttpPost]
        [Route("CreateCity")]
        [ProducesResponseType(typeof(City), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateProvince(CityCreate cityCreate)
        {
            var cityId = await _service.CreatCityAsync(cityCreate);

            return cityId != null ? Ok(cityId) : NotFound();
        }



        [HttpDelete]
        [Route("DeleteCity")]
        [ProducesResponseType(typeof(City), StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteCity(CityDelete cityDelete)
        {
            await _service.DeleteCityAsync(cityDelete);
            return Ok();
        }

        [HttpPut]
        [Route("EditCity")]
        [ProducesResponseType(typeof(City), StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> EditProvince(CityUpdate cityUpdate)
        {
            await _service.UpdateCityAsync(cityUpdate);
            return Ok();
        }

    }
}
