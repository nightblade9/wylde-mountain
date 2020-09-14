using NUnit.Framework;
using WyldeMountain.Web.Data.Monsters;
using WyldeMountain.Web.Models.Authentication;
using WyldeMountain.Web.Models.Battle;
using WyldeMountain.Web.Models.Characters;

namespace WyldeMountain.Web.Tests.Models.Battle
{
    [TestFixture]
    public class ExperienceCalculatorTests
    {
        [Test]
        public void XpGainedForReturnsSameXpForSameMonster()
        {
            var xp1 = ExperienceCalculator.XpGainedFor(RiverWoodsMonsters.Create("Dirtoad"));
            var xp2 = ExperienceCalculator.XpGainedFor(RiverWoodsMonsters.Create("Dirtoad"));
            Assert.That(xp1, Is.EqualTo(xp2));
            Assert.That(xp1, Is.GreaterThan(0));
        } 

        // Balancing: killing most enemies on 1F should level us up
        [Test]
        public void DefeatingMostOfFirstFloorOfRiverWoodLevelsUpBoltKnight()
        {
            var player = new User("Bolt Knight");
            var xp1 = ExperienceCalculator.XpGainedFor(RiverWoodsMonsters.Create("Ponderon"));
            var xp2 = ExperienceCalculator.XpGainedFor(RiverWoodsMonsters.Create("Dirtoad"));
            var totalXpGain = (3 * xp1) + (3 * xp2);
            var stats = new BoltKnightStats();
            
            Assert.That(xp1, Is.GreaterThan(0));
            Assert.That(xp2, Is.GreaterThan(0));
            Assert.That(totalXpGain, Is.GreaterThanOrEqualTo(stats.ExperienceRequiredForLevel(2)));
        }
    }
}