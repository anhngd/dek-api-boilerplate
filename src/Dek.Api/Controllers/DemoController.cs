using Boxed.AspNetCore;
using Dek.Api.Commands.Demo;
using Dek.Api.Constants;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Dek.Api.Controllers;

[Route("api/v{version:apiVersion}/[controller]")]
[ApiController]
[ApiVersion(ApiVersionName.V1)]
[SwaggerResponse(
    StatusCodes.Status500InternalServerError,
    "The MIME type in the Accept HTTP header is not acceptable.",
    typeof(ProblemDetails),
    ContentType.ProblemJson)]
public class DemoController : ControllerBase
{
    private static readonly string[] Summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    private readonly ILogger<DemoController> _logger;

    public DemoController(ILogger<DemoController> logger)
    {
        _logger = logger;
    }

    [HttpPost("/ping", Name = DemoControllerRoute.Ping)]
    [MapToApiVersion(ApiVersionName.V1)]
    [SwaggerResponse(StatusCodes.Status200OK, "")]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "The input is invalid.", typeof(ProblemDetails))]
    [SwaggerResponse(StatusCodes.Status406NotAcceptable,
        "The MIME type in the Accept HTTP header is not acceptable.", typeof(ProblemDetails))]
    [SwaggerResponse(StatusCodes.Status415UnsupportedMediaType,
        "The MIME type in the Content-Type HTTP header is unsupported.", typeof(ProblemDetails))]
    public Task<IActionResult> PingAsync(
        [FromServices] PingCommand command,
        CancellationToken cancellationToken) => command.ExecuteAsync(cancellationToken);
    
    [MapToApiVersion(ApiVersionName.V2)]
    [HttpPost("/ping2", Name = DemoControllerRoute.Ping + "2")]
    [SwaggerResponse(StatusCodes.Status200OK, "")]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "The input is invalid.", typeof(ProblemDetails))]
    [SwaggerResponse(StatusCodes.Status406NotAcceptable,
        "The MIME type in the Accept HTTP header is not acceptable.", typeof(ProblemDetails))]
    [SwaggerResponse(StatusCodes.Status415UnsupportedMediaType,
        "The MIME type in the Content-Type HTTP header is unsupported.", typeof(ProblemDetails))]
    public Task<IActionResult> Ping2Async(
        [FromServices] Ping2Command command,
        CancellationToken cancellationToken) => command.ExecuteAsync(cancellationToken);
}
