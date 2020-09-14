using System;
using System.Collections.Generic;
using System.Linq;
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
    public class BattleControllerTests
    {
        [Test]
        public void GetThrowsIfDungeonIsNull()
        {
            // Arrange
            var repository = new Mock<IGenericRepository>();
            var controller = new BattleController(repository.Object);
            controller.CurrentUser = new User();

            // Act
            var response = controller.Get(0).Result;

            Assert.That(response, Is.TypeOf(typeof(BadRequestResult)));
        }

        [TestCase(-1)]
        [TestCase(7)]
        [TestCase(888)]
        public void GetThrowsIfChoiceIsInvalid(int choice)
        {
            // Arrange
            var repository = new Mock<IGenericRepository>();
            var controller = new BattleController(repository.Object);
            controller.CurrentUser = new User();
            var dungeon = new Dungeon() { CurrentFloor = new Floor(3, 1211) };
            repository.Setup(r => r.SingleOrDefault<Dungeon>(It.IsAny<Expression<Func<Dungeon, bool>>>())).Returns(dungeon);

            // Act
            var response = controller.Get(choice).Result;

            Assert.That(response, Is.TypeOf(typeof(BadRequestResult)));
        }

        [TestCase("Choice")]
        [TestCase("Ice-Cream Truck")]
        [TestCase("Horde Invasion")]
        public void GetThrowsIfEventIsNotMonster(string invalidType)
        {
            // Arrange
            var repository = new Mock<IGenericRepository>();
            var controller = new BattleController(repository.Object);
            controller.CurrentUser = new User();
            var dungeon = new Dungeon() { CurrentFloor = new Floor(3, 1211) };
            dungeon.CurrentFloor.Events[0] = new System.Collections.Generic.List<DungeonEvent>() { new DungeonEvent(invalidType, "fake data") };
            repository.Setup(r => r.SingleOrDefault<Dungeon>(It.IsAny<Expression<Func<Dungeon, bool>>>())).Returns(dungeon);

            // Act
            var response = controller.Get(0).Result;

            Assert.That(response, Is.TypeOf(typeof(BadRequestObjectResult)));
            var result = response as BadRequestObjectResult;
            Assert.That(result.Value, Is.EqualTo("EventType"));
        }
        
        [Test]
        public void GetReturnsResultsAndUpdatesUser()
        {
            // Arrange
            var repository = new Mock<IGenericRepository>();
            var controller = new BattleController(repository.Object);
            var user = new User("Bolt Knight") { CurrentHealthPoints = 100 };
            controller.CurrentUser = user;
            var dungeon = new Dungeon() { CurrentFloor = new Floor(1, 1221) };
            bool updatedUser = false;

            repository.Setup(r => r.SingleOrDefault<Dungeon>(It.IsAny<Expression<Func<Dungeon, bool>>>())).Returns(dungeon);
            repository.Setup(r => r.Update<User>(controller.CurrentUser)).Callback(() => updatedUser = true);

            // Act. Attack a Ponderon.
            var ponderon = dungeon.CurrentFloor.Events.First(e => e[0].EventType == "Battle" && e[0].Data == "Ponderon");
            var index = dungeon.CurrentFloor.Events.IndexOf(ponderon);
            var response = controller.Get(index).Result;

            Assert.That(response, Is.TypeOf(typeof(OkObjectResult)));
            var result = response as OkObjectResult;
            var messages = result.Value as IEnumerable<string>;
            Assert.That(messages.Any());
            Assert.That(updatedUser);
        }
    }
}