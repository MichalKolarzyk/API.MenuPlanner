namespace API.MenuPlanner.Requests
{
    public class GetRecipesRequest
    {
        public int? Take { get; set; }
        public int? Skip { get; set; }
        public string? SortBy { get; set; }
        public List<string>? TagIds { get; set; }
    }
}
