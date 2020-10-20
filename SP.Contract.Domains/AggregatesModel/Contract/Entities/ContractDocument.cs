using System;
using SP.Contract.Domains.Common;

namespace SP.Contract.Domains.AggregatesModel.Contract.Entities
{
    public class ContractDocument : Entity, IAggregateRoot
    {
        private Guid _contractId;

        private ContractDocument()
        {
        }

        public ContractDocument(Contract contract, string name, string fileName, string link)
        {
            Contract = contract;
            Name = name;
            FileName = fileName;
            Link = link;
        }

        public override Guid Id { get; protected set; }

        public Contract Contract { get;  protected set; }

        public string Name { get; protected set; }

        public string FileName { get; protected set; }

        public string Link { get; protected set; }
    }
}
