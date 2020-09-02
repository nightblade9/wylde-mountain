using System;
using System.Linq.Expressions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MongoDB.Bson;
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
            var repository = new Mock<IGenericRepository>();
            var userId = ObjectId.GenerateNewId();
            Dungeon inserted = null;

            var controller = new DungeonGeneratorController(repository.Object);
            controller.CurrentUser = new User() { Id = userId };
            repository.Setup(r => r.Insert(It.IsAny<Dungeon>())).Callback<Dungeon>((dungeon) => inserted = dungeon);

            // Act
            var response = controller.Generate();

            // Assert
            Assert.That(response, Is.TypeOf(typeof(OkResult)));
            Assert.That(inserted, Is.Not.Null);
            Assert.That(inserted.UserId, Is.EqualTo(userId));
        }

        [Test]
        public void GenerateReturnsBadRequestIfPlayerAlreadyHasDungeonInRepository()
        {
            // Arrange
            var repository = new Mock<IGenericRepository>();
            var userId = ObjectId.GenerateNewId();
            var dungeon = new Dungeon() { UserId = userId };

            var controller = new DungeonGeneratorController(repository.Object);
            controller.CurrentUser = new User() { Id = ObjectId.GenerateNewId() };

            repository.Setup(r => r.SingleOrDefault<Dungeon>(It.IsAny<Expression<Func<Dungeon, bool>>>())).Returns(dungeon);

            // Act
            var response = controller.Generate();

            // Assert
            Assert.That(response, Is.TypeOf(typeof(BadRequestObjectResult)));
        }
    }
}