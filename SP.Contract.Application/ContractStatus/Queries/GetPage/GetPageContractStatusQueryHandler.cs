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
using SP.Contract.Application.ContractStatus.Models;
using SP.Contract.Common;
using SP.Contract.Common.Extensions;
using SP.Market.Identity.Common.Interfaces;

namespace SP.Contract.Application.ContractStatus.Queries.GetPage
{
    public class GetPageContractStatusQueryHandler :
        PagingQueryHandler<GetPageContractStatusQuery, CollectionViewModel<ContractStatusDto>, ContractStatusDto>
    {
        public GetPageContractStatusQueryHandler(IApplicationDbContext applicationDbContext, ICurrentUserService currentUserService, IMapper mapper)
            : base(applicationDbContext, currentUserService, mapper)
        {
        }

        public override async Task<CollectionViewModel<ContractStatusDto>> Handle(GetPageContractStatusQuery request, CancellationToken cancellationToken)
        {
            var model = ContextDb.Set<Domains.AggregatesModel.Contract.Entities.ContractStatus>()
                .Where(BuildFilter(request));

            var (list, count) = model.ApplySorting(request.PageContext);

            return new CollectionViewModel<ContractStatusDto>(
                AutoMapper.Map<List<ContractStatusDto>>(await list.ToListAsync(cancellationToken)),
                count);
        }

        private Expression<Func<Domains.AggregatesModel.Contract.Entities.ContractStatus, bool>> BuildFilter(GetPageContractStatusQuery request)
        {
            var predicate = PredicateBuilder.True<Domains.AggregatesModel.Contract.Entities.ContractStatus>();
            if (!string.IsNullOrEmpty(request.PageContext.Filter.Name))
            {
                predicate = predicate.And(x =>
                    EF.Functions.Like(x.Name, request.PageContext.Filter.Name.LikeWildcardBoth()));
            }

            return predicate;
        }
    }
}
