using API.MenuPlanner.Helpers;
using Microsoft.Extensions.Options;

namespace API.MenuPlanner.Extensions
{
    public static class ServiceCollectionExtension
    {
        public static void AddConfiguration(this IServiceCollection services, AppSettingsModels.MenuPlannerDatabase defaultSettings)
        {
            IOptions<AppSettingsModels.MenuPlannerDatabase> configuration;

            string? connectionString = Environment.GetEnvironmentVariable(EnvironmentVariableHelper.MENU_PLANNER_CONNECTION_STRING);
            if (!string.IsNullOrEmpty(connectionString))
            {
                configuration = Options.Create(new AppSettingsModels.MenuPlannerDatabase
                {
                    ConnectionString = connectionString,
                    DatabaseName = defaultSettings.DatabaseName
                }); ;
            }
            else
            {
                configuration = Options.Create(defaultSettings);
            }
            services.AddSingleton<IOptions<AppSettingsModels.MenuPlannerDatabase>>(configuration);
        }
    }
}
