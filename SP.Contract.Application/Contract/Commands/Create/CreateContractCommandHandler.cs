using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SP.Contract.Application.Account.Commands.CreateOrUpdate;
using SP.Contract.Application.Common.Handlers;
using SP.Contract.Application.Common.Interfaces;
using SP.Contract.Application.Common.Response;
using SP.Contract.Application.Organization.Commands.CreateOrUpdate;
using SP.Market.EventBus.RMQ.Shared.Models.Platform.Contracts.UpdatePurchaseContract;
using SP.Market.EventBus.RMQ.Shared.Models.Platform.Purchases.GetPurchases;
using SP.Market.Identity.Common.Interfaces;

namespace SP.Contract.Application.Contract.Commands.Create
{
    public class CreateContractCommandHandler : HandlerBase<CreateContractCommand, ProcessingResult<Guid?>>
    {
        private readonly IMediator _mediator;
        private readonly IRequestClientService<GetPurchasesRequest, GetPurchasesResponse> _purchaseRequestClientService;
        private readonly IRequestClientService<UpdatePurchaseContractRequest, UpdatePurchaseContractResponse> _purchaseUpdateContractRequestClientService;

        public CreateContractCommandHandler(
            IApplicationDbContext applicationDbContext,
            ICurrentUserService currentUserService,
            IMediator mediator,
            IRequestClientService<GetPurchasesRequest, GetPurchasesResponse> purchaseRequestClientService,
            IRequestClientService<UpdatePurchaseContractRequest, UpdatePurchaseContractResponse> purchaseUpdateContractRequestClientService)
            : base(applicationDbContext, currentUserService)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _purchaseRequestClientService = purchaseRequestClientService;
            _purchaseUpdateContractRequestClientService = purchaseUpdateContractRequestClientService;
        }

        public override async Task<ProcessingResult<Guid?>> Handle(CreateContractCommand request, CancellationToken cancellationToken)
        {
            if (request.ContractTypeId == 2)
            {
                if (!request.ParentId.HasValue)
                {
                    return ResultHelper.Error<Guid?>(new[] { Resources.Resource.ValidationError_NotBeNull });
                }

                var purchaseDataResponse = await _purchaseRequestClientService.GetResponseAsync(
                    new GetPurchasesRequest(new[] { request.ParentId.Value }),
                    cancellationToken);
                var purchase = purchaseDataResponse.Purchases.FirstOrDefault();
                if (purchase == null)
                {
                    return ResultHelper.Error<Guid?>(new[] { Resources.Resource.ValidationError_NotBeNull });
                }
            }

            var contract = new Domains.AggregatesModel.Contract.Entities.Contract(
                request.ParentId,
                request.ContractTypeId,
                request.ContractStatusId,
                request.CustomerOrganizationId,
                request.ContractorOrganizationId,
                request.Number,
                request.StartDate,
                request.FinishDate);

            await ContextDb
                .Set<Domains.AggregatesModel.Contract.Entities.Contract>()
                .AddAsync(contract, cancellationToken);

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

            await ContextDb.SaveChangesAsync(cancellationToken);

            if (request.ContractTypeId == 2 && request.ParentId.HasValue)
            {
                var purchaseContractResponse = await _purchaseUpdateContractRequestClientService.GetResponseAsync(
                    new UpdatePurchaseContractRequest(contract.Id, request.ParentId.Value),
                    cancellationToken);
                if (!purchaseContractResponse.IsSuccess)
                {
                    return ResultHelper.Error<Guid?>(new[] { Resources.Resource.FailedToBindPurchaseToContract });
                }
            }

            return ResultHelper.Success((Guid?)contract.Id);
        }
    }
}
