using System;
using System.Threading.Tasks;
using Shouldly;
using SP.Contract.API;
using SP.Contract.Application.Common.Paging;
using SP.Contract.Application.Common.Response;
using SP.Contract.Application.ContractDocuments.Models;
using SP.Contract.Application.ContractDocuments.Queries.GetPage;
using SP.Contract.Common.Extensions;
using SP.Contract.Rest.Function.Test.Common;
using Xunit;

namespace SP.Contract.Rest.Function.Test.Controllers
{
    public class ContractDocumentControllerTest : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private readonly CustomWebApplicationFactory<Startup> _factory;

        public ContractDocumentControllerTest(CustomWebApplicationFactory<Startup> factory)
        {
            _factory = factory;
        }

        [Fact]
        public async Task GetPageContractDocuments_ReturnsResult()
        {
            var client = await _factory.GetAuthenticatedClientAsync();

            var obj = new GetPageContractDocumentQuery(
                new PageContext<ContractDocumentFilter>(1, 10));

            var response = await client.PostAsync(
                "/api/v1/contractDocuments/page",
                new JsonContent<IPageContext<ContractDocumentFilter>>(obj.PageContext));

            response.EnsureSuccessStatusCode();

            var content = await Utilities.GetResponseContentAsync<CollectionViewModel<ContractDocumentDto>>(response);

            content.ShouldNotBeNull();
        }
    }
}
