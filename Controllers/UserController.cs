using AutoMapper;
using FuelGo.Dto;
using FuelGo.Inerfaces;
using FuelGo.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace FuelGo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : BaseController
    {
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;

        public UserController(UserInfoService userInfoService, IUnitOfWork unitOfWork, 
            IMapper mapper, IConfiguration configuration) : 
            base(userInfoService, unitOfWork)
        {
            _mapper = mapper;
            _configuration = configuration;
        }

        [HttpPost("login")]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> Login(ReqLoginDto loginDto)
        {
            var user = _unitOfWork._userRepository.GetUserByPhone(loginDto.Phone);
            if (user == null || user.Password != loginDto.Password)
                return Unauthorized("Invalid Phone or Password");
            // Generate JWT token
            var token = GenerateJwtToken(user.Phone, user.Role);
            // Update the token for the user in the database
            await _unitOfWork._userRepository.UpdateTokenByPhoneAsync(user.Phone, token);
            // Return the token in the response
            return Ok(new { Token = token });
        }


        [HttpPut("edit-password")]
        [Authorize]
        public IActionResult EditPassword(ReqEditPasswordDto editeData)
        {
            var user = base.GetActiveUser()!;
            if(user.Password != editeData.OldPassword)
            {
                return BadRequest("The old password is incorrect.");
            }
            if(editeData.Password != editeData.RePassword)
            {
                return BadRequest("New password and repeated password do not match.");
            }
            user.Password = editeData.Password;
            _unitOfWork.Commit();
            return Ok("Password updated");
        }


        private string GenerateJwtToken(string username, string role)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.Now.AddDays(Convert.ToDouble(_configuration["Jwt:ExpireDays"]));

            var token = new JwtSecurityToken(
                _configuration["Jwt:Issuer"],
                _configuration["Jwt:Issuer"],
                claims: new[]
                {
                new Claim(JwtRegisteredClaimNames.Sub, username),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.Role, role)
                },
                expires: expires,
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
