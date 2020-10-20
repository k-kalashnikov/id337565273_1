using System;
using SP.Contract.Application.Account.Models;
using SP.Contract.Application.ContractStatus.Models;
using SP.Contract.Application.ContractType.Models;

namespace SP.Contract.Application.Contract.Models
{
    public class ContractDto
    {
        public Guid Id { get; set; }

        public Guid? ParentId { get; set; }

        public ContractStatusDto ContractStatus { get; set; }

        public ContractTypeDto ContractType { get; set; }

        public OrganizationDto CustomerOrganization { get; set; }

        public OrganizationDto ContractorOrganization { get; set; }

        public string Number { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime FinishDate { get; set; }

        public AccountDto CreatedBy { get; set; }

        public DateTime Created { get; set; }
    }
}
