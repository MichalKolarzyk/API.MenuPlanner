using API.MenuPlanner.Dtos;
using API.MenuPlanner.Entities;
using API.MenuPlanner.Requests;
using API.MenuPlanner.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Diagnostics;
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
        [Authorize]
        public async Task<ActionResult<Recipe>> Get(string id)
        {
            Recipe recipe = await _recipeService.GetRecipeAsync(id);
            return Ok(recipe);
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult<List<Recipe>>> Get([FromBody] GetRecipesRequest request)
        {
            List<Recipe> recipes = await _recipeService.GetRecipesAsync(request);
            return Ok(recipes);
        }

        [HttpPost("new")]
        [Authorize]
        public async Task<ActionResult<Recipe>> Create(Recipe newRecipe)
        {
            await _recipeService.CreateAsync(newRecipe);
            return Ok(newRecipe);
        }


        [HttpPut]
        [Authorize]
        public async Task<IActionResult> Update([FromBody] Recipe updatedRecipe)
        {
            await _recipeService.UpdateRecipeAsync(updatedRecipe);
            return Ok(updatedRecipe.Id);
        }

        [HttpDelete]
        [Authorize]
        public async Task<IActionResult> Delete(string id)
        {
            await _recipeService.DeleteRecipeAsync(id);
            return NoContent();
        }

        [Route("/error")]
        [ApiExplorerSettings(IgnoreApi = true)]
        public IActionResult HendleError([FromServices] IHostEnvironment hostEnvironment)
        {
            var exceptionHandlerFeature = HttpContext.Features.Get<IExceptionHandlerFeature>();

            return Problem(
                detail: exceptionHandlerFeature?.Error.Message,
                title: exceptionHandlerFeature?.Error.Source);
        }

    }
}
