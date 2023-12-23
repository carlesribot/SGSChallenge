using System.ComponentModel.DataAnnotations;
using System.Net;

using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace Search_Api.Controllers;

[ApiExplorerSettings(IgnoreApi = true)]
public sealed class ErrorsController : ControllerBase
{
    [Route("/error")]
    public IActionResult Error([FromServices] IWebHostEnvironment webHostEnvironment)
    {
        var context = HttpContext.Features.Get<IExceptionHandlerFeature>();
        var expectedException = ExceptionHandler(context?.Error, out var statusCode, out var body);

        return
            !webHostEnvironment.IsDevelopment() || expectedException
                ? Problem(title: body, statusCode: (int)statusCode)
                : Problem(title: context?.Error.Message, detail: context?.Error.StackTrace);
    }

    private static bool ExceptionHandler(Exception error, out HttpStatusCode statusCode, out string body)
    {
        switch (error)
        {
            case ValidationException validationException:
                statusCode = HttpStatusCode.BadRequest;
                body = validationException.Message;
                return true;
            default:
                statusCode = HttpStatusCode.InternalServerError;
                body = "Something bad has happened.";
                return false;
        }
    }
}