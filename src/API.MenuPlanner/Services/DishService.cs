using API.MenuPlanner.Dtos;
using API.MenuPlanner.Database;
using API.MenuPlanner.Entities;
using API.MenuPlanner.Repositories;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System.Linq.Expressions;
using API.MenuPlanner.Helpers;
using API.MenuPlanner.Responses;
using API.MenuPlanner.Aggregates;

namespace API.MenuPlanner.Services
{
    public class DishService
    {
        private readonly IRepository<Dish> _dishRepository;
        private readonly IRepository<Recipe> _recipeRepository;
        private readonly IRepositoryRead<DishAggregate> _dishAggregateRepository;

        public DishService(IRepository<Dish> dishRepository, IRepository<Recipe> recipeRepository, IRepositoryRead<DishAggregate> dishAggregateRepository)
        {
            _dishRepository = dishRepository;
            _recipeRepository = recipeRepository;
            _dishAggregateRepository = dishAggregateRepository;
        }

        public async Task<List<DishDto.DishProjectionModel>> GetAsync(GetDishesRequest request)
        {
            DateTime firstDay = DateHelper.ToDateTime(request.FirstDay);
            DateTime LastDay = firstDay.AddDays(request.NumberOfDays);

            Expression<Func<DishAggregate, bool>> predicate =
                d => request.UserIds.Contains(d.UserId)
                && d.Day >= firstDay
                && d.Day <= LastDay;

            List<DishAggregate> dishes = await _dishAggregateRepository.FindAsync(predicate);

            return dishes.Select(dish => new DishDto.DishProjectionModel
            {
                Id = dish.Id,
                Day = dish.Day.ToShortDateString(),
                UserId = dish.UserId,
                DishType = dish.DishType.ToString(),
                RecipeId = dish.Recipe?.Id,
                RecipeTitle = dish.Recipe?.Title,
            }).ToList();
        }

        public async Task<string> CreateAsync(DishDto.RequestModel newDish)
        {
            var recipe = await _recipeRepository.FirstOrDefaultAsync(x => x.Id == newDish.RecipeId);
            if (recipe == null)
                throw new ExceptionResponse.NotFoundException($"Not found recepy with id {newDish.RecipeId}");

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
