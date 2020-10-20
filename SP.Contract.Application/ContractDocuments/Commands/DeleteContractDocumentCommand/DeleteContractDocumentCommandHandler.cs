using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SP.Contract.Application.Common.Exceptions;
using SP.Contract.Application.Common.Handlers;
using SP.Contract.Application.Common.Interfaces;
using SP.Contract.Application.Common.Response;
using SP.Contract.Domains.AggregatesModel.Contract.Entities;
using SP.Market.Identity.Common.Interfaces;

namespace SP.Contract.Application.ContractDocuments.Commands.DeleteContractDocumentCommand
{
    public class DeleteContractDocumentCommandHandler : HandlerBase<DeleteContractDocumentCommand, ProcessingResult<bool>>
    {
        public DeleteContractDocumentCommandHandler(IApplicationDbContext applicationDbContext, ICurrentUserService currentUserService)
            : base(applicationDbContext, currentUserService)
        {
        }

        public override async Task<ProcessingResult<bool>> Handle(DeleteContractDocumentCommand request, CancellationToken cancellationToken)
        {
            var сontractDocument = await ContextDb
                .Set<ContractDocument>()
                .Where(p => p.Id == request.ContractDocumentId).SingleOrDefaultAsync(cancellationToken);

            if (сontractDocument is null)
            {
                throw new NotFoundException(nameof(ContractDocument), request.ContractDocumentId);
            }

            сontractDocument.SetDeleted();
            await ContextDb.SaveChangesAsync(cancellationToken);

            return ResultHelper.Success(true);
        }
    }
}
