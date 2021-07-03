using System.Threading;
using System.Threading.Tasks;
using CrowdfindingApp.Common.Data.BusinessModels;
using CrowdfindingApp.Common.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace CrowdfindingApp.Common.Data.Interfaces
{
    public interface IDataProvider
    {
        DbSet<Category> Categories { get; set; }
        DbSet<Order> Orders { get; set; }
        DbSet<OrderAddress> OrderAddresses { get; set; }
        DbSet<Project> Projects { get; set; }
        DbSet<Question> Questions { get; set; }
        DbSet<Reward> Rewards { get; set; }
        DbSet<RewardGeography> RewardGeographies { get; set; }
        DbSet<User> Users { get; set; }
        DbSet<UserSocialNetwork> UserSocialNetworks { get; set; }
        DbSet<UserWebSite> UserWebSites { get; set; }
        DbSet<Role> Roles { get; set; }
        DbSet<City> Cities { get; set; }
        DbSet<Country> Countries { get; set; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
