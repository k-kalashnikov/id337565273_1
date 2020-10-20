using System.Threading;
using System.Threading.Tasks;
using Shouldly;
using SP.Contract.Application.Common.Paging;
using SP.Contract.Application.Contract.Models;
using SP.Contract.Application.ContractStatus.Models;
using SP.Contract.Application.ContractStatus.Queries.GetPage;
using SP.Contract.Application.Test.Common;
using Xunit;

namespace SP.Contract.Application.Test.ContractStatus.Queries.GetPage
{
    [Collection("QueryCollection")]
    public class GetPageContractStatusQueryTest : CommandTestBase
    {
        public GetPageContractStatusQueryTest(QueryTestFixture fixture)
            : base(fixture)
        {
        }

        [Fact]
        public async Task CreateContractAsync()
        {
            var handler = new GetPageContractStatusQueryHandler(Context, CurrentUserService, AutoMapper);

            var result = await handler.Handle(
                new GetPageContractStatusQuery(new PageContext<ContractStatusFilter>(1, 10)),
                CancellationToken.None);

            result.ShouldBeOfType<CollectionViewModel<ContractStatusDto>>();
            result.TotalCount.ShouldBe(2);
        }
    }
}
