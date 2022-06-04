using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace API.MenuPlanner.Entities
{
    public class Ingreadinet
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public float Amount { get; set; }
    }
}
