using API.MenuPlanner.Entities;

namespace API.MenuPlanner.Aggregates
{
    public class DishAggregate
    {
        public string? Id { get; set; }
        public string UserId { get; set; } = string.Empty;
        public Dish.DishTypeEnum DishType { get; set; }
        public DateTime Day { get; set; }
        public Recipe? Recipe { get; set; }
    }
}
