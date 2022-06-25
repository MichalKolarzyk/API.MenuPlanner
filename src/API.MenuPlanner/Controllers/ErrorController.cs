using API.MenuPlanner.Services;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace API.MenuPlanner.Controllers
{
    [ApiController]
    public class ErrorController : ControllerBase
    {
        readonly ILogger<ErrorController> _logger;
        readonly ErrorService _errorService;
        public ErrorController(ILogger<ErrorController> logger, ErrorService errorService)
        {
            _logger = logger;
            _errorService = errorService;
        }

        [Route("/error")]
        [ApiExplorerSettings(IgnoreApi = true)]
        public IActionResult HendleError([FromServices] IHostEnvironment hostEnvironment)
        {
            var exceptionHandlerFeature = HttpContext.Features.Get<IExceptionHandlerFeature>();

            var exceptionData = _errorService.GetExceptionResponseData(exceptionHandlerFeature?.Error);

            _logger.Log(exceptionData.LogLevel, exceptionHandlerFeature?.Error.Message);

            return Problem(
                detail: exceptionHandlerFeature?.Error.Message,
                title: exceptionHandlerFeature?.Error.Source,
                statusCode: exceptionData.StatusCode);
        }
    }
}
