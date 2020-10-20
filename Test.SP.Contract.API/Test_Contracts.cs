using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FluentAssertions;
using SP.Contract.Application.Common.Paging;
using SP.Contract.Application.Contract.Models;
using Xunit;

namespace SP.Contract.API
{
    [Collection(WebAppApplicationClientFixture.Id)]
    [Trait(nameof(TestGroup), TestGroup.Integration)]
    public class Test_Contracts
    {
        private const string _baseUri = "api/v1/contracts";

        private static readonly IDictionary<string, IComparer<ContractDto>> _sortOrder =
            new Dictionary<string, IComparer<ContractDto>>
            {
                {"number", CreateComparer(dto => dto.Number)},
                {"contractorOrganization", CreateComparer(dto => dto.ContractorOrganization)},
                {"customerOrganization", CreateComparer(dto => dto.CustomerOrganization)},
                {"contractType.name", CreateComparer(dto => dto.ContractType.Name)},
                {"contractStatus.name", CreateComparer(dto => dto.ContractStatus.Name)},
                {"startDate", CreateComparer(dto => dto.StartDate)},
                {"finishDate", CreateComparer(dto => dto.FinishDate)},
                {"createdBy", CreateComparer(dto => dto.CreatedBy)},
                {"created", CreateComparer(dto => dto.Created)}
            };

        private readonly IWebApplicationClient _webApplicationClient;

        public Test_Contracts(WebApplicationClient<Startup> webApplicationClient)
        {
            _webApplicationClient = webApplicationClient;
        }

        [Fact]
        public void WebApplicationClient_ShouldBeInitialized()
        {
            _webApplicationClient.Should().NotBeNull();
        }

        [Theory]
        [InlineData("number")]
        [InlineData("contractorOrganization")]
        [InlineData("customerOrganization")]
        [InlineData("contractType.name")]
        [InlineData("contractStatus.name")]
        [InlineData("startDate")]
        [InlineData("finishDate")]
        [InlineData("createdBy")]
        [InlineData("created")]
        public async Task Page_WithSorting_ShouldReturnOrderedData(string sortField)
        {
            var result = await _webApplicationClient.PostAsync<CollectionViewModel<ContractDto>>(
                BuildUri("page"),
                new PageContext<ContractFilter>(
                    pageIndex: 1,
                    pageSize: 12,
                    listSort: new[] { new SortDescriptor(sortField) }
                )
            );

            result.Data.Should().BeInAscendingOrder(_sortOrder[sortField]);
        }

        private static Uri BuildUri(string path) =>
            new Uri($"{_baseUri}/{path}", UriKind.Relative);

        private static IComparer<ContractDto> CreateComparer(params Func<ContractDto, object>[] properties) =>
            new MultiPropertyComparer<ContractDto>(properties);
    }
}
