using FluentValidation;
using SP.Contract.Application.Common.Paging;
using SP.Contract.Application.ContractDocuments.Models;

namespace SP.Contract.Application.ContractDocuments.Queries.GetByContractList
{
    public class GetContractDocumentByContractListQueryValidator :
        AbstractValidator<GetContractDocumentByContractListQuery>
    {
        public GetContractDocumentByContractListQueryValidator()
        {
            RuleFor(x => x.ContractList).NotEmpty();
        }
    }
}
