using API.MenuPlanner.Models;
using API.MenuPlanner.Services;
using Microsoft.AspNetCore.Mvc;

namespace API.MenuPlanner.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RecipeController : ControllerBase
    {
        private readonly RecipeService _recipeService;
        public RecipeController(RecipeService recipeService)
        {
            _recipeService = recipeService;
        }

        [HttpPost]
        public async Task<IActionResult> Post(Recipe newRecipe)
        {
            await _recipeService.CreateAsync(newRecipe);

            //return CreatedAtAction(nameof(Get), new { id = newDish.Id }, newDish);
            return Ok(newRecipe);
        }
    }
}
