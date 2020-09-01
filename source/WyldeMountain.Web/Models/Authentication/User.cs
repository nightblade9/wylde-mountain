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
    }
}