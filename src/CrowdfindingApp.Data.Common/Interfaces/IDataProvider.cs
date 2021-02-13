﻿using System.Threading;
using System.Threading.Tasks;
using CrowdfindingApp.Data.Common.Models;
using Microsoft.EntityFrameworkCore;

namespace CrowdfindingApp.Data.Common.Interfaces
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
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}