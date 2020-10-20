using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using SP.Contract.Domains.Common;

namespace SP.Contract.Application.Common.Extensions
{
    public static class MediatorExtension
    {
        public static async Task DispatchDomainEventsAsync(
            this IMediator mediator, IEnumerable<EntityEntry<IEntity>> entityEntries)
        {
            var enumerable = entityEntries.ToList();

            var domainEvents = enumerable
                .SelectMany(x => x.Entity.DomainEvents)
                .ToList();

            foreach (var domainEvent in domainEvents)
            {
                await mediator.Publish(domainEvent);
            }

            enumerable
                .ForEach(entity => entity.Entity.ClearDomainEvents());
        }
    }
}
