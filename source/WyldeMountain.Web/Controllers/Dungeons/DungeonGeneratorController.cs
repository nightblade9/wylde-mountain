using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WyldeMountain.Web.DataAccess.Repositories;
using WyldeMountain.Web.Models.Dungeons;

namespace WyldeMountain.Web.Controllers.Dungeons
{
    [Authorize]
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

        [HttpPost]
        public ActionResult Generate()
        {
            // Generate the first floor. We don't keep any metadata on dungeons right now.
            if (this.CurrentUser.Dungeon != null)
            {
                return BadRequest(new InvalidOperationException("Player is already in a dungeon."));
            }
            
            CurrentUser.Dungeon = new Dungeon() { CurrentFloor = new Floor() };
            _genericRepo.Update(this.CurrentUser);

            return Ok();
        }
    }
}
