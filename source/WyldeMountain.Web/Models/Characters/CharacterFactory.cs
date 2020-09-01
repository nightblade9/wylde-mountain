using System;

namespace WyldeMountain.Web.Models.Characters
{
    internal static class CharacterFactory
    {
        // TODO: name should be an enum
        public static ICharacter GetCharacter(string name)
        {
            if (name == "Bolt Knight")
            {
                return new BoltKnightStats();
            }

            throw new ArgumentException(nameof(name));
        }
    }
}