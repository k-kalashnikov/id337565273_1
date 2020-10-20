using System;
using SP.Contract.Events.Interfaces;

namespace SP.Contract.Events.Event.UpdateContract
{
    public class UpdateContractEvent : IEvent
    {
        public Guid Id { get; set; }

        public Guid? Parent { get; set; }

        public int ContractStatusId { get;  set; }

        public int ContractTypeId { get;  set; }

        public long CustomerOrganizationId { get; set; }

        public long ContractorOrganizationId { get; set; }

        public string Number { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime FinishDate { get; set; }

        public bool? SignedByCustomer { get; set; }

        public bool? SignedByContractor { get; set; }
    }
}
