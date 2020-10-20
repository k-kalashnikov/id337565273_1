using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using SP.Contract.Domains.Common;

namespace SP.Contract.Application.Extensions
{
    public static class DbContextExtension
    {
        public static async Task AddOrUpdateAsync<T>(this DbSet<T> dbSet, IEnumerable<T> records)
            where T : class, IPrimaryKeyLong
        {
            foreach (var data in records)
            {
                var exists = await dbSet.AsNoTracking().Where(x => x.Id == data.Id).FirstOrDefaultAsync();
                if (exists != null)
                {
                    try
                    {
                        dbSet.Update(data);
                    }
                    catch (System.InvalidOperationException)
                    {
                        var context = dbSet.GetService<ICurrentDbContext>().Context;
                        context.Entry(exists).CurrentValues.SetValues(data);
                    }

                    continue;
                }

                await dbSet.AddAsync(data);
            }
        }
    }
}