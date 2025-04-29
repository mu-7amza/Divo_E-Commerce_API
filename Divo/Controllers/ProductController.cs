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
        [Authorize(Roles = "User,Admin")]
        public async Task<IActionResult> GetProducts() 
        {
            var products = await _prodRepo.GetAll(includeProperties:false);
            if (products == null)
            {
                return NotFound();
            }
            return Ok(products);
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "User,Admin")]
        public async Task<IActionResult> GetProduct([FromRoute] int id)
        {
            var product = await _prodRepo.GetById(id);
            if (product is null)
            {
                return NotFound(new
                {
                    Message = "Product Not Found"
                });
            }
            return Ok(product);
        }

        [HttpPost()]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AddProduct(ProductDto product)
        {
            if (product == null)
            {
                return BadRequest("Product is null");
            }

            var category = await _catRepo.GetById(product.CategoryId);

            if (category == null)
            {
                return BadRequest(new
                {
                    Message = "Category not found"
                });
            }

            var productDto = _mapper.Map<Product>(product);
            await _prodRepo.Add(productDto);
            await _unitOfWork.SaveAsync();
            return Ok("Created Succesfully");
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct([FromRoute] int Id)
        {
            var product = await _prodRepo.GetById(Id);

            if (product == null)
            {
                return BadRequest(new
                {
                    Message = "Product is not found or may be deleted"
                });
            }

            _prodRepo.Update(product);
            return Ok("Product Updated !");
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct([FromRoute] int Id)
        {
            var product = await _prodRepo.GetById(Id);

            if (product == null)
            {
                return BadRequest(new
                {
                    Message = "Product is not found or may be deleted"
                });
            }

            _prodRepo.Delete(product);
            return Ok("Prodcut Deleted !");
        }



    }
}
