using Boxed.AspNetCore;
using Dek.Api.Commands.Demo;
using Dek.Api.Constants;
using Dek.Api.Models;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Dek.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[ApiVersion(ApiVersionName.V1)]
[SwaggerResponse(
    StatusCodes.Status500InternalServerError,
    "The MIME type in the Accept HTTP header is not acceptable.",
    typeof(ProblemDetails),
    ContentType.ProblemJson)]
public class UserController : ControllerBase
{
    private static readonly string[] Summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    private readonly ILogger<UserController> _logger;

    public UserController(ILogger<UserController> logger)
    {
        _logger = logger;
    }

    [HttpPost("signup", Name = DemoControllerRoute.CreateUser)]
    [SwaggerResponse(StatusCodes.Status200OK, "")]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "The input is invalid.", typeof(ProblemDetails))]
    [SwaggerResponse(StatusCodes.Status406NotAcceptable,
        "The MIME type in the Accept HTTP header is not acceptable.", typeof(ProblemDetails))]
    [SwaggerResponse(StatusCodes.Status415UnsupportedMediaType,
        "The MIME type in the Content-Type HTTP header is unsupported.", typeof(ProblemDetails))]
    public Task<IActionResult> CreateUserAsync(
        [FromServices] CreateUserCommand command,
        [FromBody] CreateUserRequest request,
        CancellationToken cancellationToken) => command.ExecuteAsync(request, cancellationToken);
    
    [HttpGet("{id}", Name = DemoControllerRoute.GetUser)]
    [SwaggerResponse(StatusCodes.Status200OK, "")]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "The input is invalid.", typeof(ProblemDetails))]
    [SwaggerResponse(StatusCodes.Status406NotAcceptable,
        "The MIME type in the Accept HTTP header is not acceptable.", typeof(ProblemDetails))]
    [SwaggerResponse(StatusCodes.Status415UnsupportedMediaType,
        "The MIME type in the Content-Type HTTP header is unsupported.", typeof(ProblemDetails))]
    public Task<IActionResult> GetUserAsync(
        [FromServices] GetUserCommand command,
        [FromRoute] string id,
        CancellationToken cancellationToken) => command.ExecuteAsync(id, cancellationToken);
}