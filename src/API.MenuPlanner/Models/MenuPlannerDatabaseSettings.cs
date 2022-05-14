namespace API.MenuPlanner.Models
{
    public class MenuPlannerDatabaseSettings
    {
        public string ConnectionString { get; set; } = string.Empty;
        public string DatabaseName { get; set; } = string.Empty;
        public string DishesCollectionName { get; set; } = string.Empty;
        public string RecipeCollectionName { get; set; } = string.Empty;
    }
}
