using API.MenuPlanner.Entities;

namespace API.MenuPlanner.Dtos
{
    public class RecipeDto
    {
        public string? Id { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public List<string> Steps { get; set; } = new List<string>();
        public List<Tag> TagIds { get; set; } = new List<Tag>();
    }
}
