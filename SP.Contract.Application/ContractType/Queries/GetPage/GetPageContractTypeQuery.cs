using SP.Contract.Application.Common.Paging;
using SP.Contract.Application.ContractType.Models;

namespace SP.Contract.Application.ContractType.Queries.GetPage
{
    public class GetPageContractTypeQuery : PagingQuery<CollectionViewModel<ContractTypeDto>, ContractTypeFilter>
    {
        public GetPageContractTypeQuery(IPageContext<ContractTypeFilter> pageContext)
            : base(pageContext)
        {
        }

        public static GetPageContractTypeQuery Create(PageContext<ContractTypeFilter> pageContext) =>
            new GetPageContractTypeQuery(pageContext);
    }
}
