using System;
using System.Threading.Tasks;
using Shouldly;
using SP.Contract.API;
using SP.Contract.Application.Common.Paging;
using SP.Contract.Application.Common.Response;
using SP.Contract.Application.Contract.Commands.Create;
using SP.Contract.Application.Contract.Commands.Delete;
using SP.Contract.Application.Contract.Commands.Update;
using SP.Contract.Application.Contract.Models;
using SP.Contract.Application.Contract.Queries.GetPage;
using SP.Contract.Common.Extensions;
using SP.Contract.Rest.Function.Test.Common;
using Xunit;

namespace SP.Contract.Rest.Function.Test.Controllers
{
    public class ContractControllerTest : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private readonly CustomWebApplicationFactory<Startup> _factory;

        public ContractControllerTest(CustomWebApplicationFactory<Startup> factory)
        {
            _factory = factory;
        }

        [Fact]
        public async Task CreateContract_ReturnsResult()
        {
            var client = await _factory.GetAuthenticatedClientAsync();

            var obj = new CreateContractCommand(Guid.NewGuid(), null, 1, 420, 421, "C-1", DateTime.Now, DateTime.Now.AddDays(10));

            var response = await client.PostAsync(
                "/api/v1/contracts",
                new JsonContent<CreateContractCommand>(obj));

            response.EnsureSuccessStatusCode();

            var content = await Utilities.GetResponseContentAsync<ProcessingResult<Guid>>(response);

            content.ShouldNotBeNull();
        }

        [Fact]
        public async Task GetPageContracts_ReturnsResult()
        {
            var client = await _factory.GetAuthenticatedClientAsync();

            var obj = GetPageContractQuery.Create(
                new PageContext<ContractFilter>(1, 10));

            var response = await client.PostAsync(
                "/api/v1/contracts/page",
                new JsonContent<IPageContext<ContractFilter>>(obj.PageContext));

            response.EnsureSuccessStatusCode();

            var content = await Utilities.GetResponseContentAsync<CollectionViewModel<ContractDto>>(response);

            content.ShouldNotBeNull();
        }

        [Theory]
        [InlineData("23dafc17-19f4-48fd-a355-c9c802702485")]
        public async Task GetPageContract_ReturnsResult(Guid id)
        {
            var client = await _factory.GetAuthenticatedClientAsync();

            var response = await client.GetAsync($"/api/v1/contracts/{id.ToString()}");

            response.EnsureSuccessStatusCode();

            var content = await Utilities.GetResponseContentAsync<ContractDto>(response);

            content.ShouldNotBeNull();
            content.Id.ShouldBe(id);
        }

        [Theory]
        [InlineData("23dafc17-19f4-48fd-a355-c9c802702485")]
        public async Task UpdateContract_ReturnsResult(Guid id)
        {
            var client = await _factory.GetAuthenticatedClientAsync();

            var obj = new UpdateContractCommand(
                id,
                null,
                1,
                420,
                421,
                "C-2",
                DateTime.Now,
                DateTime.Now.AddDays(10));

            var response = await client.PutAsync(
                "/api/v1/contracts",
                new JsonContent<UpdateContractCommand>(obj));

            response.EnsureSuccessStatusCode();

            var content = await Utilities.GetResponseContentAsync<ProcessingResult<bool>>(response);

            content.ShouldNotBeNull();
        }

        [Theory]
        [InlineData("23dafc17-19f4-48fd-a355-c9c802702485")]
        public async Task DeleteContract_ReturnsResult(Guid id)
        {
            var client = await _factory.GetAuthenticatedClientAsync();

            var obj = new DeleteContractCommand(id);

            var response = await client.DeleteAsync(string.Format("/api/v1/contracts/{0}", id));

            response.EnsureSuccessStatusCode();

            var content = await Utilities.GetResponseContentAsync<ProcessingResult<bool>>(response);

            content.ShouldNotBeNull();
        }
    }
}
