using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace API.MenuPlanner.Entities
{
    public class Dish
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }
        public string UserId { get; set; } = string.Empty;
        public DishTypeEnum DishType { get; set; }
        public DateTime Day { get; set; }

        [BsonRepresentation(BsonType.ObjectId)]
        public string RecipeId { get; set; } = string.Empty;


        public enum DishTypeEnum
        {
            starter,
            breakfast,
            lunch,
            snack,
            dinner,
            supper,
        }
    }
}
