using System;
using MediatR;
using SP.Contract.Application.Common.Response;

namespace SP.Contract.Application.Contract.Commands.Delete
{
    public class DeleteContractCommand : IRequest<ProcessingResult<bool>>
    {
        public DeleteContractCommand(Guid? contractId)
        {
            ContractId = contractId;
        }

        public Guid? ContractId { get; set; }
    }
}
