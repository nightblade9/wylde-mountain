using System;
using WyldeMountain.Web.DataAccess.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WyldeMountain.Web.Models.Authentication;
using WyldeMountain.Web.Models.Dungeons;

namespace WyldeMountain.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : WyldeMountainController
    {
        private readonly ILogger<UserController> logger;
        private readonly IGenericRepository genericRepository;

        public UserController(ILogger<UserController> logger, IGenericRepository genericRepository)
        : base(genericRepository)
        {
            this.logger = logger;
            this.genericRepository = genericRepository;
        }

        /// <summary>
        /// Returns a bit of info about the currently logged-in user. 400s if the user isn't logged in.
        /// This is called pretty much in every React front-end component to get the logged-in user.
        /// </summary>
        [HttpGet]
        public ActionResult<User> WhoAmI()
        {
            if (this.CurrentUser != null)
            {
                return Ok(this.CurrentUser);
            }
            else
            {
                // shold be impossible; [Authorize] amirite?
                return BadRequest(new InvalidOperationException("User is not logged in!"));
            }
        }
    }
}
