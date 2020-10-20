using System;

namespace SP.Contract.Events.Models
{
    public class ContractDto
    {
        public Guid Id { get; set; }

        public Guid? ParentId { get; set; }

        public ContractStatusDto ContractStatus { get; set; }

        public ContractTypeDto ContractType { get; set; }

        public long CustomerOrganizationId { get; set; }

        public long ContractorOrganizationId { get; set; }

        public string Number { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime FinishDate { get; set; }

        public string CreatedBy { get; set; }

        public DateTime Created { get; set; }
    }
}
