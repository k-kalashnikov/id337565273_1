using System;
using AutoMapper;
using MediatR;
using SP.Contract.Application.Common.Interfaces;
using SP.Market.Identity.Common.Interfaces;

namespace SP.Contract.Application.Common.Handlers
{
    public abstract class HandlerQueryBase<TQ, TM> : HandlerBase<TQ, TM>
        where TQ : IRequest<TM>
    {
        protected IMapper AutoMapper { get; }

        protected HandlerQueryBase(IApplicationDbContext applicationDbContext, ICurrentUserService currentUserService, IMapper mapper)
            : base(applicationDbContext, currentUserService)
        {
            AutoMapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }
    }
}
