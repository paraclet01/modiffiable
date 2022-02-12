using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;


namespace OPTICIP.API.Controllers
{
    public class ErrorController : Controller
    {
        //[Route("/error")]
        //public IActionResult Error([FromServices] IHostEnvironment webHostEnvironment)
        //{
        //    if (webHostEnvironment.EnvironmentName != "Production")
        //    {
        //        throw new InvalidOperationException("This shouldn't be invoked in non-production environments.");
        //    }

        //    var context = HttpContext.Features.Get<IExceptionHandlerFeature>();
        //    return Problem(detail: context.Error.StackTrace, title: context.Error.Message);
        //}
    }
}
