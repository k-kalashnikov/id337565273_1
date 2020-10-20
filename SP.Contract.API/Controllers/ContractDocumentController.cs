using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SP.Contract.Application.Common.Paging;
using SP.Contract.Application.Common.Response;
using SP.Contract.Application.ContractDocuments.Commands.DeleteContractDocumentCommand;
using SP.Contract.Application.ContractDocuments.Commands.LoadContractDocumentByContractId;
using SP.Contract.Application.ContractDocuments.Models;
using SP.Contract.Application.ContractDocuments.Queries.GetByContractList;
using SP.Contract.Application.ContractDocuments.Queries.GetPage;

namespace SP.Contract.API.Controllers
{
    [Route("api/v{version:apiVersion}/contractDocuments")]
    public class ContractDocumentController : BaseController
    {
        [HttpPost]
        [Route("page")]
        [ProducesResponseType(typeof(CollectionViewModel<ContractDocumentDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProcessingResult<bool>), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProcessingResult<bool>), StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<CollectionViewModel<ContractDocumentDto>>> Page(PageContext<ContractDocumentFilter> pageContext, CancellationToken token) =>
            await Mediator.Send(new GetPageContractDocumentQuery(pageContext), token);

        [HttpPost]
        [ProducesResponseType(typeof(ProcessingResult<bool>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProcessingResult<bool>), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProcessingResult<bool>), StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<ProcessingResult<bool>>> LoadByContractId([FromForm]LoadContractDocumentByContractIdCommand commandUpdate)
            => await Mediator.Send(commandUpdate);

        [HttpDelete]
        [Route("{id}")]
        [ProducesResponseType(typeof(ProcessingResult<bool>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProcessingResult<bool>), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProcessingResult<bool>), StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<ProcessingResult<bool>>> DeleteByContractDocumentId(Guid? id)
            => await Mediator.Send(new DeleteContractDocumentCommand() { ContractDocumentId = id });

        [HttpPost]
        [Route("byContractList")]
        [ProducesResponseType(typeof(ProcessingResult<IEnumerable<ContractDocumentDto>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProcessingResult<bool>), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProcessingResult<bool>), StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<ProcessingResult<IEnumerable<ContractDocumentDto>>>> GetByContractList(GetContractDocumentByContractListQuery command)
           => await Mediator.Send(command);
    }
}
