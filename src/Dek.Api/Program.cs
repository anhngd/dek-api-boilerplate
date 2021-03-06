using Dek.Api;
using Dek.Api.Contexts;
using Dek.Api.Extensions;
using Elastic.Apm.NetCoreAll;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((ctx, lc)
    => lc.WriteTo.Console()
        .WriteTo.Seq("http://192.168.1.143:5341")
    );
// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer()
    .AddApiDocs()
    .AddCustomRouting()
    .AddCustomApiVersioning()
    .AddVersionedApiExplorer(setup =>
    {
        setup.GroupNameFormat = "'v'VVV";
        setup.SubstituteApiVersionInUrl = true;
    }).AddSwaggerGen()
    .AddHttpContextAccessor()
    // Add useful interface for accessing the ActionContext outside a controller.
    .AddSingleton<IActionContextAccessor, ActionContextAccessor>()
    .AddResponseCompression()
    .AddProjectCommands()
    .AddProjectRepositories()
    .AddProjectServices()
    .AddProjectFilters()
    .AddDbConnections(builder.Configuration)
    .AddAutoMapper(typeof(Program)); 
    ;
var app = builder.Build();

app.UseAllElasticApm(builder.Configuration);


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

using (var scope = app.Services.CreateScope())
{
    var dataContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    Console.WriteLine("MIGRATE DATABASE: Starting");
    dataContext.Database.Migrate();
    Console.WriteLine("MIGRATE DATABASE: Done");
}

// app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.UseSerilogRequestLogging();



try
{
    app.LogApplicationStarted();
    app.Run();
    app.LogApplicationStopped();
}
catch(Exception ex)
{
    app!.LogApplicationTerminatedUnexpectedly(ex);
}