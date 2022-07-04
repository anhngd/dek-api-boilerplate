using Dek.Api.Repositories;

namespace Dek.Api;

internal static class ProjectServiceCollectionExtensions
{
    public static IServiceCollection AddProjectCommands(this IServiceCollection services) =>
        services
            .AddScoped<Commands.Demo.PingCommand>()
            .AddScoped<Commands.Demo.Ping2Command>()
            .AddScoped<Commands.Demo.CreateUserCommand>()
        ;
    
    public static IServiceCollection AddProjectRepositories(this IServiceCollection services) =>
        services.AddScoped<IUserRepository, UserRepository>()
    ;


    public static IServiceCollection AddProjectServices(this IServiceCollection services) =>
        services
        ;

    public static IServiceCollection AddProjectFilters(this IServiceCollection services) =>
       services;
}