using API.MenuPlanner.Entities;
using API.MenuPlanner.Services;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Api.MenuPlannerTest.Services
{

    public class RecipeServiceTest
    {
        private RepositoryTestBase<Recipe> _recipeRepository;

        RecipeService _recipeService;

        private List<Recipe> _recipes;
        private Recipe _recipe1;
        private Recipe _recipe2;

        private async Task Initialize()
        {
            _recipeRepository = new RepositoryTestBase<Recipe>();
            _recipeService = new RecipeService(_recipeRepository);

            _recipe1 = new Recipe
            {
                Id = "recipeId1",
                Description = "description recipe 1",
                Title = "title recipe 1"
            };

            _recipe2 = new Recipe
            {
                Id = "recipeId2",
                Description = "description recipe 2",
                Title = "title recipe 2"
            };

            _recipes = new List<Recipe>
            {
                _recipe1, _recipe2
            };

            await _recipeRepository.AddRangeAsync(_recipes);
        }

        [Fact]
        public async Task UpdateRecipeAsync_ShouldUpdateCorrectly()
        {
            await Initialize();
            var newRecipe = new Recipe
            {
                Id = _recipe1.Id,
                Description = "new description",
            };
            await _recipeService.UpdateRecipeAsync(newRecipe);

            Recipe updatedRecipe = await _recipeRepository.FirstOrDefaultAsync(r => r.Id == _recipe1.Id);
            updatedRecipe.Description.Should().BeEquivalentTo(newRecipe.Description);
            updatedRecipe.Title.Should().BeEquivalentTo("");
        }
    }
}
