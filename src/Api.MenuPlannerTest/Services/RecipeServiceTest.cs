using API.MenuPlanner.Entities;
using API.MenuPlanner.Repositories;
using API.MenuPlanner.Services;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace Api.MenuPlannerTest.Services
{

    public class RecipeServiceTest
    {
        readonly IRepository<Recipe> _recipeRepository;
        readonly RecipeService _recipeService;

        private Recipe _recipe1;
        private Recipe _recipe2;

        public RecipeServiceTest()
        {
            var services = TestEnvironment.GetServices();
            _recipeService = services.GetRequiredService<RecipeService>();
            _recipeRepository = services.GetRequiredService<IRepository<Recipe>>();
        }

        private async Task Initialize()
        {
            TestEnvironment.DropDatabases();
            _recipe1 = new Recipe
            {
                Description = "description recipe 1",
                Title = "title recipe 1"
            };

            _recipe2 = new Recipe
            {
                Description = "description recipe 2",
                Title = "title recipe 2"
            };

            await _recipeRepository.AddRangeAsync(new List<Recipe>
            {
                _recipe1, _recipe2
            });
        }

        [Fact]
        public async Task UpdateRecipeAsync_ShouldUpdateCorrectly()
        {
            await Initialize();
            var newRecipe = new Recipe
            {
                Id = _recipe1?.Id,
                Description = "new description",
            };
            await _recipeService.UpdateRecipeAsync(newRecipe);

            Recipe updatedRecipe = await _recipeRepository.FirstOrDefaultAsync(r => r.Id == _recipe1.Id);
            updatedRecipe.Description.Should().BeEquivalentTo(newRecipe.Description);
            updatedRecipe.Title.Should().BeEquivalentTo("");
        }

        [Fact]
        public async Task GetRecipeAsync_ShouldReturnGoodRecipe()
        {
            await Initialize();
            var recipe = await _recipeService.GetRecipeAsync(_recipe1.Id);
            recipe.Should().NotBeNull();
            recipe.Description.Should().Be(_recipe1.Description);
        }

        [Fact]
        public async Task CreateAsync_ShouldCreateRecipe()
        {
            await Initialize();
            string? id = await _recipeService.CreateAsync(new Recipe()
            {
                Description = "new Recipe",
                Title = "new Recipe",
            });
            id.Should().NotBeNull();
        }

        [Fact]
        public async Task GetRecipesAsync_ShouldGetInitializedRecipes()
        {
            await Initialize();
            var recipes = await _recipeService.GetRecipesAsync(new API.MenuPlanner.Requests.GetRecipesRequest());
            recipes.Count.Should().Be(2);
        }
    }
}
