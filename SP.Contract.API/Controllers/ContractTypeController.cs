using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SP.Contract.Application.Common.Paging;
using SP.Contract.Application.Common.Response;
using SP.Contract.Application.ContractType.Models;
using SP.Contract.Application.ContractType.Queries.GetPage;

namespace SP.Contract.API.Controllers
{
    [Route("api/v{version:apiVersion}/contractTypes")]
    public class ContractTypeController : BaseController
    {
        [HttpPost]
        [Route("page")]
        [ProducesResponseType(typeof(CollectionViewModel<ContractTypeDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProcessingResult<bool>), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProcessingResult<bool>), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ProcessingResult<bool>), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<CollectionViewModel<ContractTypeDto>>> Page(PageContext<ContractTypeFilter> pageContext, CancellationToken token) =>
            await Mediator.Send(GetPageContractTypeQuery.Create(pageContext), token);
    }
}
