using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SP.Contract.Application.Common.Extensions;
using SP.Contract.Application.Common.Handlers;
using SP.Contract.Application.Common.Interfaces;
using SP.Contract.Application.Common.Paging;
using SP.Contract.Application.Common.Response;
using SP.Contract.Application.ContractDocuments.Models;
using SP.Contract.Common;
using SP.Contract.Domains.AggregatesModel.Contract.Entities;
using SP.Market.Identity.Common.Interfaces;

namespace SP.Contract.Application.ContractDocuments.Queries.GetByContractList
{
    public class GetContractDocumentByContractListQueryHandler :
        HandlerBase<GetContractDocumentByContractListQuery, ProcessingResult<IEnumerable<ContractDocumentDto>>>
    {
        private readonly IMapper _mapper;

        public GetContractDocumentByContractListQueryHandler(IApplicationDbContext applicationDbContext, ICurrentUserService currentUserService, IMapper mapper)
           : base(applicationDbContext, currentUserService)
        {
            _mapper = mapper;
        }

        public override async Task<ProcessingResult<IEnumerable<ContractDocumentDto>>> Handle(GetContractDocumentByContractListQuery request, CancellationToken cancellationToken)
        {
            var model = ContextDb.Set<ContractDocument>()
                .Include(c => c.Contract)
                .Where(BuildFilter(request).And(x => x.Deleted == null));
            return new ProcessingResult<IEnumerable<ContractDocumentDto>>(
                _mapper.Map<IEnumerable<ContractDocumentDto>>(await model.ToListAsync(cancellationToken)));
        }

        private Expression<Func<ContractDocument, bool>> BuildFilter(GetContractDocumentByContractListQuery request)
        {
            var predicate = PredicateBuilder.True<ContractDocument>();

            if (request.ContractList.Any())
            {
                predicate = predicate.And(x => request.ContractList.Contains(x.Contract.Id));
            }

            return predicate;
        }
    }
}
