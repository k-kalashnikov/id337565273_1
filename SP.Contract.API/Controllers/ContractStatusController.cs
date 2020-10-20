using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SP.Contract.Application.Common.Paging;
using SP.Contract.Application.Common.Response;
using SP.Contract.Application.ContractStatus.Models;
using SP.Contract.Application.ContractStatus.Queries.GetPage;

namespace SP.Contract.API.Controllers
{
    [Route("api/v{version:apiVersion}/contractStatuses")]
    public class ContractStatusController : BaseController
    {
        [HttpPost]
        [Route("page")]
        [ProducesResponseType(typeof(CollectionViewModel<ContractStatusDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProcessingResult<bool>), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProcessingResult<bool>), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ProcessingResult<bool>), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<CollectionViewModel<ContractStatusDto>>> Page(PageContext<ContractStatusFilter> pageContext, CancellationToken token) =>
            await Mediator.Send(GetPageContractStatusQuery.Create(pageContext), token);
    }
}
