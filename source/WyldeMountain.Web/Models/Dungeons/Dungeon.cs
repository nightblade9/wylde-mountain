using MongoDB.Bson;

namespace WyldeMountain.Web.Models.Dungeons
{
    class Dungeon : HasId
    {
        public ObjectId UserId { get; set; }
        public Floor CurrentFloor { get; set; }
    }
}