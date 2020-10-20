using SP.Contract.Application.Common.Paging;
using SP.Contract.Application.ContractDocuments.Models;

namespace SP.Contract.Application.ContractDocuments.Queries.GetPage
{
    public class GetPageContractDocumentQuery : PagingQuery<CollectionViewModel<ContractDocumentDto>, ContractDocumentFilter>
    {
        public GetPageContractDocumentQuery(IPageContext<ContractDocumentFilter> pageContext)
            : base(pageContext)
        {
        }
    }
}
