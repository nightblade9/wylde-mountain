using MongoDB.Bson.Serialization.Attributes;

namespace WyldeMountain.Web.Models.Dungeons
{
    public class DungeonEvent
    {
        public string EventType { get; set; } // TODO: enum?
        public string Data { get; set; } // Can have tokens, whatever you like

        public DungeonEvent(string type, string data)
        {
            this.EventType = type;
            this.Data = data;
        }
    }
}