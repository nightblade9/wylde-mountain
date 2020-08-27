namespace WyldeMountain.Web.Models.Dungeons.Events
{
    // Battle with one (for now) monster
    public class Battle : AbstractEvent
    {
        public string MonsterName { get; private set; } // derive stats from this

        public Battle(string monsterName)
        {
            this.MonsterName = monsterName;
        }
    }
}