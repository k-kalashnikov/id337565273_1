using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using SP.Contract.Application.Common.Handlers;
using SP.Contract.Application.Common.Interfaces;
using SP.Market.Identity.Common.Interfaces;

namespace SP.Contract.Application.Common.Paging
{
    public abstract class PagingQueryHandler<TQ, TCM, TM> : HandlerQueryBase<TQ, TCM>
        where TQ : IRequest<TCM>
        where TCM : CollectionViewModel<TM>, new()
    {
        protected PagingQueryHandler(IApplicationDbContext applicationDbContext, ICurrentUserService currentUserService, IMapper mapper)
            : base(applicationDbContext, currentUserService, mapper)
        {
        }

        public abstract override Task<TCM> Handle(TQ request, CancellationToken cancellationToken);
    }
}
