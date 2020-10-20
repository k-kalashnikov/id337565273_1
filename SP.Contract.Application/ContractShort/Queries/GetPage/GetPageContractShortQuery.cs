using SP.Contract.Application.Common.Paging;
using SP.Contract.Application.ContractShort.Models;

namespace SP.Contract.Application.ContractShort.Queries.GetPage
{
    public class GetPageContractShortQuery : PagingQuery<CollectionViewModel<ContractShortDto>, ContractShortFilter>
    {
        public GetPageContractShortQuery(IPageContext<ContractShortFilter> pageContext)
            : base(pageContext)
        {
        }

        public static GetPageContractShortQuery Create(PageContext<ContractShortFilter> pageContext) =>
            new GetPageContractShortQuery(pageContext);
    }
}
