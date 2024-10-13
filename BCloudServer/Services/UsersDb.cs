using BCloudServer.Services.Entities;
using BCloudServer.Services.Entities.Configurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer;
using Microsoft.Extensions.Configuration;

namespace BCloudServer.Services;

public sealed class UsersDb: DbContext
{
    
    private readonly IConfiguration _configuration;
    
    public UsersDb(IConfiguration configuration)
    {
        _configuration = configuration;
        Database.EnsureCreated();
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfiguration(new UserConfiguration());
    }
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(_configuration["ServerConnectionString"]!);
    }
    
    public DbSet<User> Users { get; set; }
}