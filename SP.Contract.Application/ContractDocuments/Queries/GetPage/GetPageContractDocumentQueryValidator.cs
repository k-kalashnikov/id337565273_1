using SP.Contract.Application.Common.Paging;
using SP.Contract.Application.ContractDocuments.Models;

namespace SP.Contract.Application.ContractDocuments.Queries.GetPage
{
    public class GetPageContractDocumentQueryValidator :
        PagingQueryValidator<GetPageContractDocumentQuery, CollectionViewModel<ContractDocumentDto>, ContractDocumentFilter>
    {
    }
}
