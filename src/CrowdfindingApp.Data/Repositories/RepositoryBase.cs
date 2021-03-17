
using System;
using System.Linq;
using CrowdfindingApp.Data.Common.BusinessModels;
using CrowdfindingApp.Data.Common.Interfaces;

namespace CrowdfindingApp.Data.Repositories
{
    public abstract class RepositoryBase<TModel> where TModel : new()
    {
        protected IDataProvider Storage { get; }

        public RepositoryBase(IDataProvider storage)
        {
            Storage = storage ?? throw new ArgumentException(nameof(storage));
        }

        protected IQueryable<TModel> GetQuery()
        {
            if(typeof(TModel) == typeof(Category))
            {
                return (IQueryable<TModel>)Storage.Categories;
            }
            else if(typeof(TModel) == typeof(OrderAddress))
            {
                return (IQueryable<TModel>)Storage.OrderAddresses;
            }
            else if(typeof(TModel) == typeof(Order))
            {
                return (IQueryable<TModel>)Storage.Orders;
            }
            else if(typeof(TModel) == typeof(Project))
            {
                return (IQueryable<TModel>)Storage.Projects;
            }
            else if(typeof(TModel) == typeof(Question))
            {
                return (IQueryable<TModel>)Storage.Questions;
            }
            else if(typeof(TModel) == typeof(RewardGeography))
            {
                return (IQueryable<TModel>)Storage.RewardGeographies;
            }
            else if(typeof(TModel) == typeof(Reward))
            {
                return (IQueryable<TModel>)Storage.Rewards;
            }
            else if(typeof(TModel) == typeof(Role))
            {
                return (IQueryable<TModel>)Storage.Roles;
            }
            else if(typeof(TModel) == typeof(User))
            {
                return (IQueryable<TModel>)Storage.Users;
            }
            else if(typeof(TModel) == typeof(UserSocialNetwork))
            {
                return (IQueryable<TModel>)Storage.UserSocialNetworks;
            }
            else if(typeof(TModel) == typeof(UserWebSite))
            {
                return (IQueryable<TModel>)Storage.UserWebSites;
            }
            else if(typeof(TModel) == typeof(Country))
            {
                return (IQueryable<TModel>)Storage.Countries;
            }
            else if(typeof(TModel) == typeof(City))
            {
                return (IQueryable<TModel>)Storage.Cities;
            }
            else 
            {
                throw new TypeAccessException($"There is no registered type for {nameof(TModel)}");
            }
        }
    }
}
