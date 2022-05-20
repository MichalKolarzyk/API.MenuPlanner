using API.MenuPlanner.Agregates;
using API.MenuPlanner.Models;
using API.MenuPlanner.Services;
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

        [HttpGet]
        public async Task<List<Dish>> Get()
        {
            return await _dishService.GetAsync();
        }

        [HttpGet("{id:length(24)}")]
        public async Task<ActionResult<DishAgregate>> Get(string id)
        {
            var dish = await _dishService.GetAsync(id);

            if (dish is null)
            {
                return NotFound();
            }

            return dish;
        }

        [HttpPost]
        public async Task<IActionResult> Post(Dish newDish)
        {
            await _dishService.CreateAsync(newDish);

            return CreatedAtAction(nameof(Get), new { id = newDish.Id }, newDish);
        }

        //[HttpPut("{id:length(24)}")]
        //public async Task<IActionResult> Update(string id, Dish updatedDish)
        //{
        //    var book = await _dishService.GetAsync(id);

        //    if (book is null)
        //    {
        //        return NotFound();
        //    }

        //    updatedDish.Id = book.Id;

        //    await _dishService.UpdateAsync(id, updatedDish);

        //    return NoContent();
        //}

        //[HttpDelete("{id:length(24)}")]
        //public async Task<IActionResult> Delete(string id)
        //{
        //    var book = await _dishService.GetAsync(id);

        //    if (book is null)
        //    {
        //        return NotFound();
        //    }

        //    await _dishService.RemoveAsync(id);

        //    return NoContent();
        //}
    }
}
