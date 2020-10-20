using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SP.Contract.Application.Common.Handlers;
using SP.Contract.Application.Common.Interfaces;
using SP.Contract.Application.Common.Response;
using SP.Contract.Application.ContractDocuments.Commands.DeleteContractDocumentsByContractIdCommand;
using SP.Market.Identity.Common.Interfaces;

namespace SP.Contract.Application.Contract.Commands.Delete
{
    public class DeleteContractCommandHandler : HandlerBase<DeleteContractCommand, ProcessingResult<bool>>
    {
        private readonly IMediator _mediator;

        public DeleteContractCommandHandler(
            IApplicationDbContext applicationDbContext,
            ICurrentUserService currentUserService,
            IMediator mediator)
            : base(applicationDbContext, currentUserService)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        public override async Task<ProcessingResult<bool>> Handle(DeleteContractCommand request, CancellationToken cancellationToken)
        {
            var contract = await ContextDb
                .Set<Domains.AggregatesModel.Contract.Entities.Contract>()
                .Where(p => p.Id == request.ContractId).SingleOrDefaultAsync(cancellationToken);

            if (contract is null)
            {
                return ResultHelper.Error<bool>(new[] { Resources.Resource.ContractNotExist });
            }

            if (contract.Parent != null)
            {
                return ResultHelper.Error<bool>(new[] { Resources.Resource.ContractTiesToPurchase });
            }

            await _mediator.Send(
                new DeleteContractDocumentsByContractIdCommand(contract.Id), cancellationToken);

            contract.SetDeleted();
            await ContextDb.SaveChangesAsync(cancellationToken);

            return ResultHelper.Success(true);
        }
    }
}
