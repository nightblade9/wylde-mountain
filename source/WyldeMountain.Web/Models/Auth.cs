using Mongo.Migration.Documents;
using MongoDB.Bson;

namespace WyldeMountain.Web.Models
{
    /// <summary>Authentication instance (user ID, password hash, salt, etc.)</summary>
    public class Auth : HasId
    {
        public ObjectId UserId { get; set; }
        public string Salt { get; set; }
        public string HashedPassword { get; set; }
    }
}