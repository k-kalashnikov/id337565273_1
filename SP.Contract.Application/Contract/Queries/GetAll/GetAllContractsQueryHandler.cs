using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SP.Contract.Application.Common.Exceptions;
using SP.Contract.Application.Common.Handlers;
using SP.Contract.Application.Common.Interfaces;
using SP.Contract.Application.Contract.Models;
using SP.Market.Identity.Common.Interfaces;

namespace SP.Contract.Application.Contract.Queries.GetAll
{
    public class GetAllContractsQueryHandler : HandlerQueryBase<GetAllContractsQuery, IEnumerable<ContractDto>>
    {
        public GetAllContractsQueryHandler(IApplicationDbContext applicationDbContext, ICurrentUserService currentUserService, IMapper mapper)
            : base(applicationDbContext, currentUserService, mapper)
        {
        }

        public override async Task<IEnumerable<ContractDto>> Handle(GetAllContractsQuery request, CancellationToken cancellationToken)
        {
            var contracts = await ContextDb.Set<Domains.AggregatesModel.Contract.Entities.Contract>()
                .Include(c => c.ContractorOrganization)
                .Include(c => c.CustomerOrganization)
                .Include(c => c.ContractStatus)
                .Include(c => c.ContractType)
                .Include(c => c.CreatedBy)
                .ToListAsync(cancellationToken);

            if (contracts is null)
            {
                throw new NotFoundException(nameof(Contract), request);
            }

            return contracts.Select(m => AutoMapper.Map<ContractDto>(m));
        }
    }
}
