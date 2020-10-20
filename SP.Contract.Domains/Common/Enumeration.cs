using System;

namespace SP.Contract.Domains.Common
{
    public abstract class Enumeration : ReadOnlyEnumeration
    {
        public string CreatedBy { get; set; }

        public DateTime Created { get; set; }

        public string ModifiedBy { get; set; }

        public DateTime? Modified { get; set; }

        public DateTime? Deleted { get; set; }

        protected Enumeration(int id, string name)
            : base(id, name)
        {
        }
    }
}
