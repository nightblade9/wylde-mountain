using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using WyldeMountain.Web.Models.Dungeons;

namespace WyldeMountain.Web.Tests.Models.Dungeons
{
    [TestFixture]
    public class FloorTests
    {
        [Test]
        public void ConstructorGeneratesEightToTenFloorAppropriateMonstersInThreeOrFourChoices()
        {
            var floor = new Floor(1, 12321);

            Assert.That(floor.Events.Count, Is.InRange(3, 4));
            Assert.That(floor.Events.Sum(list => list.Count), Is.InRange(8, 10));

            var actualMonsters = new List<string>();
            foreach (var choice in floor.Events)
            {
                actualMonsters.AddRange(choice.Select(c => (c as DungeonEvent).Data));
            }
            
            Assert.That(actualMonsters.Any(m => m == "Dirtoad"));
            Assert.That(actualMonsters.Any(m => m == "Ponderon"));
        }
    }
}