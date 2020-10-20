using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SP.Contract.Application.Account.Commands.CreateOrUpdate;
using SP.Contract.Application.Common.Handlers;
using SP.Contract.Application.Common.Interfaces;
using SP.Contract.Application.Common.Response;
using SP.Contract.Application.Organization.Commands.CreateOrUpdate;
using SP.Market.Identity.Common.Interfaces;

namespace SP.Contract.Application.Contract.Commands.Update
{
    public class UpdateContractCommandHandler : HandlerBase<UpdateContractCommand, ProcessingResult<bool>>
    {
        private readonly IMediator _mediator;

        public UpdateContractCommandHandler(
            IApplicationDbContext applicationDbContext,
            ICurrentUserService currentUserService,
            IMediator mediator)
            : base(applicationDbContext, currentUserService)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        public override async Task<ProcessingResult<bool>> Handle(UpdateContractCommand request, CancellationToken cancellationToken)
        {
            var contract = await ContextDb
                .Set<Domains.AggregatesModel.Contract.Entities.Contract>()
                .Where(pd => pd.Id == request.Id)
                .SingleOrDefaultAsync(cancellationToken);

            var currentUser = CurrentUserService.GetCurrentUser();

            await _mediator.Send(
                new CreateOrUpdateOrganization(
                    new List<long>
                    {
                        request.CustomerOrganizationId,
                        request.ContractorOrganizationId,
                        currentUser.OrganizationId ?? 0
                    }), cancellationToken);

            await _mediator.Send(
                new CreateOrUpdateAccountCommand(
                    new List<long>
                    {
                        currentUser.Id
                    }), cancellationToken);

            contract.Update(
                request.ParentId,
                request.ContractStatusId,
                request.CustomerOrganizationId,
                request.ContractorOrganizationId,
                request.Number,
                request.StartDate,
                request.FinishDate);

            await ContextDb.SaveChangesAsync(cancellationToken);

            return ResultHelper.Success(true);
        }
    }
}
