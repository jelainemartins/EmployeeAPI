using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeAPI.Controllers
{
    /// Anotação que irá ignorar como API;
    [ApiExplorerSettings(IgnoreApi = true)]
    public class ThrowController : Controller
    {
        [Route("/error")]
        public IActionResult HandlerError() => Problem();

        [Route("/error-development")]
        public IActionResult HandlerErrorDevelopment([FromServices] IHostEnvironment hostingEnvironment)
        {
            if (!hostingEnvironment.IsDevelopment())
            {
                return NotFound();
            }
            var exceptionHandlerFeature = HttpContext.Features.Get<IExceptionHandlerFeature>()!;
            return Problem(detail: exceptionHandlerFeature.Error.StackTrace, title: exceptionHandlerFeature.Error.Message);
        }
    }
}
