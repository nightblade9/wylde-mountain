using WyldeMountain.Web.Models.Characters;

namespace WyldeMountain.Web.Models.Authentication
{
    /// <summary>
    /// A registered user (sans credentials, which are in the Auth class).
    /// </summary>
    public class User : HasId
    {
        public string EmailAddress { get; set; }
        
        // TODO: should be enum
        public string Character { get; set; } = "Bolt Knight";
        public int ExperiencePoints { get; set; } = 0;
        public int CurrentHealthPoints { get; set; }
        public int CurrentSkillPoints { get; set; }
        public int Level { get; set; }
        private ICharacter Stats
        {
            get
            {
                if (_stats == null)
                {
                    _stats = CharacterFactory.GetCharacter(this.Character);
                }

                return _stats;
            }
        }

        private ICharacter _stats;
        

        // Testing only
        internal User()
        {
        }

        public User(string character)
        {
            this.Character = character;
        }

        public int MaxHealthPoints => this.Stats.HealthAtLevel(this.Level);
        public int MaxSkillPoints => this.Stats.SkillPointsAtLevel(this.Level);

        public int Strength => this.Stats.StrengthAtLevel(this.Level);
        public int Defense => this.Stats.DefenseAtLevel(this.Level);
        public int SpecialAttack => this.Stats.SpecialAttackAtLevel(this.Level);
        public int SpecialDefense => this.Stats.SpecialDefenseAtLevel(this.Level);
        public int Speed => this.Stats.SpeedAtLevel(this.Level);

        internal void HealToMax()
        {
            this.CurrentHealthPoints = this.MaxHealthPoints;
            this.CurrentSkillPoints = this.MaxSkillPoints;
        }
    }
}