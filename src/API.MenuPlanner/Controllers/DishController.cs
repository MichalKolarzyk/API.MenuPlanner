using API.MenuPlanner.Aggregates;
using API.MenuPlanner.Dtos;
using API.MenuPlanner.Entities;
using API.MenuPlanner.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.MenuPlanner.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DishController : ControllerBase
    {
        private readonly DishService _dishService;

        public DishController(DishService dishService)
        {
            _dishService = dishService;
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult<List<DishDto.DishProjectionModel>>> Get(GetDishesRequest request)
        {
            return await _dishService.GetAsync(request);
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create(DishDto.RequestModel newDish)
        {
            string id =await _dishService.CreateAsync(newDish);

            return CreatedAtAction(nameof(Get), new { id = id });
        }
    }
}
