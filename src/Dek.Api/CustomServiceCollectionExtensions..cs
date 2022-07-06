using System.Reflection;
using System.Text;
using Dek.Api.Contexts;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace Dek.Api;

internal  static class CustomServiceCollectionExtensions
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="services"></param>
    /// <param name="configuration"></param>
    /// <returns></returns>
    public static IServiceCollection AddDbConnections(this IServiceCollection services,
        IConfiguration configuration)
    {
        var assemblyName = typeof(Program).GetTypeInfo().Assembly.GetName().Name;
        var mySqlConnectionString = configuration.GetConnectionString("MySqlDatabase");

        _ = services.AddDbContext<ApplicationDbContext>(options =>
            options.UseMySql(mySqlConnectionString, ServerVersion.AutoDetect(mySqlConnectionString),
                sqlOptions => sqlOptions.MigrationsAssembly(assemblyName)));
        
        
        // var redisConnectionString = configuration.GetConnectionString("Redis");
        return services;
    }
    
    public static IServiceCollection AddCustomRouting(this IServiceCollection services) =>
        services.AddRouting(options =>
        {
            options.LowercaseUrls = true;
        });
    
    public static IServiceCollection AddAuthentication(this IServiceCollection services,
        IConfiguration configuration)
    {
        // Adding Authentication
        _ = services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            // Adding Jwt Bearer
            .AddJwtBearer(options =>
            {
                options.SaveToken = true;
                options.RequireHttpsMetadata = false;
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("SIn25HbCrnHtQGVRmU9NYAkFbLpSl0zuc1AcBOcZ")),
                };
            });
        return services;
    }

    public static IServiceCollection AddCustomApiVersioning(this IServiceCollection services) =>
        services
            .AddApiVersioning(
                options =>
                {
                    options.AssumeDefaultVersionWhenUnspecified = true;
                    options.ReportApiVersions = true;
                    options.ApiVersionReader = ApiVersionReader.Combine(
                        new QueryStringApiVersionReader("api-version"),
                        new HeaderApiVersionReader("X-Version"),
                        new MediaTypeApiVersionReader("ver"),
                        new UrlSegmentApiVersionReader());
                });
}

