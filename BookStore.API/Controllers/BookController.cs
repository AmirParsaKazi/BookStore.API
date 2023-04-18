using AutoMapper;
using BookStore.Busines.Configuration;
using BookStore.Common.Dtos.Author;
using BookStore.Common.Dtos.Book;
using BookStore.Common.Dtos.Category;
using BookStore.Common.Models;
using BookStore.Common.Models.Interfaces.IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace BookStore.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly IBookService _service;

        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public BookController(IBookService service, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _service = service;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }


        [HttpGet]
        [Route("Books")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetBooks()
        {
            var books = await _service.GetBooksAsync();
            if (books.IsNullOrEmpty())
            {
                return NoContent();
            }
            return Ok(books);
        }


        [HttpGet]
        [Route("GetBookById/{Id}")]
        [ProducesResponseType(typeof(Book), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetBookById(string Id)
        {
            var book = await _service.GetBookByIdAsync(Id);
            return book == null ? NotFound() : Ok(book);
        }


        [HttpGet]
        [Route("FilterBooks")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetBooksByFilter([FromQuery] BooksGetByFilter booksGetByFilter)
        {
            var books = await _service.GetBooksByFilter(booksGetByFilter);
            return Ok(books);
        }


        [HttpPost]
        [Route("CreateBook")]
        [ProducesResponseType(typeof(Book), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateBook(BookCreate bookCreate)
        {
            var bookId = await _service.CreatBookAsync(bookCreate);

            return bookId != null ? Ok(bookId) : NotFound();
        }

        [HttpPut]
        [Route("EditBook")]
        [ProducesResponseType(typeof(Book), StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> EditBook(BookUpdate bookUpdate)
        {
            await _service.UpdateBookAsync(bookUpdate);
            return Ok();
        }

        [HttpDelete]
        [Route("DeleteBook")]
        [ProducesResponseType(typeof(Book), StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteAuthor(BookDelete bookDelete)
        {
            await _service.DeleteBookAsync(bookDelete);
            return Ok();
        }

        [HttpPost]
        [Route("AddAuthorsForBook")]
        [ProducesResponseType(typeof(AuthorBook), StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> AddBookAuthors(ModifieBookAuthors modifieBookAuthors)
        {
            await _service.AddBookAuthorsAsync(modifieBookAuthors);
            return Ok();
        }

        [HttpDelete]
        [Route("DeleteAuthorsForBook")]
        [ProducesResponseType(typeof(AuthorBook), StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteBookAuthors(ModifieBookAuthors modifieBookAuthors)
        {
            await _service.DeleteBookAuthorsAsync(modifieBookAuthors);
            return Ok();
        }

        [HttpPost]
        [Route("AddCategoriesForBook")]
        [ProducesResponseType(typeof(CategoryBook), StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> AddBookCategories(ModifieBookCategories modifieBookCategories)
        {
            await _service.AddBookCategoriesAsync(modifieBookCategories);
            return Ok();
        }

        [HttpDelete]
        [Route("DeleteCategoriesForBook")]
        [ProducesResponseType(typeof(CategoryBook), StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteBookCategories(ModifieBookCategories modifieBookCategories)
        {
            await _service.DeleteBookCategoriesAsync(modifieBookCategories);
            return Ok();
        }
    }
}
