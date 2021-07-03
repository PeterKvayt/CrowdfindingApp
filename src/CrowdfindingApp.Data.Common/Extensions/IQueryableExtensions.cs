using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CrowdfindingApp.Common.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace CrowdfindingApp.Common.Data.Extensions
{
    public static class IQueryableExtensions
    {
        private static IQueryable<T> ApplyPaging<T>(this IQueryable<T> query, Paging paging = null)
        {
            if(paging == null)
            {
                return query;
            }

            if(paging.PageNumber <= 0)
            {
                paging.PageNumber = 1;
            }

            if(paging.PageSize <= 0)
            {
                paging.PageSize = 12;
            }

            query = query.Skip(paging.PageSize * (paging.PageNumber - 1))
                .Take(paging.PageSize);

            return query;
        }

        public static async Task<List<T>> ToPagedListAsync<T>(this IQueryable<T> query, Paging paging)
        {
            if(paging != null)
            {
                paging.TotalCount = await query.CountAsync();
                query = ApplyPaging(query, paging);
            }

            return await query.ToListAsync();
        }
    }
}
