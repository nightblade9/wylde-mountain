using System;
using System.Linq.Expressions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using WyldeMountain.Web.Controllers.Dungeons;
using WyldeMountain.Web.DataAccess.Repositories;
using WyldeMountain.Web.Models.Authentication;
using WyldeMountain.Web.Models.Dungeons;

namespace WyldeMountain.Web.Tests.Controllers.Dungeons
{
    [TestFixture]
    public class DungeonControllerTests
    {
        [Test]
        public void GetReturnsAllEvents()
        {
            // Arrange
            var repository = new Mock<IGenericRepository>();
            
            var controller = new DungeonController(repository.Object);
            controller.CurrentUser = new User();
            var dungeon = new Dungeon() { CurrentFloor = new Floor(1) };
            repository.Setup(r => r.SingleOrDefault<Dungeon>(It.IsAny<Expression<Func<Dungeon, bool>>>())).Returns(dungeon);
            
            // Act
            var response = controller.Get();

            // Assert
            Assert.That(response, Is.TypeOf(typeof(ActionResult<Dungeon>)));
            var actualDungeon = ((ObjectResult)response.Result).Value as Dungeon;
            Assert.That(actualDungeon, Is.Not.Null);
            Assert.That(actualDungeon, Is.EqualTo(dungeon));
        }

        [Test]
        public void GetReturnsBadRequestIfDungeonIsNull()
        {
            // Arrange
            var repository = new Mock<IGenericRepository>();
            var controller = new DungeonController(repository.Object);
            controller.CurrentUser = new User();

            // Act
            var response = controller.Get().Result;

            Assert.That(response, Is.TypeOf(typeof(BadRequestResult)));
        }
    }
}