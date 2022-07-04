using Microsoft.AspNetCore.Mvc;

namespace Dek.Api.Commands.Demo;



public class Ping2Command
{
    public Task<IActionResult> ExecuteAsync(CancellationToken cancellationToken)
    {
        
        return Task.FromResult<IActionResult>(new OkObjectResult("pong 2"));
    }
}