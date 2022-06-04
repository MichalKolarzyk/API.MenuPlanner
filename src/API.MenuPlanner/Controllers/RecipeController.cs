using API.MenuPlanner.Entities;
using API.MenuPlanner.Requests;
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

        [HttpGet("{id:length(24)}")]
        public async Task<ActionResult<Recipe>> Get(string id)
        {
            Recipe recipe = await _recipeService.GetRecipeAsync(id);
            return Ok(recipe);
        }

        [HttpPost]
        public async Task<ActionResult<List<Recipe>>> Get([FromBody] GetRecipesRequest request)
        {
            List<Recipe> recipes = await _recipeService.GetRecipesAsync(request);
            return Ok(recipes);
        }

        [HttpPost("new")]
        public async Task<ActionResult<Recipe>> Create(Recipe newRecipe)
        {
            await _recipeService.CreateAsync(newRecipe);
            return Ok(newRecipe);
        }


        [HttpPut]
        public async Task<IActionResult> Update([FromBody] Recipe updatedRecipe)
        {
            await _recipeService.UpdateRecipeAsync(updatedRecipe);
            return Ok(updatedRecipe.Id);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(string id)
        {
            await _recipeService.DeleteRecipeAsync(id);
            return NoContent();
        }
    }
}
