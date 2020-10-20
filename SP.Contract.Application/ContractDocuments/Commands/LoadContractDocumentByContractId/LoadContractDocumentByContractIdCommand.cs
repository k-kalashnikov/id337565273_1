using System;
using MediatR;
using Microsoft.AspNetCore.Http;
using SP.Contract.Application.Common.Response;

namespace SP.Contract.Application.ContractDocuments.Commands.LoadContractDocumentByContractId
{
    public class LoadContractDocumentByContractIdCommand : IRequest<ProcessingResult<bool>>
    {
        public Guid ContractId { get; set; }

        public string Name { get; set; }

        public IFormFile Data { get; set; }
    }
}
