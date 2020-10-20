using SP.Contract.Application.Common.Paging;
using SP.Contract.Application.Contract.Models;

namespace SP.Contract.Application.Contract.Queries.GetPage
{
    public class GetPageContractQueryValidator :
        PagingQueryValidator<GetPageContractQuery, CollectionViewModel<ContractDto>, ContractFilter>
    {
    }
}
