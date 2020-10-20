using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SP.Contract.Application.Common.Handlers;
using SP.Contract.Application.Common.Interfaces;
using SP.Contract.Application.Common.Response;
using SP.Contract.Domains.AggregatesModel.Contract.Entities;
using SP.Market.Identity.Common.Interfaces;

namespace SP.Contract.Application.ContractDocuments.Commands.DeleteContractDocumentsByContractIdCommand
{
    public class DeleteContractDocumentsByContractIdCommandHandler : HandlerBase<DeleteContractDocumentsByContractIdCommand, ProcessingResult<bool>>
    {
        public DeleteContractDocumentsByContractIdCommandHandler(IApplicationDbContext applicationDbContext, ICurrentUserService currentUserService)
            : base(applicationDbContext, currentUserService)
        {
        }

        public override async Task<ProcessingResult<bool>> Handle(DeleteContractDocumentsByContractIdCommand request, CancellationToken cancellationToken)
        {
            var contractDocuments = await ContextDb
                .Set<ContractDocument>()
                .Include(cd => cd.Contract)
                .Where(p => p.Contract.Id == request.ContractId).ToListAsync(cancellationToken);

            contractDocuments.ForEach(x => x.SetDeleted());

            await ContextDb.SaveChangesAsync(cancellationToken);

            return ResultHelper.Success(true);
        }
    }
}
