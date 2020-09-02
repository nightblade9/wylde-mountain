using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WyldeMountain.Web.DataAccess.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using WyldeMountain.Web.Models.Authentication;

namespace WyldeMountain.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LoginController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<LoginController> _logger;
        private readonly IGenericRepository _genericRepository;

        public LoginController(ILogger<LoginController> logger, IConfiguration configuration, IGenericRepository genericRepository)
        {
            _logger = logger;
            _configuration = configuration;
            _genericRepository = genericRepository;
        }

        /// <summary>Log in.</summary>
        [HttpPost]
        public ActionResult<string> Login(LoginRequest request)
        {
            var emailAddress = request.EmailAddress;
            var plainTextPassword = request.Password;

            var user = this._genericRepository.SingleOrDefault<User>(u => u.EmailAddress == emailAddress);
            
            if (user == null)
            {
                return Unauthorized(new ArgumentException(nameof(emailAddress)));
            }

            var userCredentials = this._genericRepository.SingleOrDefault<Auth>(a => a.UserId == user.Id);
            if (userCredentials == null)
            {
                _logger.LogError($"Missing Auth record for {emailAddress} (id={user.Id})");
                return StatusCode(500);
            }

            var hash = userCredentials.HashedPasswordWithSalt;
            if (!BCrypt.Net.BCrypt.Verify(plainTextPassword, hash))
            {
                _logger.LogInformation($"Login failed: incorrect password for {emailAddress}");
                return Unauthorized(new ArgumentException(nameof(emailAddress)));
            }

            return Ok(new {
                token = GenerateJsonWebToken(user),
            });
        }
        
        // https://www.c-sharpcorner.com/article/jwt-json-web-token-authentication-in-asp-net-core/
        private string GenerateJsonWebToken(User user)  
        {  
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(this._configuration["Jwt:Key"]));  
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);  
  
            var claims = new[] {  
                new Claim(JwtRegisteredClaimNames.Sub, user.EmailAddress),  
                new Claim(JwtRegisteredClaimNames.Email, user.EmailAddress),  
                //new Claim("DateOfJoing", user.DateOfJoing.ToString("yyyy-MM-dd")),  
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };  

            var token = new JwtSecurityToken(this._configuration["Jwt:Issuer"],  
              this._configuration["Jwt:Issuer"],  
              claims,  
              expires: DateTime.Now.AddMinutes(120),  
              signingCredentials: credentials);  
  
            return new JwtSecurityTokenHandler().WriteToken(token);  
        }

        public class LoginRequest
        {
            public string EmailAddress { get; set; }
            public string Password { get; set; }
        }
    }
}
