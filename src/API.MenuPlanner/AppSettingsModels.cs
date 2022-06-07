namespace API.MenuPlanner
{
    public class AppSettingsModels
    {
        public static class SectionNames
        {
            public static string AuthenticationSettings = "Authentication";
            public static string MenuPlannerDatabase = "MenuPlannerDatabase";
        }
        public class AuthenticationSettings
        {
            public string JwtKey { get; set; } = string.Empty;
            public int JwtExpireSeconds { get; set; }
            public string JwtIssuer { get; set; } = string.Empty;
        }

        public class MenuPlannerDatabase
        {
            public string ConnectionString { get; set; } = string.Empty;
            public string DatabaseName { get; set; } = string.Empty;
        }
    }
}
