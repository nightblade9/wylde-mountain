using WyldeMountain.Web.Controllers;
using WyldeMountain.Web.DataAccess.Repositories;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using Moq;
using NUnit.Framework;
using WyldeMountain.Web.Models.Authentication;

namespace WyldeMountain.Web.Tests.Controllers
{
    [TestFixture]
    public class UserControllerTests
    {
        [Test]
        public void WhoAmIReturnsOkWithUser()
        {
            var controller = new UserController(new Mock<IGenericRepository>().Object);
            var expectedUser = new User("Bolt Knight") { Id = ObjectId.GenerateNewId() };
            controller.CurrentUser = expectedUser;

            // Act
            var response = controller.WhoAmI().Result;

            // Assert
            Assert.That(response, Is.TypeOf(typeof(OkObjectResult)));
            var obj = ((OkObjectResult)response).Value;
            Assert.That(obj, Is.EqualTo(expectedUser));
        }

        [Test]
        public void WhoAmIReturnsBadRequestIfUserIsNotLoggedIn()
        {
            // Arrange. Use a partial mock because we have to mock CurrentUser.
            var controller = new Mock<UserController>(new Mock<IGenericRepository>().Object) { CallBase = true };
            controller.Setup(c => c.CurrentUser).Returns((User)null);

            // Act
            var response = controller.Object.WhoAmI().Result;

            // Assert
             Assert.That(response, Is.TypeOf(typeof(BadRequestResult)));
        }

        
        [Test]
        public void ResurrectReturnsBadRequestIfUserIsNotLoggedIn()
        {
            // Arrange. Use a partial mock because we have to mock CurrentUser.
            var controller = new Mock<UserController>(new Mock<IGenericRepository>().Object) { CallBase = true };
            controller.Setup(c => c.CurrentUser).Returns((User)null);

            // Act
            var response = controller.Object.Resurrect();

            // Assert
             Assert.That(response, Is.TypeOf(typeof(BadRequestResult)));
        }

        [Test]
        public void ResurrectResetsHealthToFull()
        {
            // Arrange
            var repo = new Mock<IGenericRepository>();
            var controller = new UserController(repo.Object);
            controller.CurrentUser = new User("Bolt Knight") { CurrentHealthPoints = 0 };

            // Act
            controller.Resurrect();

            // Assert
            Assert.That(controller.CurrentUser.CurrentHealthPoints, Is.EqualTo(controller.CurrentUser.MaxHealthPoints));
        }
    }
}