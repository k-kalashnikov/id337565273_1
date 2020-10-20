using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SP.Contract.Application.Common.Paging;
using SP.Contract.Application.Common.Response;
using SP.Contract.Application.Contract.Commands.Create;
using SP.Contract.Application.Contract.Commands.Delete;
using SP.Contract.Application.Contract.Commands.Update;
using SP.Contract.Application.Contract.Models;
using SP.Contract.Application.Contract.Queries.Get;
using SP.Contract.Application.Contract.Queries.GetAll;
using SP.Contract.Application.Contract.Queries.GetPage;
using SP.Contract.Application.ContractShort.Models;
using SP.Contract.Application.ContractShort.Queries.GetPage;

namespace SP.Contract.API.Controllers
{
    [Route("api/v{version:apiVersion}/contracts")]
    public class ContractController : BaseController
    {
        [HttpGet]
        [Route("{id}")]
        [ProducesResponseType(typeof(ContractDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProcessingResult<bool>), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProcessingResult<bool>), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ProcessingResult<bool>), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ContractDto>> Get(Guid id, CancellationToken token) =>
            await Mediator.Send(GetContractQuery.Create(id), token);

        [HttpPost]
        [Route("page")]
        [ProducesResponseType(typeof(CollectionViewModel<ContractDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProcessingResult<bool>), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProcessingResult<bool>), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ProcessingResult<bool>), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<CollectionViewModel<ContractDto>>> Page(PageContext<ContractFilter> pageContext, CancellationToken token) =>
            await Mediator.Send(GetPageContractQuery.Create(pageContext), token);

        [HttpPost]
        [Route("short/page")]
        [ProducesResponseType(typeof(CollectionViewModel<ContractShortDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProcessingResult<bool>), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProcessingResult<bool>), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ProcessingResult<bool>), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<CollectionViewModel<ContractShortDto>>> Page(PageContext<ContractShortFilter> pageContext, CancellationToken token) =>
        await Mediator.Send(GetPageContractShortQuery.Create(pageContext), token);

        [HttpPost]
        [Route("")]
        [ProducesResponseType(typeof(ProcessingResult<Guid?>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProcessingResult<bool>), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProcessingResult<bool>), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ProcessingResult<bool>), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ProcessingResult<Guid?>>> Create(CreateContractCommand command, CancellationToken token) =>
            await Mediator.Send(command, token);

        [HttpPut]
        [Route("")]
        [ProducesResponseType(typeof(ProcessingResult<bool>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProcessingResult<bool>), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProcessingResult<bool>), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ProcessingResult<bool>), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ProcessingResult<bool>>> Update(UpdateContractCommand command, CancellationToken token) =>
            await Mediator.Send(command, token);

        [HttpDelete]
        [Route("{id}")]
        [ProducesResponseType(typeof(ProcessingResult<bool>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProcessingResult<bool>), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProcessingResult<bool>), StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<ProcessingResult<bool>>> DeleteByContractDocumentId(Guid? id)
            => await Mediator.Send(new DeleteContractCommand(id));
    }
}
