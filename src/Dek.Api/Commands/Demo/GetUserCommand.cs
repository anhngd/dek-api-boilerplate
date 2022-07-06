using AutoMapper;
using Dek.Api.Entities;
using Dek.Api.Models;
using Dek.Api.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Dek.Api.Commands.Demo;

public class GetUserCommand
{
    
    private readonly IMapper _mapper;
    private readonly IUserRepository _userRepository;

    public GetUserCommand(IMapper mapper, IUserRepository userRepository)
    {
        this._mapper = mapper;
        this._userRepository = userRepository;
    }
    public async Task<IActionResult> ExecuteAsync(string id, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetAsync(new Guid(id), cancellationToken);
        var userViewModel = _mapper.Map<UserInfo>(user);

        return new OkObjectResult(userViewModel);
    }
}