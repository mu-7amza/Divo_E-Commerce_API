using AutoMapper;
using BLL.IRepositories;
using DAL.Dtos;
using DAL.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace PL.Divo.Controllers
{
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

        [HttpPost()]
        public async Task<IActionResult> AddCategory(CategoryDto category)
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
            return Ok();
        }
    }
}
