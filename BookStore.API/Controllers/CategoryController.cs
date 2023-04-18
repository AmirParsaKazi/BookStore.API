using AutoMapper;
using BookStore.Busines.Configuration;
using BookStore.Common.Dtos.Author;
using BookStore.Common.Dtos.Category;
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
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _service;

        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public CategoryController(ICategoryService service, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _service = service;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }


        [HttpGet]
        [Route("GetCategoryById/{Id}")]
        [ProducesResponseType(typeof(Category), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetCategoryById(string Id)
        {
            var category = await _service.GetCategoryByIdAsync(Id);
            return category == null ? NotFound() : Ok(category);
        }

        [HttpGet]
        [Route("Categories")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetCategories()
        {
            var Categories = await _service.GetCategoriesAsync();
            if (Categories.IsNullOrEmpty())
            {
                return NoContent();
            }
            return Ok(Categories);
        }

        [HttpPost]
        [Route("CreateCategory")]
        [ProducesResponseType(typeof(Category), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateCategory(CategoryCreate categoryCreate)
        {
            var categoryId = await _service.CreatCategoryAsync(categoryCreate);

            return categoryId != null ? Ok(categoryId) : NotFound();
        }


        [HttpDelete]
        [Route("DeleteCategory")]
        [ProducesResponseType(typeof(Category), StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteCategory(CategoryDelete categoryDelete)
        {
            await _service.DeleteCategoryAsync(categoryDelete);
            return Ok();
        }

        [HttpPut]
        [Route("EditCategory")]
        [ProducesResponseType(typeof(Category), StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> EditCategory(CategoryUpdate categoryUpdate)
        {
            await _service.UpdateCategoryAsync(categoryUpdate);
            return Ok();
        }

        [HttpPost]
        [Route("AddBooksForCategory")]
        [ProducesResponseType(typeof(CategoryBook), StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> AddCategoryBooks(ModifieCategoryBooks modifieCategoryBooks)
        {
            await _service.AddCategoryBooksAsync(modifieCategoryBooks);
            return Ok();
        }

        [HttpDelete]
        [Route("DeleteBooksFromCategory")]
        [ProducesResponseType(typeof(CategoryBook), StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteCategoryBooks(ModifieCategoryBooks modifieCategoryBooks)
        {
            await _service.DeleteCategoryBooks(modifieCategoryBooks);
            return Ok();
        }


    }
}
