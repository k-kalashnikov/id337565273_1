using System;
using System.Collections.Generic;
using MediatR;
using SP.Contract.Application.Common.Paging;
using SP.Contract.Application.Common.Response;
using SP.Contract.Application.ContractDocuments.Models;

namespace SP.Contract.Application.ContractDocuments.Queries.GetByContractList
{
    public class GetContractDocumentByContractListQuery : IRequest<ProcessingResult<IEnumerable<ContractDocumentDto>>>
    {
        public GetContractDocumentByContractListQuery(IEnumerable<Guid> contractList)
        {
            ContractList = contractList;
        }

        public IEnumerable<Guid> ContractList { get; set; }
    }
}
