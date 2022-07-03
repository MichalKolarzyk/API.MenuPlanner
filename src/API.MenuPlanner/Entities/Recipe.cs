using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace API.MenuPlanner.Entities
{
    public class Recipe
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public List<string> Steps { get; set; } = new List<string>();
        public List<string> TagIds { get; set; } = new List<string>();
        public List<string> IngreadientIds { get; set; } = new List<string>();
        
    }
}
