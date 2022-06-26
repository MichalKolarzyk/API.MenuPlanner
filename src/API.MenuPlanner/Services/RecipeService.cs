using API.MenuPlanner.Entities;
using API.MenuPlanner.Repositories;
using API.MenuPlanner.Requests;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System.Linq.Expressions;

namespace API.MenuPlanner.Services
{
    public class RecipeService
    {
        private readonly IRepository<Recipe> _recipeRepository;

        private readonly Dictionary<string, Expression<Func<Recipe, object>>> _sortKeySelectors = new Dictionary<string, Expression<Func<Recipe, object>>>
        {
            {nameof(Recipe.Title), r => r.Title },
            {nameof(Recipe.Description), r => r.Description },
        };

        public RecipeService(IRepository<Recipe> recipeRepository)
        {
            _recipeRepository = recipeRepository;
        }
        public async Task<string?> CreateAsync(Recipe newRecipe)
        {
            await _recipeRepository.AddAsync(newRecipe);
            return newRecipe.Id;
        }

        public async Task<Recipe> GetRecipeAsync(string id)
        {
            Recipe recipe = await _recipeRepository.FirstOrDefaultAsync(r => r.Id == id);
            return recipe;
        }

        public async Task<List<Recipe>> GetRecipesAsync(GetRecipesRequest request)
        {
            Expression<Func<Recipe, bool>>? expression = null;
            Expression<Func<Recipe, object>>? sortBySelector = null;

            if (request.TagIds != null && request.TagIds.Any())
                expression = r => request.TagIds.All(t => r.TagIds.Contains(t));

            if (request.SortBy != null)
                sortBySelector = _sortKeySelectors[request.SortBy];


            List<Recipe> recipes = await _recipeRepository.FindAsync(expression, request?.Skip, request?.Take, sortBySelector);
            return recipes;
        }

        public async Task UpdateRecipeAsync(Recipe recipe)
        {
            await _recipeRepository.ReplaceOneAsync(x => x.Id == recipe.Id, recipe);
        }

        public async Task DeleteRecipeAsync(string id)
        {
            await _recipeRepository.DeleteOneAsync(x => x.Id == id);
        }

    }
}
