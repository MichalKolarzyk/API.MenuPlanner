using API.MenuPlanner.Models;
using API.MenuPlanner.Repositories;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace API.MenuPlanner.Services
{
    public class RecipeService
    {
        private readonly IRepository<Recipe> _recipeRepository;

        public RecipeService(IRepository<Recipe> recipeRepository)
        {
            _recipeRepository = recipeRepository;
        }
        public async Task CreateAsync(Recipe newRecipe)
        {
            await _recipeRepository.AddAsync(newRecipe);
        }
    }
}
