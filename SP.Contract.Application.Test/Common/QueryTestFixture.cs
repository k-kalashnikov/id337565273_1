using System;
using AutoMapper;
using SP.Contract.Application.MapperProfiles;
using SP.Contract.Persistence;
using SP.Market.Identity.Common.Interfaces;

namespace SP.Contract.Application.Test.Common
{
    public class QueryTestFixture : IDisposable
    {
        public ApplicationDbContext Context { get; }

        public ICurrentUserService CurrentUserService { get; }

        public IMapper AutoMapper { get; private set; }

        public QueryTestFixture()
        {
            Context = ApplicationDbContextFactory.Create();
            CurrentUserService = MockFactory.CreateCurrentUserServiceMock();

            var config = new MapperConfiguration(c =>
            {
                c.AddProfile<MiscProfile>();
            });
            AutoMapper = config.CreateMapper();
        }

        public void Dispose()
        {
            ApplicationDbContextFactory.Destroy(Context);
        }
    }
}
