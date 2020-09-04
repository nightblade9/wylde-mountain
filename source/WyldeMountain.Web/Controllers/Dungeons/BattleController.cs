using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using WyldeMountain.Web.Data.Monsters;
using WyldeMountain.Web.DataAccess.Repositories;
using WyldeMountain.Web.Models.Battle;
using WyldeMountain.Web.Models.Dungeons;

namespace WyldeMountain.Web.Controllers.Dungeons
{
    [ApiController]
    [Route("/api/[controller]")]
    public class BattleController : WyldeMountainController
    {
        public BattleController(IGenericRepository genericRepository)
        : base(genericRepository)
        {
        }

        [HttpGet]
        public ActionResult<IEnumerable<string>> Get(int choice)
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

            var allEvents = dungeon.CurrentFloor.Events;

            if (choice < 0 || choice >= allEvents.Count || allEvents[choice].Count == 0)
            {
                return BadRequest();
            }

            // User picked the choice, they get the first event.
            var e = allEvents[choice][0];

            if (e.EventType != "Battle")
            {
                return BadRequest(nameof(e.EventType));
            }

            var monster = RiverWoodsMonsters.Create(e.Data);
            var results = new BattleResolver(this.CurrentUser, monster).Resolve();

            // TODO: persist results

            return Ok(results);
        }
    }
}
