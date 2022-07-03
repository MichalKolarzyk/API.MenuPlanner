using API.MenuPlanner.Helpers;
using Microsoft.Extensions.Options;

namespace API.MenuPlanner.Extensions
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddConfiguration(this IServiceCollection services, out ConfigurationModel configuration)
        {
            configuration = new ConfigurationModel
            {
                ConnectionString = Environment.GetEnvironmentVariable(EnvironmentVariableHelper.MENU_PLANNER_CONNECTION_STRING) ?? "mongodb://localhost:27017",
                DatabaseName = "MenuPlanner",
                JwtExpireSeconds = 10000000,
                JwtIssuer = "http://menuplannerapi.com",
                JwtKey = Environment.GetEnvironmentVariable(EnvironmentVariableHelper.MENU_PLANNER_PRIVATE_KEY) ?? "PRIVATE_KEY_DONT_SHARE",
            };

            return services.AddSingleton(configuration);
        }
    }
}
