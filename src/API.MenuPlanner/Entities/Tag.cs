using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace API.MenuPlanner.Entities
{
    public class Tag
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
    }
}
