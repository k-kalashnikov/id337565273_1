using System.Collections.Generic;
using MediatR;

namespace SP.Contract.Domains.Common
{
    public interface IEntity
    {
        bool IsNew { get; set; }

        bool IsReadOnly { get; }

        IReadOnlyCollection<INotification> DomainEvents { get; }

        void AddDomainEvent(INotification eventItem);

        void RemoveDomainEvent(INotification eventItem);

        void ClearDomainEvents();
    }
}
