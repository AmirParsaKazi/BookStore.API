using AutoMapper;
using BookStore.Busines.Configuration;
using BookStore.Common.Dtos.Author;
using BookStore.Common.Dtos.Language;
using BookStore.Common.Models;
using BookStore.Common.Models.Interfaces.IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace BookStore.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LanguageController : ControllerBase
    {
        private readonly ILanguageService _service;

        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public LanguageController(ILanguageService service, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _service = service;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }



        [HttpGet]
        [Route("Languages")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetLanguages()
        {
            var language = await _service.GetLanguagesAsync();
            if (language.IsNullOrEmpty())
            {
                return NoContent();
            }
            return Ok(language);
        }

        [HttpGet]
        [Route("GetLanguageById/{Id}")]
        [ProducesResponseType(typeof(Language), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetLanguageById(string Id)
        {
            var language = await _service.GetLanguageByIdAsync(Id);
            return language == null ? NotFound() : Ok(language);
        }

        [HttpGet]
        [Route("FilterLanguages")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAuthorsByFilter([FromQuery] LanguageGetByFilter languageGetByFilter)
        {
            var languages = await _service.GetLanguageByFilter(languageGetByFilter);
            return Ok(languages);
        }


        [HttpPost]
        [Route("CreateLanguage")]
        [ProducesResponseType(typeof(Language), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateLanguage(LanguageCreate languageCreate)
        {
            var languageId = await _service.CreatLanguageAsync(languageCreate);

            return languageId != null ? Ok(languageId) : NotFound();
        }

        [HttpPut]
        [Route("EditLanguage")]
        [ProducesResponseType(typeof(Language), StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> EditAuthor(LanguageUpdate language)
        {
            await _service.UpdateLanguageAsync(language);
            return Ok();
        }

        [HttpDelete]
        [Route("DeleteLanguage")]
        [ProducesResponseType(typeof(Language), StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteLanguage(LanguageDelete languageDelete)
        {
            await _service.DeleteLanguageAsync(languageDelete);
            return Ok();
        }


    }
}
