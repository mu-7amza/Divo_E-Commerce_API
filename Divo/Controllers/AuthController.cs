using BLL.IRepositories;
using DAL.Dtos;
using DAL.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace PL.Divo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController(UserManager<ApplicationUser> userManager, ITokenRepository tokenRepository) : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager = userManager;
        private readonly ITokenRepository _tokenRepository = tokenRepository;

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register(RegisterRequestDto requestDto)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(new
                {
                    Message = "Invalid request ",
                    Errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage)
                });
            }

            var User = new ApplicationUser
            {
                Email = requestDto.Email.Trim(),
                UserName = requestDto.Email.Trim(),
            };

            var result = await _userManager.CreateAsync(User, requestDto.Password);

            if (result.Succeeded)
            {
                result = await _userManager.AddToRoleAsync(User, "User");
                if (result.Succeeded)
                {
                    return Ok(new
                    {
                        Message = "User Registered Successfully",
                    });
                }
                else
                {
                    return BadRequest(new
                    {
                        Message = "Failed to add Role to User",
                        Errors = result.Errors.Select(e => e.Description)
                    });
                }
            }
            else
            {
                return BadRequest(new
                {
                    Message = "Failed to create user",
                    Errors = result.Errors.Select(e => e.Description)
                });
            }

        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto requestDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new
                {
                    Message = "Invalid request ",
                    Errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage)
                });
            }
            var user = await _userManager.FindByEmailAsync(requestDto.Email.Trim());
            if (user == null)
            {
                return NotFound(new
                {
                    Message = "User not found",
                });
            }
            var result = await _userManager.CheckPasswordAsync(user, requestDto.Password);
            if (result)
            {
                // Generate JWT token here
                var roles = await _userManager.GetRolesAsync(user);

                var jwtToken = _tokenRepository.GenerateJwtToken(user, roles.ToList());

                var response = new LoginResponseDto
                {
                    Message = "Login Successful",
                    Email = requestDto.Email,
                    Roles = roles.ToList(),
                    Token = jwtToken
                };
                return Ok(response);
            }
            else
            {
                return Unauthorized(new
                {
                    Message = "Invalid Password",
                });
            }
        }
    }
}
