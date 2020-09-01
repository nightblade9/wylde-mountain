namespace WyldeMountain.Web.Models.Characters
{
    interface ICharacter
    {
        int ExperienceRequiredForLevel(int level);
        int HealthAtLevel(int level);
        int SkillPointsAtLevel(int level);
        int StrengthAtLevel(int level);
        int DefenseAtLevel(int level);
        int SpecialAttackAtLevel(int level);
        int SpecialDefenseAtLevel(int level);
        int SpeedAtLevel(int level);
    }
}