using Microsoft.EntityFrameworkCore;
using MT.Innovation.Shared.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Extensions
{
    public static class IQueryableExtensions
    {
        public static async Task<PagedList<T>> ToPagedListAsync<T>(this IQueryable<T> dbQuery, int pageNumber = 1, int pageSize = -1, bool isCountOnly = false, CancellationToken cancellationToken = default) where T : class
        {
            if (dbQuery == null)
            {
                throw new ArgumentNullException(nameof(dbQuery));
            }

            var count = await dbQuery.CountAsync(cancellationToken: cancellationToken);
            dbQuery = pageSize < 0 ? dbQuery : dbQuery.Page(pageNumber, pageSize);
            var data = isCountOnly ? new List<T>() : await dbQuery.ToListAsync(cancellationToken: cancellationToken);

            return new PagedList<T>(data, count, pageNumber, pageSize);
        }
        public static IQueryable<TSource> Page<TSource>(this IQueryable<TSource> source, int pageNumber = 1, int pageSize = 10)
        {
            if (pageNumber < 1)
            {
                pageNumber = 1;
            }

            if (pageSize <= 0)
            {
                pageSize = 10;
            }

            return source.Skip((pageNumber - 1) * pageSize).Take(pageSize);
        }

    }
}
