using WyldeMountain.Web.Models.Dungeons;

namespace WyldeMountain.Web.Models.Authentication
{
    /// <summary>
    /// A registered user (sans credentials, which are in the Auth class).
    /// </summary>
    public class User : HasId
    {
        public string EmailAddress { get; set; }
    }
}