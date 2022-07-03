using API.MenuPlanner.Entities;

namespace API.MenuPlanner.Dtos
{
    public class DishDto
    {
        public class RequestModel
        {
            public string? UserId { get; set; }
            public string? DishType { get; set; }
            public string? Day { get; set; }
            public string? RecipeId { get; set; }
        }

        public class DishProjectionModel
        {
            public string? Id { get; set; }
            public string? UserId { get; set; }
            public string? DishType { get; set; }
            public string? Day { get; set; }
            public string? RecipeId { get; set; }
            public string? RecipeTitle { get; set; }
        }
    }
    public class GetDishesRequest
    {
        public string? FirstDay { get; set; }
        public int NumberOfDays { get; set; }
        public List<string>? UserIds { get; set; }
    }

}
