using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace SP.Contract.Application.Common.Interfaces
{
    public interface IApplicationDbContext
    {
        DbSet<T> Set<T>()
            where T : class;

        DbContext AppDbContext { get; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);

        DbSet<Domains.AggregatesModel.Misc.Entities.Account> Accounts { get; set; }

        DbSet<Domains.AggregatesModel.Misc.Entities.Organization> Organizations { get; set; }
    }
}
