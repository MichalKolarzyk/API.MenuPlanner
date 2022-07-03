namespace API.MenuPlanner
{
    public class ConfigurationModel
    {
        public string JwtKey { get; set; } = string.Empty;
        public int JwtExpireSeconds { get; set; }
        public string JwtIssuer { get; set; } = string.Empty;
        public string ConnectionString { get; set; } = string.Empty;
        public string DatabaseName { get; set; } = string.Empty;
    }
}
