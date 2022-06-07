using API.MenuPlanner.Entities;

namespace API.MenuPlanner.Agregates
{
    public class DishDto
    {
        public string? Id { get; set; }
        public Recipe? Recipe { get; set; }
    }
}
