using SP.Contract.Application.Common.Paging;
using SP.Contract.Application.Contract.Models;

namespace SP.Contract.Application.Contract.Queries.GetPage
{
    public class GetPageContractQuery : PagingQuery<CollectionViewModel<ContractDto>, ContractFilter>
    {
        public GetPageContractQuery(IPageContext<ContractFilter> pageContext)
            : base(pageContext)
        {
        }

        public static GetPageContractQuery Create(PageContext<ContractFilter> pageContext) =>
            new GetPageContractQuery(pageContext);
    }
}
