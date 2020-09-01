using NUnit.Framework;
using WyldeMountain.Web.Models.Characters;

namespace WyldeMountain.Web.Tests.Models.Characters
{
    [TestFixture]
    public class BoltKnightStatsTests
    {
        [Test]
        public void ExperiencRequiredIncreasesEveryLevel()
        {
            var stats = new BoltKnightStats();
            var lastLevel = stats.ExperienceRequiredForLevel(1);
            for (var i = 2; i <= 100; i++)
            {
                var currentLevel = stats.ExperienceRequiredForLevel(i);
                Assert.That(currentLevel, Is.GreaterThan(lastLevel));
                lastLevel = currentLevel;
            }
        }

        [Test]
        public void AllStatsIncreaseOnLevelUp()
        {
            var currentLevel = 37;
            var stats = new BoltKnightStats();
            Assert.That(stats.DefenseAtLevel(currentLevel), Is.GreaterThan(stats.DefenseAtLevel(currentLevel - 1)));
            Assert.That(stats.HealthAtLevel(currentLevel), Is.GreaterThan(stats.HealthAtLevel(currentLevel - 1)));
            Assert.That(stats.SkillPointsAtLevel(currentLevel), Is.GreaterThan(stats.SkillPointsAtLevel(currentLevel - 1)));
            Assert.That(stats.SpecialAttackAtLevel(currentLevel), Is.GreaterThan(stats.SpecialAttackAtLevel(currentLevel - 1)));
            Assert.That(stats.SpecialDefenseAtLevel(currentLevel), Is.GreaterThan(stats.SpecialDefenseAtLevel(currentLevel - 1)));
            Assert.That(stats.SpeedAtLevel(currentLevel), Is.GreaterThan(stats.SpeedAtLevel(currentLevel - 1)));
            Assert.That(stats.StrengthAtLevel(currentLevel), Is.GreaterThan(stats.StrengthAtLevel(currentLevel - 1)));
        }
    }
}