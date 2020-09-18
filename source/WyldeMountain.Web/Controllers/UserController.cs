using System;
using WyldeMountain.Web.DataAccess.Repositories;
using Microsoft.AspNetCore.Mvc;
using WyldeMountain.Web.Models.Authentication;

namespace WyldeMountain.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : WyldeMountainController
    {
        public UserController(IGenericRepository genericRepository)
        : base(genericRepository)
        {
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
                return BadRequest();
            }
        }

        [HttpPatch]
        public ActionResult Resurrect()
        {
            if (this.CurrentUser != null)
            {
                this.CurrentUser.CurrentHealthPoints = this.CurrentUser.MaxHealthPoints;
                _genericRepository.Update(this.CurrentUser);

                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }
    }
}
