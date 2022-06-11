using API.MenuPlanner.Dtos;
using API.MenuPlanner.Database;
using API.MenuPlanner.Entities;
using API.MenuPlanner.Repositories;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System.Linq.Expressions;
using API.MenuPlanner.Helpers;

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

        public async Task<List<DishDto.ResponseModel>> GetAsync(GetDishesRequest request)
        {
            DateTime firstDay = DateHelper.ToDateTime(request.FirstDay);
            DateTime LastDay = firstDay.AddDays(request.NumberOfDays);

            Expression<Func<Dish, bool>> predicate =
                d => request.UserIds.Contains(d.UserId)
                && d.Day >= firstDay
                && d.Day <= LastDay;

            List<Dish> dishes = await _dishRepository.FindAsync(predicate);

            List<string> recipeIds = dishes.Select(d => d.RecipeId).ToList();
            List<Recipe> recipes = await _recipeRepository.FindAsync(r => recipeIds.Contains(r.Id));

            return dishes.Select(dish => new DishDto.ResponseModel
            {
                Id = dish.Id,
                Day = dish.Day.ToShortDateString(),
                UserId = dish.UserId,
                DishType = dish.DishType.ToString(),
                RecipeId = recipes.FirstOrDefault(r => r.Id == dish.RecipeId)?.Id,
                RecipeTitle = recipes.FirstOrDefault(r => r.Id == dish.RecipeId)?.Title
            }).ToList();
        }

        public async Task<string> CreateAsync(DishDto.RequestModel newDish)
        {
            var recipe = await _recipeRepository.FirstOrDefaultAsync(x => x.Id == newDish.RecipeId);
            if (recipe == null)
                throw new Exception($"Not found recepy with id {newDish.RecipeId}");

            var dishTypeEnum = EnumHelper.Parse<Dish.DishTypeEnum>(newDish.DishType);

            DateTime day = DateHelper.ToDateTime(newDish.Day);

            Dish dish = new()
            {
                Day = day,
                DishType = dishTypeEnum,
                RecipeId = newDish.RecipeId ?? "",
                UserId = newDish.UserId ?? "",
            };

            await _dishRepository.AddAsync(dish);
            return dish.Id ?? "";
        }
    }
}
