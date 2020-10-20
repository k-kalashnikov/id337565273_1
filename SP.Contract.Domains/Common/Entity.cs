using System;
using System.ComponentModel.DataAnnotations.Schema;
using SP.Contract.Domains.AggregatesModel.Misc.Entities;

namespace SP.Contract.Domains.Common
{
    public abstract class Entity : ReadOnlyEntity
    {
        private long? _modifiedById;

        [NotMapped]
        public override bool IsReadOnly { get; } = false;

        public Account ModifiedBy { get; set; }

        public DateTime? Modified { get; set; }

        public DateTime? Deleted { get; set; }

        public void SetModifiedBy(long id)
        {
            _modifiedById = id;
        }

        public void SetDeleted() => Deleted = DateTime.Now;
    }
}
