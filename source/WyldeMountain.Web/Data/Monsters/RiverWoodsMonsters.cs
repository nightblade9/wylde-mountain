using System;
using WyldeMountain.Web.Models.Battle;

namespace WyldeMountain.Web.Data.Monsters
{
    internal static class RiverWoodsMonsters
    {
        public static class  Dirtoad // tank
        {
            public const int Health = 100;
            public const int SkillPoints = 10;
            public const int Strength = 30;
            public const int Defense = 15;
            public const int SpecialAttack = 10;
            public const int SpecialDefense = 10;
            public const int Speed = 2;
        }

        public static class  Ponderon // balanced
        {
            public const int Health = 60;
            public const int SkillPoints = 6;
            public const int Strength = 20;
            public const int Defense = 7;
            public const int SpecialAttack = 14;
            public const int SpecialDefense = 7;
            public const int Speed = 5;
        }

        public static Monster Create(string monsterName)
        {
            if (monsterName == "Dirtoad")
            {
                return new Monster(typeof(Dirtoad).Name, Dirtoad.Health, Dirtoad.SkillPoints, Dirtoad.Strength, Dirtoad.Defense, Dirtoad.SpecialAttack, Dirtoad.SpecialDefense, Dirtoad.Speed);
            }
            else if (monsterName == "Ponderon")
            {
                return new Monster(typeof(Ponderon).Name, Ponderon.Health, Ponderon.SkillPoints, Ponderon.Strength, Ponderon.Defense, Ponderon.SpecialAttack, Ponderon.SpecialDefense, Ponderon.Speed);
            }

            throw new ArgumentException($"Not sure how to make a(n) {monsterName}");
        }
    }
}