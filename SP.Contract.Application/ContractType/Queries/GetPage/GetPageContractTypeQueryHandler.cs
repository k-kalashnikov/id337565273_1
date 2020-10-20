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
using SP.Contract.Application.ContractType.Models;
using SP.Contract.Common;
using SP.Contract.Common.Extensions;
using SP.Market.Identity.Common.Interfaces;

namespace SP.Contract.Application.ContractType.Queries.GetPage
{
    public class GetPageContractTypeQueryHandler :
        PagingQueryHandler<GetPageContractTypeQuery, CollectionViewModel<ContractTypeDto>, ContractTypeDto>
    {
        public GetPageContractTypeQueryHandler(IApplicationDbContext applicationDbContext, ICurrentUserService currentUserService, IMapper mapper)
            : base(applicationDbContext, currentUserService, mapper)
        {
        }

        public override async Task<CollectionViewModel<ContractTypeDto>> Handle(GetPageContractTypeQuery request, CancellationToken cancellationToken)
        {
            var model = ContextDb.Set<Domains.AggregatesModel.Contract.Entities.ContractType>()
                .Where(BuildFilter(request));

            var (list, count) = model.ApplySorting(request.PageContext);

            return new CollectionViewModel<ContractTypeDto>(
                AutoMapper.Map<List<ContractTypeDto>>(await list.ToListAsync(cancellationToken)),
                count);
        }

        private Expression<Func<Domains.AggregatesModel.Contract.Entities.ContractType, bool>> BuildFilter(GetPageContractTypeQuery request)
        {
            var predicate = PredicateBuilder.True<Domains.AggregatesModel.Contract.Entities.ContractType>();
            if (!string.IsNullOrEmpty(request.PageContext.Filter.Name))
            {
                predicate = predicate.And(x =>
                    EF.Functions.Like(x.Name, request.PageContext.Filter.Name.LikeWildcardBoth()));
            }

            return predicate;
        }
    }
}
