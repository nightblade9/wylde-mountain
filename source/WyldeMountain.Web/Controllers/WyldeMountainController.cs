using Microsoft.AspNetCore.Mvc;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using WyldeMountain.Web.DataAccess.Repositories;
using WyldeMountain.Web.Models.Authentication;

namespace WyldeMountain.Web.Controllers
{
    public class WyldeMountainController : ControllerBase
    {
        private readonly IGenericRepository genericRepository;
        private User currentUser;

        public WyldeMountainController(IGenericRepository genericRepository)
        {
            this.genericRepository = genericRepository;
        }
        
        /// <summary>
        /// Returns the current user (from the JWT Bearer token). If there is no bearer token, or the
        /// user isn't in the database, or the token is invalid, then it returns null.
        /// </summary>
        public virtual User CurrentUser
        {
            get
            {
                if (currentUser != null)
                {
                    // Already loaded, reuse
                    return currentUser;
                }

                if (HttpContext.Request.Headers.ContainsKey("Bearer") && HttpContext.Request.Headers["Bearer"][0] != "null")
                {
                    var jwtToken = HttpContext.Request.Headers["Bearer"];
                    var jwtSecurityToken = new JwtSecurityTokenHandler().ReadJwtToken(jwtToken);
                    if (jwtSecurityToken.ValidFrom <= DateTime.UtcNow && jwtSecurityToken.ValidTo >= DateTime.UtcNow)
                    {
                        var claims = jwtSecurityToken.Claims;
                        var email = claims.Single(c => c.Type == "email").Value;
                        return genericRepository.SingleOrDefault<User>(u => u.EmailAddress.ToUpperInvariant() == email.ToUpperInvariant());
                    }
                }

                return null;
            }
            internal set
            {
                // For unit testing
                currentUser = value;
            }
        }
    }
}