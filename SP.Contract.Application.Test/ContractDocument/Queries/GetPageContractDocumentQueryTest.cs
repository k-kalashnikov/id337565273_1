using System.Threading;
using System.Threading.Tasks;
using Shouldly;
using SP.Contract.Application.Common.Paging;
using SP.Contract.Application.ContractDocuments.Models;
using SP.Contract.Application.ContractDocuments.Queries.GetPage;
using SP.Contract.Application.Test.Common;
using Xunit;

namespace SP.Contract.Application.Test.ContractDocument.Queries
{
    [Collection("QueryCollection")]
    public class GetPageContractDocumentQueryTest : CommandTestBase
    {
        public GetPageContractDocumentQueryTest(QueryTestFixture fixture)
            : base(fixture)
        {
        }

        [Fact]
        public async Task GetPageContractDocumentAsync()
        {
            var handler = new GetPageContractDocumentQueryHandler(Context, CurrentUserService, AutoMapper);

            var result = await handler.Handle(
                new GetPageContractDocumentQuery(new PageContext<ContractDocumentFilter>(1, 10)),
                CancellationToken.None);

            result.ShouldBeOfType<CollectionViewModel<ContractDocumentDto>>();
            result.TotalCount.ShouldBeGreaterThanOrEqualTo(1);
        }
    }
}
