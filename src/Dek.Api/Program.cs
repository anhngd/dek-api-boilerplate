using Dek.Api;
using Dek.Api.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer()
    .AddApiDocs()
    .AddCustomRouting()
    .AddResponseCompression()
    .AddProjectCommands()
    .AddProjectRepositories()
    .AddProjectServices()
    .AddProjectFilters()
    .AddDbConnections(builder.Configuration)
    .AddAutoMapper(typeof(Program)); 
    ;

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
