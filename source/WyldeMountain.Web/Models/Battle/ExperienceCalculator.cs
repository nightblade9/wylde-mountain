using System;
using System.Collections.Generic;

namespace WyldeMountain.Web.Models.Battle
{
    internal static class ExperienceCalculator
    {
        private const int DivideXpMagicNumber = 30;

         // use formula but hand-modify some values
        private static IDictionary<Type, int> modifiers = new Dictionary<Type, int>();

        internal static int XpGainedFor(Monster monster)
        {
            var toReturn = CalculateFor(monster);

            if (modifiers.ContainsKey(monster.GetType()))
            {
                toReturn += modifiers[monster.GetType()];
            }

            return toReturn;
        }

        private static int CalculateFor(Monster monster)
        {
            // - More XP for more damage done (strength * speed)
            // - More XP for longer battles (defense * HP)
            // - Account for skill (sattack * sdef * sp)
            // - Divide by Magic Number based on: level 2 reqiures 340XP, killing sufficient (not all) enemies = level up
            return (
                    (monster.Strength * monster.Speed) + 
                    (monster.Defense * monster.TotalHealthPoints) + 
                    (monster.SpecialAttack * monster.SpecialDefense * monster.TotalSkillPoints)
            ) / DivideXpMagicNumber;
        }
    }
}