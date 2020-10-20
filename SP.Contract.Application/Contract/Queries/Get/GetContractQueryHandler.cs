using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SP.Contract.Application.Common.Exceptions;
using SP.Contract.Application.Common.Handlers;
using SP.Contract.Application.Common.Interfaces;
using SP.Contract.Application.Contract.Models;
using SP.Market.Identity.Common.Interfaces;

namespace SP.Contract.Application.Contract.Queries.Get
{
    public class GetContractQueryHandler : HandlerQueryBase<GetContractQuery, ContractDto>
    {
        public GetContractQueryHandler(IApplicationDbContext applicationDbContext, ICurrentUserService currentUserService, IMapper mapper)
            : base(applicationDbContext, currentUserService, mapper)
        {
        }

        public override async Task<ContractDto> Handle(GetContractQuery request, CancellationToken cancellationToken)
        {
            var contract = await ContextDb.Set<Domains.AggregatesModel.Contract.Entities.Contract>()
                .Include(c => c.ContractorOrganization)
                .Include(c => c.CustomerOrganization)
                .Include(c => c.ContractStatus)
                .Include(c => c.ContractType)
                .Include(c => c.CreatedBy)
                .Where(c => c.Id == request.Id)
                .SingleOrDefaultAsync(cancellationToken);

            if (contract is null)
            {
                throw new NotFoundException(nameof(Contract), request.Id);
            }

            return AutoMapper.Map<ContractDto>(contract);
        }
    }
}
