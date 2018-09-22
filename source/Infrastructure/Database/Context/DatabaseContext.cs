using Microsoft.EntityFrameworkCore;
using Solution.CrossCutting.Security;
using Solution.Model.Entities;
using Solution.Model.Enums;

namespace Solution.Infrastructure.Database
{
    public sealed class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions options) : base(options)
        {
        }

        private IHash Hash { get; } = new Hash();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            Configure(modelBuilder);
            Seed(modelBuilder);
        }

        private void Configure(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new UserLogEntityTypeConfiguration());
        }

        private void Seed(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserEntity>().HasData(new UserEntity
            {
                Email = "administrator@administrator.com",
                Login = Hash.Create("admin"),
                Name = "Administrator",
                Password = Hash.Create("admin"),
                Roles = Roles.User | Roles.Admin,
                Status = Status.Active,
                Surname = "Administrator",
                UserId = 1
            });
        }
    }
}
