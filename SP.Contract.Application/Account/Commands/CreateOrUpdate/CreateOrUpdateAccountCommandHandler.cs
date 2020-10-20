using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SP.Consumers.Models;
using SP.Contract.Application.Common.Handlers;
using SP.Contract.Application.Common.Interfaces;
using SP.Contract.Application.Common.Response;
using SP.Contract.Application.Extensions;
using SP.Market.Identity.Common.Interfaces;

namespace SP.Contract.Application.Account.Commands.CreateOrUpdate
{
    public class CreateOrUpdateAccountCommandHandler : HandlerBase<CreateOrUpdateAccountCommand, ProcessingResult<bool>>
    {
        private readonly IRequestClientService<GetAccountsByIdsRequest, GetAccountsByIdsResponse> _accountRequestClientService;

        public CreateOrUpdateAccountCommandHandler(
            IApplicationDbContext applicationDbContext,
            ICurrentUserService currentUserService,
            IRequestClientService<GetAccountsByIdsRequest, GetAccountsByIdsResponse> accountRequestClientService)
            : base(applicationDbContext, currentUserService)
        {
            _accountRequestClientService = accountRequestClientService ?? throw new ArgumentNullException(nameof(accountRequestClientService));
        }

        public override async Task<ProcessingResult<bool>> Handle(CreateOrUpdateAccountCommand request, CancellationToken cancellationToken)
        {
            var accountList = await _accountRequestClientService.GetResponseAsync(
                new GetAccountsByIdsRequest(request.Accounts.ToArray()),
                cancellationToken);

            var accountEntities =
                accountList.Accounts
                    .Select(x =>
                        new Domains.AggregatesModel.Misc.Entities.Account(x.AccountId, x.FirstName, x.LastName, x.MiddleName, x.OrganizationId))
                    .ToList();

            await ContextDb.Accounts.AddOrUpdateAsync(accountEntities);

            return new ProcessingResult<bool>(true);
        }
    }
}
