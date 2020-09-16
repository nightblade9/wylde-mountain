using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using WyldeMountain.Web.DataAccess.Repositories;
using WyldeMountain.Web.Models.Dungeons;

namespace WyldeMountain.Web.Controllers.Dungeons
{
    [ApiController]
    [Route("/api/[controller]")]
    public class DungeonController : WyldeMountainController
    {
        public DungeonController(IGenericRepository genericRepository)
        : base(genericRepository)
        {
        }

        [HttpGet]
        public ActionResult<Dungeon> Get()
        {
            if (this.CurrentUser == null)
            {
                return BadRequest();
            }
            
            var dungeon = _genericRepository.SingleOrDefault<Dungeon>(d => d.UserId == this.CurrentUser.Id);
            if (dungeon == null)
            {
                return BadRequest();
            }

            return Ok(dungeon);
        }
    }
}
