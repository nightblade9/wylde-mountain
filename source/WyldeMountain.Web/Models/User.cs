using Mongo.Migration.Documents;

namespace WyldeMountain.Web.Models
{
    /// <summary>
    /// A registered user (sans credentials, which are in the Auth class).
    /// </summary>
    public class User : HasId
    {
        public string EmailAddress { get; set; }
    }
}