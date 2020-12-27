using CrowdfindingApp.Core.Interfaces.Data;
using CrowdfindingApp.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace CrowdfindingApp.Data
{
    public sealed class DataProvider : DbContext, IDataProvider
    {
        public DataProvider(DbContextOptions<DataProvider> options) : base(options)
        {

        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderAddress> OrderAddresses { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<Reward> Rewards { get; set; }
        public DbSet<RewardGeography> RewardGeographies { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserSocialNetwork> UserSocialNetworks { get; set; }
        public DbSet<UserWebSite> UserWebSites { get; set; }
        public DbSet<Role> Roles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Ignore<BaseModel>();
            base.OnModelCreating(modelBuilder);
        }
    }
}
