using AutoMapper;
using BLL.IRepositories;
using DAL.Dtos;
using DAL.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PL.Divo.Dtos;

namespace PL.Divo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BrandController : ControllerBase
    {
        private readonly IGenericRepository<Brand> _repo;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public BrandController(IGenericRepository<Brand> repo, IMapper mapper, IUnitOfWork unitOfWork)
        {
            _repo = repo;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        [HttpGet()]
        [Authorize(Roles = "User,Admin")]
        public async Task<IActionResult> GetBrands()
        {
            var brands = await _repo.GetAll();
            if (brands == null)
            {
                return NotFound("no brands found");
            }
            return Ok(brands);
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "User,Admin")]
        public async Task<IActionResult> GetBrands([FromRoute] int id)
        {
            var brandFromDb = await _repo.GetById(id);
            if (brandFromDb == null)
            {
                return NotFound("brand is not found");
            }
            var brandResponse = _mapper.Map<BrandDto>(brandFromDb);
            return Ok(brandResponse);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost()]
        public async Task<IActionResult> AddBrand(BrandDto brandDto)
        {
            if (brandDto == null)
            {
                return BadRequest(new
                {
                    Message = "brand is null or may be invalid"
                });
            }

            var brand = _mapper.Map<Brand>(brandDto);
            await _repo.Add(brand);
            await _unitOfWork.SaveAsync();
            return Ok("Brand Added !");
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBrand([FromRoute] int Id)
        {
            var brand = await _repo.GetById(Id);

            if (brand == null)
            {
                return BadRequest(new
                {
                    Message = "brand is not found or may be deleted"
                });
            }

            _repo.Delete(brand);
            return Ok("brand Deleted !");
        }
    }
}
