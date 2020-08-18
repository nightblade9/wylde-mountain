using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WyldeMountain.Web.DataAccess.Repositories;
using WyldeMountain.Web.Http;
using WyldeMountain.Web.Models.Dungeons;

namespace WyldeMountain.Web.Controllers.Dungeons
{
    [ApiController]
    [Route("/api/[controller]")]
    public class DungeonGeneratorController : WyldeMountainController
    {
        private readonly ILogger<DungeonGeneratorController> _logger;
        private readonly IGenericRepository _genericRepo;

        public DungeonGeneratorController(ILogger<DungeonGeneratorController> logger, IGenericRepository genericRepository)
        : base(genericRepository)
        {
            _logger = logger;
            _genericRepo = genericRepository;
        }

        [AjaxOnly]
        [HttpPost]
        public ActionResult Generate()
        {
            // Generate the first floor. We don't keep any metadata on dungeons right now.
            var existingDungeon = _genericRepo.SingleOrDefault<Dungeon>(d => d.UserId == this.CurrentUser.Id);
            if (existingDungeon != null)
            {
                return BadRequest(new InvalidOperationException("Player is already in a dungeon."));
            }
            
            var dungeon = new Dungeon() { UserId = this.CurrentUser.Id, CurrentFloor = new Floor() };
            _genericRepo.Insert(dungeon);

            return Ok();
        }
    }
}
