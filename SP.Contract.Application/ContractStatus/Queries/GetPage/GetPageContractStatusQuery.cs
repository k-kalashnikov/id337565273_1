using SP.Contract.Application.Common.Paging;
using SP.Contract.Application.ContractStatus.Models;

namespace SP.Contract.Application.ContractStatus.Queries.GetPage
{
    public class GetPageContractStatusQuery : PagingQuery<CollectionViewModel<ContractStatusDto>, ContractStatusFilter>
    {
        public GetPageContractStatusQuery(IPageContext<ContractStatusFilter> pageContext)
            : base(pageContext)
        {
        }

        public static GetPageContractStatusQuery Create(PageContext<ContractStatusFilter> pageContext) =>
            new GetPageContractStatusQuery(pageContext);
    }
}
