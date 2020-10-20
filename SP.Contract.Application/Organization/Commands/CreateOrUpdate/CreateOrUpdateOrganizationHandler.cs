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

namespace SP.Contract.Application.Organization.Commands.CreateOrUpdate
{
    public class CreateOrUpdateOrganizationHandler : HandlerBase<CreateOrUpdateOrganization, ProcessingResult<bool>>
    {
        private readonly IRequestClientService<GetOrganizationsRequest, GetOrganizationsResponse> _organizationRequestClientService;

        public CreateOrUpdateOrganizationHandler(
            IApplicationDbContext applicationDbContext,
            ICurrentUserService currentUserService,
            IRequestClientService<GetOrganizationsRequest, GetOrganizationsResponse> organizationRequestClientService)
            : base(applicationDbContext, currentUserService)
        {
            _organizationRequestClientService = organizationRequestClientService ?? throw new ArgumentNullException(nameof(organizationRequestClientService));
        }

        public override async Task<ProcessingResult<bool>> Handle(CreateOrUpdateOrganization request, CancellationToken cancellationToken)
        {
            var organizationList = await _organizationRequestClientService.GetResponsesAsync(
                new GetOrganizationsRequest(request.Organizations.Where(o => o != 0).ToArray()),
                cancellationToken);

            var organizationEntities =
                organizationList
                    .Select(x =>
                        new Domains.AggregatesModel.Misc.Entities.Organization(x.OrganizationId, x.Name))
                    .ToList();

            await ContextDb.Organizations.AddOrUpdateAsync(organizationEntities);

            return new ProcessingResult<bool>(true);
        }
    }
}
