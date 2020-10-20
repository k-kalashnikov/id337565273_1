using SP.Contract.Application.Common.Paging;
using SP.Contract.Application.ContractShort.Models;

namespace SP.Contract.Application.ContractShort.Queries.GetPage
{
    public class GetPageContractShortQueryValidator :
        PagingQueryValidator<GetPageContractShortQuery, CollectionViewModel<ContractShortDto>, ContractShortFilter>
    {
    }
}
