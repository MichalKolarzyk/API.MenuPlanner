using API.MenuPlanner.Database;
using API.MenuPlanner.Entities;

namespace API.MenuPlanner.Repositories
{
    public class UserRepository : MongoRepositoryBase<User>
    {
        public UserRepository(IMongoDbContext mongoDbContext) : base(mongoDbContext, "Users")
        {
        }
    }
}
