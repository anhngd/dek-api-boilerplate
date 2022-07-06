using Microsoft.AspNetCore.Mvc;

namespace Dek.Api.Commands.Demo;



public class PingCommand
{
    public Task<IActionResult> ExecuteAsync(CancellationToken cancellationToken)
    {
        
        return Task.FromResult<IActionResult>(new OkObjectResult("pong"));
    }
}
