using API.MenuPlanner;
using API.MenuPlanner.Database;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Api.MenuPlannerTest.Services
{
    [Collection("ServiceTest")]
    public abstract class BaseServiceTest
    {
        private readonly IServiceProvider _serviceProvider;
        public BaseServiceTest()
        {
            _serviceProvider = TestEnvironment.GetServices();
        }

        protected T GetService<T>() 
            where T : notnull
        {
            return _serviceProvider.GetRequiredService<T>();
        }

        protected void DropDatabases()
        {
            IMongoDbContext mongodbContext = GetService<IMongoDbContext>();
            ConfigurationModel configuration = GetService<ConfigurationModel>();
            mongodbContext.GetClient().DropDatabase(configuration.DatabaseName);
        }
    }
}
