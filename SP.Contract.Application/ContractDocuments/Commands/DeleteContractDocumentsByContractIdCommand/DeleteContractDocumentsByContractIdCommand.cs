using System;
using MediatR;
using SP.Contract.Application.Common.Response;

namespace SP.Contract.Application.ContractDocuments.Commands.DeleteContractDocumentsByContractIdCommand
{
    public class DeleteContractDocumentsByContractIdCommand : IRequest<ProcessingResult<bool>>
    {
        public DeleteContractDocumentsByContractIdCommand(Guid contractId)
        {
            ContractId = contractId;
        }

        public Guid ContractId { get; set; }
    }
}
