using API.MenuPlanner;
using API.MenuPlanner.Database;
using API.MenuPlanner.Entities;
using API.MenuPlanner.Repositories;
using API.MenuPlanner.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;

namespace Api.MenuPlannerTest
{
    public static class TestEnvironment
    {
        readonly static WebApplication _webApplication;

        private readonly static IOptions<AppSettingsModels.MenuPlannerDatabase> _databaseOptions = GetDatabaseOptions();

        static TestEnvironment()
        {
            var builder = WebApplication.CreateBuilder();
            builder.Services.AddSingleton(_databaseOptions);
            builder.Services.AddSingleton<RecipeService>();
            builder.Services.AddSingleton<IRepository<Recipe>, RecipeRepository>();
            builder.Services.AddSingleton<IMongoDbContext, MongoDbContext>();

            _webApplication = builder.Build();
        }

        public static IServiceProvider GetServices()
        {
            return _webApplication.Services;
        }

        public static void DropDatabases()
        {
            IMongoDbContext mongodbContext = GetServices().GetRequiredService<IMongoDbContext>();
            mongodbContext.GetClient().DropDatabase(_databaseOptions.Value.DatabaseName);
        }


        private static IOptions<AppSettingsModels.MenuPlannerDatabase> GetDatabaseOptions()
        {
            var connectionString = TestEnvironmentVariables.GetEnvironmentVariableValue(TestEnvironmentVariables.Variable.MenuPlannerConnectionString);

            return Options.Create(new AppSettingsModels.MenuPlannerDatabase()
            {
                DatabaseName = "MenuPlanner",
                ConnectionString = connectionString,
            });
        }
    }
}
