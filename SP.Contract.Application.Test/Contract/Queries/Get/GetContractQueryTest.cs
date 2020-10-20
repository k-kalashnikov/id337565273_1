using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Shouldly;
using SP.Contract.Application.Contract.Models;
using SP.Contract.Application.Contract.Queries.Get;
using SP.Contract.Application.Test.Common;
using Xunit;

namespace SP.Contract.Application.Test.Contract.Queries.Get
{
    [Collection("QueryCollection")]
    public class GetContractQueryTest : CommandTestBase
    {
        public GetContractQueryTest(QueryTestFixture fixture)
            : base(fixture)
        {
        }

        [Fact]
        public async Task GetContractAsync()
        {
            var contract = await Context.Contracts.FirstAsync();

            var handler = new GetContractQueryHandler(Context, CurrentUserService, AutoMapper);

            var result = await handler.Handle(
                new GetContractQuery(contract.Id),
                CancellationToken.None);

            result.ShouldBeOfType<ContractDto>();
            result.ShouldNotBeNull();
            result.Id.ShouldBe(contract.Id);
        }
    }
}
