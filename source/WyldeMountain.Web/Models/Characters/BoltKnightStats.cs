namespace WyldeMountain.Web.Models.Characters
{
    class BoltKnightStats : ICharacter
    {
        private const int baseHealth = 100;
        private const int baseSkillPoints = 40;
        private const int baseStrength = 15;
        private const int baseDefense = 10;
        const int baseSpecialAttack = 10;
        const int baseSpecialDefense = 7;
        private const int baseSpeed = 10;

        public int ExperienceRequiredForLevel(int level)
        {
            // 23n^3 + 13n^2 + 3n + 100
            // Total: 139, 342, 847, 1792, ...
            // Brain broke. Amonut required doesn't double every level. Roughly 3M XP for level 50.
            return (23 * level * level * level) + (13 * level * level) + (3 * level) + 100;
        }

        public int HealthAtLevel(int level)
        {
            return baseHealth + (level * 20);
        }

        public int SkillPointsAtLevel(int level)
        {
            return baseSkillPoints + (level * 10);
        }

        public int StrengthAtLevel(int level)
        {
            return baseStrength + (level * 5);
        }

        public int DefenseAtLevel(int level)
        {
            return baseDefense + (level * 3);
        }
        public int SpecialAttackAtLevel(int level)
        {
            return baseSpecialAttack + (level * 4);
        }

        public int SpecialDefenseAtLevel(int level)
        {
            return baseSpecialDefense + level;
        }

        public int SpeedAtLevel(int level)
        {
            return baseSpeed + (level * 3);
        }
    }
}