using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Posterr.Domain.Models;

namespace Posterr.InfraData.Context
{
    public class DataContext : DbContext
    {
        private readonly IHostEnvironment _env;

        public DataContext(DbContextOptions<DataContext> options, IHostEnvironment env) : base(options)
        {
            _env = env;
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<UserFollower> UserFollower { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(DataContext).Assembly);
            base.OnModelCreating(modelBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (optionsBuilder.IsConfigured) return;

            var config = new ConfigurationBuilder()
                .SetBasePath(_env.ContentRootPath)
                .AddJsonFile("appsettings.json")
                .Build();

#if DEBUG
            optionsBuilder.EnableSensitiveDataLogging();
#endif

            optionsBuilder.UseSqlServer(config.GetConnectionString("SqlConnection"), sqlOption =>
            {
                sqlOption.EnableRetryOnFailure(
                    maxRetryCount: 5,
                    maxRetryDelay: System.TimeSpan.FromSeconds(10),
                    errorNumbersToAdd: null);
            });
        }
    }
}
