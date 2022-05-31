using API.MenuPlanner.Entities;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace API.MenuPlanner.Agregates
{
    public class DishAgregate
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }
        public Recipe? Recipe { get; set; }
    }
}
