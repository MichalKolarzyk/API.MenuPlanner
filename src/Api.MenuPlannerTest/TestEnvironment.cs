using API.MenuPlanner;
using API.MenuPlanner.Database;
using API.MenuPlanner.Entities;
using API.MenuPlanner.Helpers;
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

        private readonly static ConfigurationModel _configuration = GetConfiguration();

        static TestEnvironment()
        {
            var builder = WebApplication.CreateBuilder();
            builder.Services.AddSingleton(_configuration);
            builder.Services.AddSingleton<RecipeService>();
            builder.Services.AddSingleton<IRepository<Recipe>, RecipeRepository>();
            builder.Services.AddSingleton<IMongoDbContext, MongoDbContext>();

            _webApplication = builder.Build();
        }

        public static IServiceProvider GetServices()
        {
            return _webApplication.Services;
        }

        private static ConfigurationModel GetConfiguration()
        {
            return new ConfigurationModel()
            {
                DatabaseName = "MenuPlannerTest",
                ConnectionString = Environment.GetEnvironmentVariable(EnvironmentVariableHelper.MENU_PLANNER_CONNECTION_STRING) ?? "mongodb://localhost:27017",
                JwtExpireSeconds = 1000000,
                JwtIssuer = "http://menuplannerapi.com",
                JwtKey = "PRIVATE_KEY_DONT_SHARE",
            };
        }
    }
}
