using SP.Contract.Domains.Common;

namespace SP.Contract.Domains.AggregatesModel.Misc.Entities
{
    public class Organization : ImmutableEntity, IPrimaryKeyLong
    {
        private Organization()
        {
        }

        public Organization(long id, string name)
        {
            Id = id;
            Name = name;
        }

        public long Id { get; }

        public string Name { get; }
    }
}
