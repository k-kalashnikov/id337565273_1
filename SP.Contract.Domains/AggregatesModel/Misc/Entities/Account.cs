using System.ComponentModel.DataAnnotations.Schema;
using SP.Contract.Domains.Common;

namespace SP.Contract.Domains.AggregatesModel.Misc.Entities
{
    public class Account : ImmutableEntity, IPrimaryKeyLong
    {
        private long? _organizationId;

        private Account()
        {
        }

        public Account(long id, string firstName, string lastName, string middleName, long? organizationId)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            MiddleName = middleName;

            _organizationId = organizationId;
        }

        public long Id { get; }

        public string FirstName { get; }

        public string LastName { get; }

        public string MiddleName { get; }

        public Organization Organization { get; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public string FullName { get; }
    }
}
