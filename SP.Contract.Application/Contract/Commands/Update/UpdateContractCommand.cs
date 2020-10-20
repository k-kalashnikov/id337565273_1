using System;
using MediatR;
using SP.Contract.Application.Common.Response;

namespace SP.Contract.Application.Contract.Commands.Update
{
    public class UpdateContractCommand : IRequest<ProcessingResult<bool>>
    {
        public UpdateContractCommand(Guid id, Guid? parentId, int contractStatusId, long customerOrganizationId, long contractorOrganizationId, string number, DateTime startDate, DateTime finishDate)
        {
            Id = id;
            ParentId = parentId;
            ContractStatusId = contractStatusId;
            CustomerOrganizationId = customerOrganizationId;
            ContractorOrganizationId = contractorOrganizationId;
            Number = number;
            StartDate = startDate;
            FinishDate = finishDate;
        }

        public Guid Id { get; set; }

        public Guid? ParentId { get; set; }

        public int ContractStatusId { get; set; }

        public long CustomerOrganizationId { get; set; }

        public long ContractorOrganizationId { get; set; }

        public string Number { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime FinishDate { get; set; }
    }
}
