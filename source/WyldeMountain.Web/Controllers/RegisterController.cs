using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WyldeMountain.Web.DataAccess.Repositories;
using WyldeMountain.Web.Models.Authentication;

namespace WyldeMountain.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RegisterController : ControllerBase
    {
        private const string DefaultCharacter = "Bolt Knight";
        private readonly ILogger<RegisterController> logger;
        private readonly IGenericRepository genericRepository;

        public RegisterController(ILogger<RegisterController> logger, IGenericRepository genericRepository)
        {
            this.logger = logger;
            this.genericRepository = genericRepository;
        }
        
        /// <summary>
        /// Registers a new user. Returns the user's ID if successful.
        /// </summary>
        [HttpPost]
        public ActionResult<User> Register(RegistrationRequest request)
        {
            // TODO: validate email address
            // TODO: validate password is sufficiently long

            var emailAddress = request.EmailAddress;
            var plainTextPassword = request.Password;

            var newUser = new User(DefaultCharacter) { EmailAddress = emailAddress };
            newUser.Level = 1;
            newUser.HealToMax();

            var existingUser = this.genericRepository.SingleOrDefault<User>(u => u.EmailAddress == emailAddress);
            if (existingUser != null)
            {
                return BadRequest(new ArgumentException(nameof(emailAddress)));
            }

            this.genericRepository.Insert<User>(newUser);
            newUser = this.genericRepository.SingleOrDefault<User>(u => u.EmailAddress == emailAddress); // Load back with ID

            // As of writing, BCrypt.Net-Next relies on BlowFish, which has no cryptoanalysis attacks, but isn't recommended.
            // Schneier and others recommend TwoFish; doesn't seem to be a way to use it in this package, though.
            // You may want to swap out for a different package or something.
            var hash = BCrypt.Net.BCrypt.HashPassword(plainTextPassword); // Includes salt
            var auth = new Auth() { UserId = newUser.Id, HashedPasswordWithSalt = hash };
            this.genericRepository.Insert<Auth>(auth);

            return Ok(newUser);
        }

        public class RegistrationRequest
        {
            public string EmailAddress { get; set; }
            public string Password { get; set; }
        }
    }
}
