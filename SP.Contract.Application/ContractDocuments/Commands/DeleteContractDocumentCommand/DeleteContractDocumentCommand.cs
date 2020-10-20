using System;
using MediatR;
using SP.Contract.Application.Common.Response;

namespace SP.Contract.Application.ContractDocuments.Commands.DeleteContractDocumentCommand
{
    public class DeleteContractDocumentCommand : IRequest<ProcessingResult<bool>>
    {
        public Guid? ContractDocumentId { get; set; }
    }
}
