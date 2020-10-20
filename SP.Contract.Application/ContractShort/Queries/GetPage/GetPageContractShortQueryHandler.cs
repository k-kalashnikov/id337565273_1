using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SP.Contract.Application.Common.Extensions;
using SP.Contract.Application.Common.Interfaces;
using SP.Contract.Application.Common.Paging;
using SP.Contract.Application.ContractShort.Models;
using SP.Contract.Common;
using SP.Market.Identity.Common.Interfaces;

namespace SP.Contract.Application.ContractShort.Queries.GetPage
{
    public class GetPageContractShortQueryHandler :
        PagingQueryHandler<GetPageContractShortQuery, CollectionViewModel<ContractShortDto>, ContractShortDto>
    {
        public GetPageContractShortQueryHandler(IApplicationDbContext applicationDbContext, ICurrentUserService currentUserService, IMapper mapper)
            : base(applicationDbContext, currentUserService, mapper)
        {
        }

        public override async Task<CollectionViewModel<ContractShortDto>> Handle(GetPageContractShortQuery request, CancellationToken cancellationToken)
        {
            var model = ContextDb.Set<Domains.AggregatesModel.Contract.Entities.Contract>()
                .Include(c => c.ContractorOrganization)
                .Include(c => c.CustomerOrganization)
                .Include(c => c.ContractStatus)
                .Include(c => c.ContractType)
                .Include(c => c.CreatedBy)
                    .ThenInclude(c => c.Organization)
                .Where(BuildFilter(request, out var hasRole).And(x => x.Deleted == null));
            var (list, count) = model.ApplySorting(request.PageContext);

            return hasRole
                ? new CollectionViewModel<ContractShortDto>(
                    AutoMapper.Map<List<ContractShortDto>>(await list.ToListAsync(cancellationToken)),
                    count)
                : new CollectionViewModel<ContractShortDto>();
        }

        private Expression<Func<Domains.AggregatesModel.Contract.Entities.Contract, bool>> BuildFilter(
            GetPageContractShortQuery request,
            out bool hasRole)
        {
            var predicate = PredicateBuilder.True<Domains.AggregatesModel.Contract.Entities.Contract>();

            hasRole = CurrentUserService.IsSuperuser();

            if (!hasRole)
            {
                var organizationId = CurrentUserService.GetCurrentUser().OrganizationId;
                if (CurrentUserService.IsContractorPlatform())
                {
                    predicate = predicate.And(x => x.ContractorOrganization.Id == organizationId);
                    hasRole = true;
                }
                else if (CurrentUserService.IsManager())
                {
                    predicate = predicate.And(x => x.CreatedBy.Organization.Id == organizationId);
                    hasRole = true;
                }
            }

            // filter by number
            if (!string.IsNullOrEmpty(request.PageContext.Filter.Number))
            {
                predicate = predicate.And(x => x.Number != null && x.Number.StartsWith(request.PageContext.Filter.Number));
            }

            return predicate;
        }
    }
}
