using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SP.Contract.Application.Common.Interfaces;
using SP.Market.Identity.Common.Interfaces;

namespace SP.Contract.Application.Common.Handlers
{
    public abstract class HandlerBase<TQ, TM> : IRequestHandler<TQ, TM>
        where TQ : IRequest<TM>
    {
        protected IApplicationDbContext ContextDb { get; }

        protected ICurrentUserService CurrentUserService { get; }

        protected HandlerBase(IApplicationDbContext applicationDbContext, ICurrentUserService currentUserService)
        {
            ContextDb = applicationDbContext ?? throw new ArgumentNullException(nameof(applicationDbContext));
            CurrentUserService = currentUserService ?? throw new ArgumentNullException(nameof(currentUserService));
        }

        public abstract Task<TM> Handle(TQ request, CancellationToken cancellationToken);
    }
}
