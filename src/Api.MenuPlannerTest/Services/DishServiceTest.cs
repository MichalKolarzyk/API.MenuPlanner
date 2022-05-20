using API.MenuPlanner.Agregates;
using API.MenuPlanner.Database;
using API.MenuPlanner.Models;
using API.MenuPlanner.Repositories;
using API.MenuPlanner.Services;
using FluentAssertions;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Api.MenuPlannerTest.Services
{
    public class DishServiceTest
    {
        private DishService? _dishService;
        private RepositoryTestBase<Dish> _dishRepository;
        private RepositoryTestBase<Recipe> _recipeRepository;

        private List<Dish> _dishes;
        private List<Recipe> _recipes;
        private async Task Initialize()
        {
            _dishRepository = new RepositoryTestBase<Dish>();
            _recipeRepository = new RepositoryTestBase<Recipe>();
            _dishService = new DishService(_dishRepository, _recipeRepository);

            _dishes = new List<Dish>
            {
                new Dish{Id = "dish1", RecipeId = "recipe1"},
                new Dish{Id = "dish2", RecipeId = "recipe2"},
            };

            _recipes = new List<Recipe>
            {
                new Recipe{ Id = "recipe1", Description="description recipe 1", Title="title recipe 1"},
                new Recipe{ Id = "recipe2", Description="description recipe 2", Title="title recipe 2"},
            };

            await _dishRepository.AddRangeAsync(_dishes);
            await _recipeRepository.AddRangeAsync(_recipes);
        }
        [Fact]
        public async void ShouldReturnGoodDishAgregate()
        {
            await Initialize();
            DishAgregate dishAgregate = await _dishService.GetAsync("dish1");

            dishAgregate?.Recipe.Should().NotBeNull();
            dishAgregate.Recipe.Id.Should().BeEquivalentTo("recipe1");
            dishAgregate.Recipe.Description.Should().BeEquivalentTo("description recipe 1");
        }

        [Fact]
        public async void ShouldThrowExceptionWhenIdNotFound()
        {
            await Initialize();
            Assert.Throws(typeof(AggregateException),() => _dishService.GetAsync("IdThatNotExists").Result);
        }
    }
}
