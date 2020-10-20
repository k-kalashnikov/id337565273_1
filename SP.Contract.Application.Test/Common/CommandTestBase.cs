using System;
using AutoMapper;
using SP.Contract.Persistence;
using SP.Market.Identity.Common.Interfaces;

namespace SP.Contract.Application.Test.Common
{
    public class CommandTestBase
    {
        protected ApplicationDbContext Context { get; private set; }

        protected ICurrentUserService CurrentUserService { get; private set; }

        protected IMapper AutoMapper { get; private set; }

        protected CommandTestBase(QueryTestFixture fixture)
        {
            Context = fixture.Context;
            CurrentUserService = fixture.CurrentUserService;
            AutoMapper = fixture.AutoMapper;
        }
    }
}
