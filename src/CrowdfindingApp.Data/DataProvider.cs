using System;
using System.Collections.Generic;
using CrowdfindingApp.Common.Data.BusinessModels;
using CrowdfindingApp.Common.Data.Interfaces;
using CrowdfindingApp.Common.Data.Models;
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
        public DbSet<City> Cities { get; set; }
        public DbSet<Country> Countries { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Ignore<BaseModel>();

            modelBuilder.Entity<Role>().HasData(_defaultRoles);

            base.OnModelCreating(modelBuilder);
        }

        private List<Role> _defaultRoles = new List<Role>
        {
            new Role
            {
                Id = new Guid(CrowdfindingApp.Common.Immutable.Roles.DefaultUser),
                Name = nameof(CrowdfindingApp.Common.Immutable.Roles.DefaultUser),
                Permissions = string.Empty
            },
            new Role
            {
                Id = new Guid(CrowdfindingApp.Common.Immutable.Roles.Admin),
                Name = nameof(CrowdfindingApp.Common.Immutable.Roles.Admin),
                Permissions = string.Empty
            },
        };
    }
}
