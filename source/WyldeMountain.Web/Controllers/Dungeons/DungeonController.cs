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

            // Return just what the user can see (first item in each event list). You tricksy little hobbits.
            var visibleEvents = dungeon.CurrentFloor.Events.Select(s => new List<DungeonEvent>(new DungeonEvent[] { s.FirstOrDefault() }));
            dungeon.CurrentFloor.Events = visibleEvents.ToList();

            return Ok(dungeon);
        }
    }
}
