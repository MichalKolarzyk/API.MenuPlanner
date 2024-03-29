﻿using API.MenuPlanner.Database;
using API.MenuPlanner.Entities;
using MongoDB.Driver;
using System.Linq.Expressions;

namespace API.MenuPlanner.Repositories
{
    public class DishRepository : MongoRepositoryBase<Dish>
    {
        public DishRepository(IMongoDbContext mongoDbContext) : base(mongoDbContext, "Dishes")
        {
        }
    }
}
