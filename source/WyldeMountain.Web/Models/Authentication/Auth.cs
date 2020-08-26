using Mongo.Migration.Documents;
using MongoDB.Bson;

namespace WyldeMountain.Web.Models.Authentication
{
    /// <summary>Authentication instance (user ID, password hash, salt, etc.)</summary>
    public class Auth : HasId
    {
        public ObjectId UserId { get; set; }
        public string HashedPasswordWithSalt { get; set; }
    }
}