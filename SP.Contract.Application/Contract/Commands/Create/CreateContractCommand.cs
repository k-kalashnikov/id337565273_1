using System;
using MediatR;
using SP.Contract.Application.Common.Response;

namespace SP.Contract.Application.Contract.Commands.Create
{
    public class CreateContractCommand : IRequest<ProcessingResult<Guid?>>
    {
        public CreateContractCommand(Guid? parentId, int? contractStatusId, int contractTypeId, long customerOrganizationId, long contractorOrganizationId, string number, DateTime startDate, DateTime finishDate)
        {
            ParentId = parentId;
            ContractTypeId = contractTypeId;
            CustomerOrganizationId = customerOrganizationId;
            ContractorOrganizationId = contractorOrganizationId;
            ContractStatusId = contractStatusId;
            StartDate = startDate;
            FinishDate = finishDate;
            Number = number;
        }

        public Guid? ParentId { get; set; }

        public int ContractTypeId { get; set; }

        public int? ContractStatusId { get; set; }

        public long CustomerOrganizationId { get; set; }

        public long ContractorOrganizationId { get; set; }

        public string Number { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime FinishDate { get; set; }
    }
}
