using System.Reflection;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using Swashbuckle.AspNetCore.SwaggerUI;

namespace Dek.Api.Extensions;

public static class SwaggerExtension
{
    public static IServiceCollection AddApiDocs(this IServiceCollection services)
    {
        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1",
                new OpenApiInfo
                {
                    Title = "Restful API Boilerplate",
                    Version = "v1.0",
                    Description = "API Boilerplate",
                    Contact = new OpenApiContact
                    {
                        Name = "Nguyen Anh",
                        Url = new Uri("https://github.com/anhngd")
                    },
                    // License = new OpenApiLicense
                    // {
                    //     Name = "MIT",
                    //     Url = new Uri("")
                    // }
                });
            c.DescribeAllParametersInCamelCase();
            c.OrderActionsBy(x => x.RelativePath);

            var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
            if (File.Exists(xmlPath))
            {
                c.IncludeXmlComments(xmlPath);
            }

            c.OperationFilter<AppendAuthorizeToSummaryOperationFilter>();
            c.OperationFilter<SecurityRequirementsOperationFilter>();

            // To Enable authorization using Swagger (JWT)    
            c.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme()
            {
                Name = "Authorization",
                Type = SecuritySchemeType.ApiKey,
                BearerFormat = "JWT",
                In = ParameterLocation.Header,
                Description = "Enter your valid token in the text input below.\r\n\r\nExample: \"Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9\"",
            });

        });
        return services;
    }

    public static IApplicationBuilder UseApiDoc(this IApplicationBuilder app)
    {
        app.UseSwagger()
           .UseSwaggerUI(c =>
           {
               c.RoutePrefix = "api-docs";
               c.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
               c.DocExpansion(DocExpansion.List);
           });
        return app;
    }
}