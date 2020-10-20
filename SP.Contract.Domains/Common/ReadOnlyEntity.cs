using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using MediatR;
using Newtonsoft.Json;
using SP.Contract.Domains.AggregatesModel.Misc.Entities;

namespace SP.Contract.Domains.Common
{
    public abstract class ReadOnlyEntity : IEntity
    {
        private long _createdById;

        public virtual Guid Id { get; protected set; }

        [NotMapped]
        public virtual bool IsNew { get; set; }

        [NotMapped]
        public virtual bool IsReadOnly { get; } = true;

        public Account CreatedBy { get; set; }

        public DateTime Created { get; set; }

        private List<INotification> _domainEvents;

        [NotMapped]
        [JsonIgnore]
        public IReadOnlyCollection<INotification> DomainEvents
            => _domainEvents?.AsReadOnly();

        public void AddDomainEvent(INotification eventItem)
        {
            _domainEvents ??= new List<INotification>();
            _domainEvents.Add(eventItem);
        }

        public void RemoveDomainEvent(INotification eventItem)
        {
            _domainEvents?.Remove(eventItem);
        }

        public void ClearDomainEvents()
        {
            _domainEvents?.Clear();
        }

        public void SetCreatedBy(long id)
        {
            _createdById = id;
        }
    }
}
