using AutoMapper;
using Dek.Api.Entities;
using Dek.Api.Models;
using Dek.Api.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Dek.Api.Commands.Demo;

public class CreateUserCommand
{
    
    private readonly IMapper _mapper;
    private readonly IUserRepository _userRepository;

    public CreateUserCommand(IMapper mapper, IUserRepository userRepository)
    {
        this._mapper = mapper;
        this._userRepository = userRepository;
    }
    public async Task<IActionResult> ExecuteAsync(CreateUserRequest request, CancellationToken cancellationToken)
    {
        var user = _mapper.Map<User>(request);
        _ = await _userRepository.AddAsync(user, cancellationToken);
        var userViewModel = _mapper.Map<UserInfo>(user);

        return new OkObjectResult(userViewModel);
    }
}