using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameManagement.Entities.ReponseType
{
    public static class PagedExtensions
    {
        public static async Task<PagedList<TSource>> ToPagedList<TSource>(this IQueryable<TSource> source, int pageNumber, int pageSize)
        {
            var count = source.Count();
            var items = await source
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize).ToListAsync();
            return new PagedList<TSource>(items, count, pageNumber, pageSize);
        }
    }
}