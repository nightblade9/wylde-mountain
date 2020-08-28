using System;
using System.Collections.Generic;

namespace WyldeMountain.Web.Models.Dungeons
{
    public class Floor
    {
        private Random _random = new Random();
        
        public uint FloorNumber { get; set; }
        // Linked list of events. User only sees the first in each list.
        public List<List<DungeonEvent>> Events { get; set; } = new List<List<DungeonEvent>>();

        public Floor(uint floorNumber, int seed = 0)
        {
            if (seed != 0)
            {
                _random = new Random(seed);
            }
            
            this.FloorNumber = floorNumber;
            var numChoices = _random.Next(3, 5); // 3 or 4 forks
            var numEvents = _random.Next(8, 11);
            var generated = new List<DungeonEvent>();

            // Seed choices with monsters
            for (var i = 0; i < numChoices; i++)
            {
                var list = new List<DungeonEvent>();
                list.Add(this.GenerateBattle());
                this.Events.Add(list);
            }
            numEvents -= numChoices;

            // Randomly distribute the rest of the monsters
            while (numEvents-- > 0)
            {
                var list = _random.Next(numChoices);
                this.Events[list].Add(this.GenerateBattle());
            }
        }

        private DungeonEvent GenerateBattle()
        {
            var monster = _random.Next(100) <= 50 ? "Dirtoad" : "Ponderon";
            return new DungeonEvent("Battle", monster);
        }
    }
}