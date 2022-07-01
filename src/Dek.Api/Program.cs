using Dek.Api;
using Dek.Api.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services
    .AddApiDocs()
    .AddResponseCompression()
    .AddProjectCommands()
    .AddProjectRepositories()
    .AddProjectServices()
    .AddProjectFilters();

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
