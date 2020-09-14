using System;
using System.Collections.Generic;
using WyldeMountain.Web.Models.Authentication;

namespace WyldeMountain.Web.Models.Battle
{
    class BattleResolver
    {
        private const int MaxRounds = 100; // there's a bug if you survive but don't kill the monster in N rounds.
        private User _player;
        private Monster _monster;

        public BattleResolver(User user, Monster monster)
        {
            if (user.CurrentHealthPoints <= 0)
            {
                throw new ArgumentException(nameof(user));
            }
            if (monster.CurrentHealthPoints <= 0)
            {
                throw new ArgumentException(nameof(monster));
            }

            _player = user;
            _monster = monster;
        }

        public IEnumerable<string> Resolve()
        {
            // TODO: should probably be translation strings (keys) instead of text.

            var toReturn = new List<string>();
            toReturn.Add($"You face a(n) {_monster.Name}! It roars with fury!");

            // TODO: print out monster stats, buffs, etc. here as applicable.

            var roundsLeft = MaxRounds;
            while (roundsLeft-- > 0 && _player.CurrentHealthPoints > 0 && _monster.CurrentHealthPoints > 0)
            {
                // Naive speed resolution: 3x to 3.99x faster => 3x more attacks
                if (_player.Speed >= _monster.Speed)
                {
                    int ratio = Math.Max(_player.Speed / _monster.Speed, 1);
                    this.TakePlayerTurns(ratio, toReturn);
                }
                if (_monster.CurrentHealthPoints > 0) // might already be dead
                {
                    int ratio = Math.Max(_monster.Speed / _player.Speed, 1);
                    this.TakeMonsterTurns(ratio, toReturn);
                }
            }

            // Favour monsters if there's a tie
            if (_player.CurrentHealthPoints <= 0)
            {
                toReturn.Add("You are DEAD!");
            }
            else if (_monster.CurrentHealthPoints <= 0)
            {
                toReturn.Add($"You defeated the {_monster.Name}!");
                
                var xpGain = ExperienceCalculator.XpGainedFor(_monster);
                toReturn.Add($"You gained {xpGain} experience points.");
                _player.ExperiencePoints += xpGain;
            }
            else if (roundsLeft <= 0)
            {
                toReturn.Add($"After a fierce battle, you both drop to the floor, exhausted.");
            }

            return toReturn;
        }

        private void TakeMonsterTurns(int numTurns, List<string> messages)
        {
            if (numTurns <= 0)
            {
                throw new ArgumentException(nameof(numTurns));
            }

            // TODO: skils
            var totalDamage = numTurns * Math.Max(0, _monster.Strength - _player.Defense);
            _player.CurrentHealthPoints = Math.Max(0, _player.CurrentHealthPoints - totalDamage);
            messages.Add($"{_monster.Name} attacks {numTurns} times for {totalDamage} damage!");
        }

        private void TakePlayerTurns(int numTurns, List<string> messages)
        {
            if (numTurns <= 0)
            {
                throw new ArgumentException(nameof(numTurns));
            }

            // TODO: skils
            var totalDamage = numTurns * Math.Max(0, _player.Strength - _monster.Defense);
            _monster.CurrentHealthPoints = Math.Max(0, _monster.CurrentHealthPoints - totalDamage);
            messages.Add($"You attack the {_monster.Name} {numTurns} times for {totalDamage} damage!");
        }
    }
}