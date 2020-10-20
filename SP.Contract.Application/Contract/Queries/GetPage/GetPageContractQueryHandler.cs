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
using SP.Contract.Application.Contract.Models;
using SP.Contract.Application.Specification;
using SP.Contract.Common;
using SP.Market.Identity.Common.Interfaces;

namespace SP.Contract.Application.Contract.Queries.GetPage
{
    public class GetPageContractQueryHandler :
        PagingQueryHandler<GetPageContractQuery, CollectionViewModel<ContractDto>, ContractDto>
    {
        public GetPageContractQueryHandler(IApplicationDbContext applicationDbContext, ICurrentUserService currentUserService, IMapper mapper)
            : base(applicationDbContext, currentUserService, mapper)
        {
        }

        public override async Task<CollectionViewModel<ContractDto>> Handle(GetPageContractQuery request, CancellationToken cancellationToken)
        {
            var specification = new SpecificationContract().CreatePaging(request.PageContext);

            var model = ContextDb.Set<Domains.AggregatesModel.Contract.Entities.Contract>()
                .Include(c => c.ContractorOrganization)
                .Include(c => c.CustomerOrganization)
                .Include(c => c.ContractStatus)
                .Include(c => c.ContractType)
                .Include(c => c.CreatedBy)
                .ThenInclude(c => c.Organization)
                .Where(BuildFilterByRole().And(x => x.Deleted == null))
                .OrderByDescending(m => m.Created);

            var (query, count) = specification.PreparePaging(model);
            var entities = await query.ToListAsync(cancellationToken);
            var dtos = AutoMapper.Map<List<ContractDto>>(entities);
            return new CollectionViewModel<ContractDto>(dtos, count);
        }

        private Expression<Func<Domains.AggregatesModel.Contract.Entities.Contract, bool>> BuildFilterByRole()
        {
            var predicate = PredicateBuilder.True<Domains.AggregatesModel.Contract.Entities.Contract>();

            var organizationId = CurrentUserService.GetCurrentUser().OrganizationId;
            if (CurrentUserService.IsContractorPlatform() && CurrentUserService.IsManager())
            {
                predicate = predicate.And(x => x.CustomerOrganization.Id == organizationId);
            }
            else if (CurrentUserService.IsContractorPlatform())
            {
                predicate = predicate.And(x => x.ContractorOrganization.Id == organizationId);
            }
            else if (CurrentUserService.IsManager())
            {
                predicate = predicate.And(x => x.CustomerOrganization.Id == organizationId);
            }

            return predicate;
        }
    }
}
