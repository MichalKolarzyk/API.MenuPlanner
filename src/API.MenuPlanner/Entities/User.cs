using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace API.MenuPlanner.Entities
{
    public class User
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public RoleEnum Role { get; set; } = RoleEnum.Viewer;
        public string Password { get; set; } = string.Empty;
    }

    public enum RoleEnum
    {
        Viewer,
        Creator,
        Admin,
    }
}
