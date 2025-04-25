using AutoMapper;
using BLL.IRepositories;
using BLL.Repositories;
using DAL.Dtos;
using DAL.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace PL.Divo.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IGenericRepository<Product> _prodRepo;
        private readonly IGenericRepository<Category> _catRepo;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ProductController(IGenericRepository<Product> repo, IMapper mapper, IUnitOfWork unitOfWork, IGenericRepository<Category> catRepo)
        {
            _prodRepo = repo;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _catRepo = catRepo;
        }

        [HttpGet()]
        public async Task<IActionResult> GetProducts() 
        {
            var products = await _prodRepo.GetAll();
            if (products == null)
            {
                return NotFound();
            }
            return Ok(products);
        }

        [HttpPost()]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AddProduct(ProductDto product)
        {
            var category = await _catRepo.GetById(product.CategoryId);

            if (category == null)
            {
                return BadRequest(new
                {
                    Message = "Category not found"
                });
            }

            if (product == null)
            {
                return BadRequest("Product is null");
            }

            var productDto = _mapper.Map<Product>(product);
            await _prodRepo.Add(productDto);
            await _unitOfWork.SaveAsync();
            return Ok("Created Succesfully");
        }

        

    }
}
