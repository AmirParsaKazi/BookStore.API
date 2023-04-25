using AutoMapper;
using BookStore.Busines.Configuration;
using BookStore.Common.Dtos.Author;
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
    public class ProvinceController : ControllerBase
    {
        private readonly IProvinceService _service;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public ProvinceController(IProvinceService service, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _service = service;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("GetProvinceById/{Id}")]
        [ProducesResponseType(typeof(Province), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetProvinceById(string Id)
        {
            var province = await _service.GetProvinceByIdAsync(Id);
            return province == null ? NotFound() : Ok(province);
        }

        [HttpGet]
        [Route("Provinces")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetProvinces()
        {
            var provinces = await _service.GetProvincesAsync();
            if (provinces.IsNullOrEmpty())
            {
                return NoContent();
            }
            return Ok(provinces);
        }

        [HttpGet]
        [Route("FilterProvinces")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetProvincesByFilter([FromQuery] ProvincesGetByFilter provincesGetByFilter)
        {
            var provinces = await _service.GetProvincesByFilter(provincesGetByFilter);
            return Ok(provinces);
        }


        [HttpPost]
        [Route("CreateProvince")]
        [ProducesResponseType(typeof(Province), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateProvince(ProvinceCreate provinceCreate)
        {
            var provinceId = await _service.CreatProvinceAsync(provinceCreate);

            return provinceId != null ? Ok(provinceId) : NotFound();
        }

        [HttpDelete]
        [Route("DeleteProvince")]
        [ProducesResponseType(typeof(Province), StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteProvince(ProvinceDelete provinceDelete)
        {
            await _service.DeleteProvinceAsync(provinceDelete);
            return Ok();
        }

        [HttpPut]
        [Route("EditProvince")]
        [ProducesResponseType(typeof(Province), StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> EditProvince(ProvinceUpdate provinceUpdate)
        {
            await _service.UpdateProvinceAsync(provinceUpdate);
            return Ok();
        }
    }
}
