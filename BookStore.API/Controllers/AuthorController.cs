using AutoMapper;
using BookStore.Busines.Configuration;
using BookStore.Common.Dtos.Author;
using BookStore.Common.Models;
using BookStore.Common.Models.Interfaces.IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace BookStore.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorController : ControllerBase
    {
        private readonly IAuthorService _service;

        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public AuthorController(IAuthorService service, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _service = service;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("GetAuthorById/{Id}")]
        [ProducesResponseType(typeof(Author), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAuthorById(string Id)
        {
            var author = await _service.GetAuthorByIdAsync(Id);
            return author == null? NotFound():Ok(author);
        }

        [HttpGet]
        [Route("Authors")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAuthors()
        {
            var authors = await _service.GetAuthorsAsync();
            if (authors.IsNullOrEmpty())
            {
                return NoContent();
            }
            return Ok(authors);
        }

        [HttpGet]
        [Route("FilterAuthors")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAuthorsByFilter([FromQuery] AuthorsGetByFilter authorsGetByFilter)
        {
            var authors = await _service.GetAuthorsByFilter( authorsGetByFilter);
            return Ok(authors);
        }


        [HttpPost]
        [Route("CreateAuthor")]
        [ProducesResponseType(typeof(Author), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateAuthor(AuthorCreate authorCreate)
        {
            var authorId = await _service.CreatAuthorAsync(authorCreate);

            return authorId != null ? Ok(authorId) : NotFound();
        }

        [HttpPut]
        [Route("EditAuthor")]
        [ProducesResponseType(typeof(Author), StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> EditAuthor(AuthorUpdate author)
        {
            await _service.UpdateAuthorAsync(author);
            return Ok();
        }

        [HttpDelete]
        [Route("DeleteAuthor")]
        [ProducesResponseType(typeof(Author), StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteAuthor(AuthorDelete authorDelete)
        {
            await _service.DeleteAuthorAsync(authorDelete);
            return Ok();
        }

        [HttpPost]
        [Route("AddBooksForAuthor")]
        [ProducesResponseType(typeof(AuthorBook), StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> AddAuthorBooks(ModifieAuthorBooks modifieAuthorBooks)
        {
            await _service.AddAuthorBooksAsync(modifieAuthorBooks);
            return Ok();
        }

        [HttpDelete]
        [Route("DeleteBooksFromAuthor")]
        [ProducesResponseType(typeof(AuthorBook), StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteAuthorBooks(ModifieAuthorBooks modifieAuthorBooks)
        {
            await _service.DeleteAuthorBooks(modifieAuthorBooks);
            return Ok();
        }


    }
}
