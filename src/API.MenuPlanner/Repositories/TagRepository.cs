using API.MenuPlanner.Database;
using API.MenuPlanner.Entities;

namespace API.MenuPlanner.Repositories
{
    public class TagRepository : MongoRepositoryBase<Tag>
    {
        public TagRepository(IMongoDbContext mongoDbContext) : base(mongoDbContext, "Tags")
        {
        }
    }
}
