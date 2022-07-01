namespace Dek.Api;

internal static class ProjectServiceCollectionExtensions
{
    public static IServiceCollection AddProjectCommands(this IServiceCollection services) =>
        services
            .AddScoped<Commands.Demo.PingCommand>()
        ;
    
    public static IServiceCollection AddProjectRepositories(this IServiceCollection services) =>
        services
        ;


    public static IServiceCollection AddProjectServices(this IServiceCollection services) =>
        services
        ;

    public static IServiceCollection AddProjectFilters(this IServiceCollection services) =>
       services;
}