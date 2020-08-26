using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using WyldeMountain.Web.Controllers.Dungeons;
using WyldeMountain.Web.DataAccess.Repositories;
using WyldeMountain.Web.Models.Authentication;
using WyldeMountain.Web.Models.Dungeons;

namespace WyldeMountain.Web.Tests.Controllers.Dungeons
{
    [TestFixture]
    public class DungeonGeneratorControllerTests
    {
        [Test]
        public void GenerateReturnsOkAndGeneratesDungeonIfTereIsNoExistingDungeon()
        {
            // Arrange
            var controller = new DungeonGeneratorController(new Mock<ILogger<DungeonGeneratorController>>().Object, new Mock<IGenericRepository>().Object);
            controller.CurrentUser = new Models.Authentication.User(); // no dungeon

            // Act
            var response = controller.Generate();

            // Assert
            Assert.That(response, Is.TypeOf(typeof(OkResult)));
            Assert.That(controller.CurrentUser.Dungeon, Is.Not.Null);
        }

        [Test]
        public void GenerateReturnsBadRequestIfPlayerAlreadyHasDungeonInRepository()
        {
            // Arrange
            var controller = new DungeonGeneratorController(new Mock<ILogger<DungeonGeneratorController>>().Object, new Mock<IGenericRepository>().Object);
            controller.CurrentUser = new User() { Dungeon = new Dungeon() };

            // Act
            var response = controller.Generate();

            // Assert
            Assert.That(response, Is.TypeOf(typeof(BadRequestObjectResult)));
        }
    }
}