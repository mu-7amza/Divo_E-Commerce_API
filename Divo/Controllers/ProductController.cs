using AutoMapper;
using BLL.IRepositories;
using BLL.Repositories;
using BLL.Specifications;
using DAL.Entities;
using Divo.Errors;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PL.Divo.Dtos;

namespace PL.Divo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : BaseApiController
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
        public async Task<ActionResult<IReadOnlyList<ProductToReturnDto>>> GetProducts() 
        {
            var spec = new ProductsWithCategoriesAndBrandsSpecification();

            var products = await _prodRepo.ListAsync(spec);

            return Ok(_mapper.Map<IReadOnlyList<Product>,IReadOnlyList<ProductToReturnDto>>(products));
        }

        [Authorize(Roles = "User,Admin")]
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ProductToReturnDto>> GetProduct([FromRoute] int id)
        {
            var spec = new ProductsWithCategoriesAndBrandsSpecification(id);

            var product = await _prodRepo.GetEntityWithSpec(spec);

            if (product is null)
            {
                return NotFound(new ApiErrorResponse(404));
            }

            return  _mapper.Map<Product, ProductToReturnDto>(product);
        }

        [HttpPost()]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AddProduct(ProductToSendDto product)
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

            var productDto = _mapper.Map<ProductToSendDto,Product>(product);
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
