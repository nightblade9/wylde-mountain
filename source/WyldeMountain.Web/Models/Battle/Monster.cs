namespace WyldeMountain.Web.Models.Battle
{
    class Monster
    {
        public int CurrentHealthPoints { get; set; }
        public int CurrentSkillPoints { get; set; }
        public readonly string Name; 

        public readonly int TotalHealthPoints;
        public readonly int TotalSkillPoints;
        public readonly int Strength;
        public readonly int Defense;
        public readonly int SpecialAttack;
        public readonly int SpecialDefense;
        public readonly int Speed;

        public Monster(string name, int health, int skillPoints, int strength, int defense, int specialAttack, int specialDefense, int speed)
        {
            this.Name = name;
            this.CurrentHealthPoints = this.TotalHealthPoints = health;
            this.CurrentSkillPoints = this.TotalSkillPoints = skillPoints;
            this.Strength = strength;
            this.Defense = defense;
            this.SpecialAttack = specialAttack;
            this.SpecialDefense = specialDefense;
            this.Speed = speed;
        }
    }
}