using System;
using System.Linq;
using NUnit.Framework;
using WyldeMountain.Web.Data.Monsters;
using WyldeMountain.Web.Models.Authentication;
using WyldeMountain.Web.Models.Battle;

namespace WyldeMountain.Web.Tests.Models.Battle
{
    [TestFixture]
    public class BattleResolverTests
    {
        
        [TestCase(0)]
        [TestCase(-3)]
        public void ConstructorThrowsIfPlayerHealthIsNonPositive(int health)
        {
            var player = new User("Bolt Knight") { CurrentHealthPoints = health };
            Assert.Throws<ArgumentException>(() => new BattleResolver(player, RiverWoodsMonsters.Create("Dirtoad")));
        }

        [TestCase(0)]
        [TestCase(-11)]
        public void ConstructorThrowsIfMonsterHealthIsNonPositive(int health)
        {
            var player = new User("Bolt Knight") { CurrentHealthPoints = 50 };
            var monster = RiverWoodsMonsters.Create("Dirtoad");
            monster.CurrentHealthPoints = health;
            Assert.Throws<ArgumentException>(() => new BattleResolver(player, monster));
        }
        
        [Test]
        public void ResolveEndsInVictoryAndBequeathsXp()
        {
            // Arrange
            var player = new User("Bolt Knight") { Level = 1, CurrentHealthPoints = 1000 };
            var monster = RiverWoodsMonsters.Create("Ponderon");
            monster.CurrentHealthPoints = 60;

            // Act
            var results = new BattleResolver(player, monster).Resolve();

            // Assert
            Assert.That(results.Any(r => r.ToUpperInvariant().Contains("YOU DEFEATED")));
            Assert.That(player.ExperiencePoints, Is.GreaterThan(0));
            Assert.That(player.Level, Is.EqualTo(1)); // Didn't level up
        }

        [Test]
        public void ResolveVictoryIncreasesLevelIfRequired()
        {
            // Arrange
            // Trick: user XP can be like 2-3 levels higher, will still level up appropriately
            var player = new User("Bolt Knight") { Level = 1, CurrentHealthPoints = 1000, ExperiencePoints = 9999 };
            var monster = RiverWoodsMonsters.Create("Ponderon");
            monster.CurrentHealthPoints = 1;

            // Act
            var results = new BattleResolver(player, monster).Resolve();

            // Assert
            Assert.That(player.Level, Is.GreaterThan(1));
        }

        [Test]
        public void ResolveEndsInDefeatAndDoesntIncrementXp()
        {
            var player = new User("Bolt Knight") { CurrentHealthPoints = 100 };
            var monster = RiverWoodsMonsters.Create("Dirtoad");
            monster.CurrentHealthPoints = 2000;

            var results = new BattleResolver(player, monster).Resolve();
            Assert.That(results.Last().ToUpperInvariant().Contains("YOU ARE DEAD"), results.Last());
            Assert.That(player.ExperiencePoints, Is.Zero);
        }

        [Test]
        public void ResolveEndsInStalemateWithNoXpGain()
        {
            var player = new User("Bolt Knight") { CurrentHealthPoints = 100 };
            var monster = new Monster("Clone Boi", 1, 0, player.Defense, player.Strength, 999, 999, player.Speed);

            var results = new BattleResolver(player, monster).Resolve();
            var lastMessage = results.Last().ToUpperInvariant();
            Assert.That(lastMessage.Contains("BOTH") && lastMessage.Contains("EXHAUSTED"), results.Last());
            Assert.That(player.ExperiencePoints, Is.Zero); 
        }
    }
}