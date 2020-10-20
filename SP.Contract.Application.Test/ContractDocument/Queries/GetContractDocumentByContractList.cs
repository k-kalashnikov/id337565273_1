using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Shouldly;
using SP.Contract.Application.Common.Paging;
using SP.Contract.Application.ContractDocuments.Models;
using SP.Contract.Application.ContractDocuments.Queries.GetByContractList;
using SP.Contract.Application.ContractDocuments.Queries.GetPage;
using SP.Contract.Application.Test.Common;
using Xunit;

namespace SP.Contract.Application.Test.ContractDocument.Queries
{
    [Collection("QueryCollection")]
    public class GetContractDocumentByContractList : CommandTestBase
    {
        public GetContractDocumentByContractList(QueryTestFixture fixture)
            : base(fixture)
        {
        }

        [Fact]
        public async Task GetContractDocumentByContractoListAsync()
        {
            var pageHandler = new GetPageContractDocumentQueryHandler(Context, CurrentUserService, AutoMapper);

            var pageResult = await pageHandler.Handle(
                new GetPageContractDocumentQuery(new PageContext<ContractDocumentFilter>(1, 100)),
                CancellationToken.None);
            pageResult.ShouldBeOfType<CollectionViewModel<ContractDocumentDto>>();
            pageResult.TotalCount.ShouldBeGreaterThanOrEqualTo(1);

            var handler = new GetContractDocumentByContractListQueryHandler(Context, CurrentUserService, AutoMapper);
            var result = await handler.Handle(new GetContractDocumentByContractListQuery(pageResult.Data.Select(x => x.ContractId)), CancellationToken.None);
            result.Result.Count().ShouldBeGreaterThanOrEqualTo(pageResult.Data.Count());
        }
    }
}
