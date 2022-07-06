using AutoMapper;
using Dek.Api.Entities;
using Dek.Api.Models;

namespace Dek.Api.MapperProfiles;

public class UserMapperProfile : Profile
{
    public UserMapperProfile()
    {
        CreateMap(typeof(User), typeof(UserInfo));
        CreateMap(typeof(CreateUserRequest), typeof(User));
    }
}