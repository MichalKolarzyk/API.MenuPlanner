using API.MenuPlanner.Agregates;
using API.MenuPlanner.Database;
using API.MenuPlanner.Entities;
using API.MenuPlanner.Repositories;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace API.MenuPlanner.Services
{
    public class DishService
    {
        private readonly IRepository<Dish> _dishRepository;
        private readonly IRepository<Recipe> _recipeRepository;

        public DishService(IRepository<Dish> dishRepository, IRepository<Recipe> recipeRepository)
        {
            _dishRepository = dishRepository;
            _recipeRepository = recipeRepository;
        }

        public async Task<List<Dish>> GetAsync()
        {
            return await _dishRepository.FindAsync(_ => true);
        }
        public async Task<DishDto?> GetAsync(string id)
        {
            DishDto dishAgregate = new DishDto();
            Dish dish = await _dishRepository.FirstOrDefaultAsync(x => x.Id == id);
            if (dish == null)
                throw new Exception($"Not found dish with id {id}");

            dishAgregate.Id = id;
            dishAgregate.Recipe = await _recipeRepository.FirstOrDefaultAsync(x => x.Id == dish.RecipeId);

            return dishAgregate;

        }

        public async Task CreateAsync(Dish newDish)
        {
            var recipe = await _recipeRepository.FirstOrDefaultAsync(x => x.Id == newDish.RecipeId);
            if (recipe == null)
                throw new Exception($"Not found recepy with id {newDish.RecipeId}");

            await _dishRepository.AddAsync(newDish);
        }

        //public async Task UpdateAsync(string id, Dish updatedDish) =>
        //    await _dishRepository.ReplaceOneAsync(x => x.Id == id, updatedDish);

        //public async Task RemoveAsync(string id) =>
        //    await _dishRepository.DeleteOneAsync(x => x.Id == id);
    }
}
