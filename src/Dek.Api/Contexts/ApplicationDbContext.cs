using Dek.Api.Entities;

namespace Dek.Api.Contexts;

using Microsoft.EntityFrameworkCore;

public class ApplicationDbContext : DbContext
{
    private readonly IConfiguration _configuration;
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, IConfiguration configuration) : base(options)
    {
        _configuration = configuration;
    }
    public DbSet<User> Users { get; set; } = null!;
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        _ = modelBuilder.Entity<User>().ToTable("Users");
    }
}