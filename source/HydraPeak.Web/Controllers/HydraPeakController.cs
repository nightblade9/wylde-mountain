using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using HydraPeak.Web.DataAccess.Repositories;
using HydraPeak.Web.Models;
using Microsoft.AspNetCore.Mvc;

namespace HydraPeak.Web.Controllers
{
    public class HydraPeakController : ControllerBase
    {
        private readonly IGenericRepository genericRepository;

        public HydraPeakController(IGenericRepository genericRepository)
        {
            this.genericRepository = genericRepository;
        }
        
        /// <summary>
        /// Returns the current user (from the JWT Bearer token). If there is no bearer token, or the
        /// user isn't in the database, or the token is invalid, then it returns null.
        /// </summary>
        public virtual User CurrentUser { get { 
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
        } }
    }
}