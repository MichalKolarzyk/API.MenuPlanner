namespace API.MenuPlanner.Dtos
{
    public class RecipeDto
    {
        public class Create
        {
            public string Title { get; set; } = string.Empty;
            public string Description { get; set; } = string.Empty;
            public List<string> Steps { get; set; } = new List<string>();
            public List<string> TagIds { get; set; } = new List<string>();
            public List<string> IngreadientIds { get; set; } = new List<string>();
        }
    }
}
