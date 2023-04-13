using BuberDinner.Application.Common.Errors;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BuberDinner.Api.Controllers
{

    public class ErrorController : ControllerBase
    {
        [Route("/error")]
        [ApiExplorerSettings(IgnoreApi = true)]
        public IActionResult Error()
        {
            Exception? exception = HttpContext.Features.Get<IExceptionHandlerFeature>()?.Error;

            var (statusCode, message) = exception switch
            {
                IExceptionService exceptionService => ((int)exceptionService.StatusCode, exceptionService.Message),
                _ => (StatusCodes.Status500InternalServerError, "An unexpected error occurred."),
            };
            return Problem(statusCode: statusCode, title: message);

        }
    }
}
