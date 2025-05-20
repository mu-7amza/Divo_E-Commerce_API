using AutoMapper;
using BLL.IRepositories;
using DAL.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PL.Divo.Dtos;

namespace PL.Divo.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly IGenericRepository<Category> _repo;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public CategoryController(IGenericRepository<Category> repo, IMapper mapper, IUnitOfWork unitOfWork)
        {
            _repo = repo;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        [HttpGet()]
        [Authorize(Roles = "User,Admin")]
        public async Task<IActionResult> GetCategories()
        {
            var categories = await _repo.GetAll();
            if (categories == null)
            {
                return NotFound();
            }
            return Ok(categories);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost()]
        public async Task<IActionResult> AddCategory([FromBody]CategoryDto category)
        {
            if (category == null)
            {
                return BadRequest(new
                {
                    Message = "Category is null"
                });
            }

            var catDto = _mapper.Map<Category>(category);
            await _repo.Add(catDto);
            await _unitOfWork.SaveAsync();
            return Ok("Category Added !");
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory([FromRoute]int Id)
        {
            var category = await _repo.GetById(Id);

            if (category == null)
            {
                return BadRequest(new
                {
                    Message = "Category is not found or may be deleted"
                });
            }

             _repo.Delete(category);
            return Ok("Category Deleted !");
        }
    }
}
