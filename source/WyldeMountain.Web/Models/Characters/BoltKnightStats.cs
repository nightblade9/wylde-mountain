namespace WyldeMountain.Web.Models.Characters
{
    class BoltKnightStats : ICharacter
    {
        private const int baseHealth = 50;
        private const int baseSkillPoints = 20;
        private const int baseStrength = 7;
        private const int baseDefense = 5;
        const int baseSpecialAttack = 5;
        const int baseSpecialDefense = 3;
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
            return baseHealth + (level * 10);
        }

        public int SkillPointsAtLevel(int level)
        {
            return baseSkillPoints + (level * 5);
        }

        public int StrengthAtLevel(int level)
        {
            return baseStrength + (int)(level * 2.5);
        }

        public int DefenseAtLevel(int level)
        {
            return baseDefense + (int)(level * 1.5);
        }
        public int SpecialAttackAtLevel(int level)
        {
            return baseSpecialAttack + (level * 2);
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