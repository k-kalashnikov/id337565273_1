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
using SP.Contract.Application.ContractDocuments.Models;
using SP.Contract.Common;
using SP.Contract.Domains.AggregatesModel.Contract.Entities;
using SP.Market.Identity.Common.Interfaces;

namespace SP.Contract.Application.ContractDocuments.Queries.GetPage
{
    public class GetPageContractDocumentQueryHandler :
        PagingQueryHandler<GetPageContractDocumentQuery, CollectionViewModel<ContractDocumentDto>, ContractDocumentDto>
    {
        public GetPageContractDocumentQueryHandler(IApplicationDbContext applicationDbContext, ICurrentUserService currentUserService, IMapper mapper)
            : base(applicationDbContext, currentUserService, mapper)
        {
        }

        public override async Task<CollectionViewModel<ContractDocumentDto>> Handle(GetPageContractDocumentQuery request, CancellationToken cancellationToken)
        {
            var model = ContextDb.Set<ContractDocument>()
                .Include(c => c.Contract)
                .Where(BuildFilter(request).And(x => x.Deleted == null));

            var (list, count) = model.ApplySorting(request.PageContext);

            return new CollectionViewModel<ContractDocumentDto>(
                AutoMapper.Map<List<ContractDocumentDto>>(await list.ToListAsync(cancellationToken)),
                count);
        }

        private Expression<Func<ContractDocument, bool>> BuildFilter(GetPageContractDocumentQuery request)
        {
            var predicate = PredicateBuilder.True<ContractDocument>();

            if (request.PageContext.Filter.ContractId.HasValue)
            {
                predicate = predicate.And(x => x.Contract.Id == request.PageContext.Filter.ContractId);
            }

            return predicate;
        }
    }
}
