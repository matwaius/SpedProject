using dotnet_api.Models.Entities;
using Microsoft.EntityFrameworkCore;
namespace dotnet_api.Context
{

    public class dotnet_apiContext : DbContext
    {
        public dotnet_apiContext(DbContextOptions<dotnet_apiContext> options) : base(options)
        { }
        public DbSet<Users> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
        }
    }
}
