using NUnit.Framework;
using WyldeMountain.Web.Models.Authentication;
using WyldeMountain.Web.Models.Characters;

namespace WyldeMountain.Web.Tests.Models
{
    [TestFixture]
    public class UserTests
    {
        [Test]
        public void CheckForAndLevelUpReturnsTrueAndLevelsUpIfXpIsSufficient()
        {
            // Arrange
            var player = new User("Bolt Knight") {
                Level = 1,
                ExperiencePoints = new BoltKnightStats().ExperienceRequiredForLevel(2)
            };

            // Act
            var expected = player.CheckForAndLevelUp();

            // Assert
            Assert.That(expected, Is.True);
            Assert.That(player.Level, Is.EqualTo(2));
        }

        [Test]
        public void CheckForAndLevelUpSkipsLevelsIfXpIsSufficient()
        {
            // Arrange
            var player = new User("Bolt Knight") {
                Level = 1,
                ExperiencePoints = new BoltKnightStats().ExperienceRequiredForLevel(4)
            };

            // Act
            var expected = player.CheckForAndLevelUp();

            // Assert
            Assert.That(expected, Is.True);
            Assert.That(player.Level, Is.EqualTo(4));
        }

        [Test]
        public void CheckForAndLevelUpReturnsFalseAndDoesntLevelUpWithTooLittleExperience()
        {
            // Arrange
            var player = new User("Bolt Knight") {
                Level = 1,
                ExperiencePoints = 7
            };

            // Act
            var expected = player.CheckForAndLevelUp();

            // Assert
            Assert.That(expected, Is.False);
            Assert.That(player.Level, Is.EqualTo(1));
        }
    }
}