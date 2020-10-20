using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Shouldly;
using SP.Contract.Application.Common.Paging;
using SP.Contract.Application.Contract.Models;
using SP.Contract.Application.Contract.Queries.GetPage;
using SP.Contract.Application.Test.Common;
using Xunit;

namespace SP.Contract.Application.Test.Contract.Queries.GetPage
{
    [Collection("QueryCollection")]
    public class GetPageContractQueryTest : CommandTestBase
    {
        public GetPageContractQueryTest(QueryTestFixture fixture)
            : base(fixture)
        {
        }

        [Fact]
        public async Task GetPageContractAsync()
        {
            var handler = new GetPageContractQueryHandler(Context, CurrentUserService, AutoMapper);

            var result = await handler.Handle(
                new GetPageContractQuery(new PageContext<ContractFilter>(1, 10)),
                CancellationToken.None);

            result.ShouldBeOfType<CollectionViewModel<ContractDto>>();
            result.TotalCount.ShouldBeGreaterThanOrEqualTo(3);
        }

        [Fact]
        public async Task GetPageContractByContractorAsync()
        {
            var handler = new GetPageContractQueryHandler(
                Context,
                MockFactory.CreateCurrentUserServiceMock(3, 2, "contractor.module.platform"),
                AutoMapper);

            var result = await handler.Handle(
                new GetPageContractQuery(new PageContext<ContractFilter>(1, 10)),
                CancellationToken.None);

            result.ShouldBeOfType<CollectionViewModel<ContractDto>>();
            result.TotalCount.ShouldBe(1);
        }

        [Fact]
        public async Task GetPageContractFilterNumberAsync()
        {
            var handler = new GetPageContractQueryHandler(Context, CurrentUserService, AutoMapper);

            var filter = new ContractFilter()
            {
                Number = new Service.Common.Filters.Filters.FilterFieldDefinition<string>
                {
                    Operation = Service.Common.Filters.Filters.EnumFilterOperations.Equal,
                    Value = "Д-112"
                }
            };
            var result = await handler.Handle(
                new GetPageContractQuery(new PageContext<ContractFilter>(1, 10, null, null, filter)),
                CancellationToken.None);

            result.ShouldBeOfType<CollectionViewModel<ContractDto>>();
            result.TotalCount.ShouldBe(1);
        }

        [Fact]
        public async Task GetPageContractFilterCustomerAsync()
        {
            var handler = new GetPageContractQueryHandler(Context, CurrentUserService, AutoMapper);

            var filter = new ContractFilter()
            {
                CustomerName = new Service.Common.Filters.Filters.FilterFieldDefinition<string>
                {
                    Operation = Service.Common.Filters.Filters.EnumFilterOperations.Contains,
                    Value = "Се"
                }
            };
            var result = await handler.Handle(
                new GetPageContractQuery(new PageContext<ContractFilter>(1, 10, null, null, filter)),
                CancellationToken.None);

            result.ShouldBeOfType<CollectionViewModel<ContractDto>>();
            result.TotalCount.ShouldBe(2);
        }

        [Fact]
        public async Task GetPageContractFilterContractorAsync()
        {
            var handler = new GetPageContractQueryHandler(Context, CurrentUserService, AutoMapper);

            var filter = new ContractFilter()
            {
                ContractorName = new Service.Common.Filters.Filters.FilterFieldDefinition<string>
                {
                    Operation = Service.Common.Filters.Filters.EnumFilterOperations.Equal,
                    Value = "Поставщик"
                }
            };
            var result = await handler.Handle(
                new GetPageContractQuery(new PageContext<ContractFilter>(1, 10, null, null, filter)),
                CancellationToken.None);

            result.ShouldBeOfType<CollectionViewModel<ContractDto>>();
            result.TotalCount.ShouldBe(1);
        }

        [Fact]
        public async Task GetPageContractFilterContractTypeAsync()
        {
            var handler = new GetPageContractQueryHandler(Context, CurrentUserService, AutoMapper);

            var filter = new ContractFilter()
            {
                ContractTypeName = new Service.Common.Filters.Filters.FilterFieldDefinition<string>
                {
                    Operation = Service.Common.Filters.Filters.EnumFilterOperations.Equal,
                    Value = "Рамочный"
                }
            };
            var result = await handler.Handle(
                new GetPageContractQuery(new PageContext<ContractFilter>(1, 10, null, null, filter)),
                CancellationToken.None);

            result.ShouldBeOfType<CollectionViewModel<ContractDto>>();
            result.TotalCount.ShouldBeGreaterThanOrEqualTo(3);
        }

        [Fact]
        public async Task GetPageContractFilterContractStatusAsync()
        {
            var handler = new GetPageContractQueryHandler(Context, CurrentUserService, AutoMapper);

            var filter = new ContractFilter()
            {
                ContractStatusName = new Service.Common.Filters.Filters.FilterFieldDefinition<string>
                {
                    Operation = Service.Common.Filters.Filters.EnumFilterOperations.Equal,
                    Value = "Черновик"
                }
            };
            var result = await handler.Handle(
                new GetPageContractQuery(new PageContext<ContractFilter>(1, 10, null, null, filter)),
                CancellationToken.None);

            result.ShouldBeOfType<CollectionViewModel<ContractDto>>();
            result.TotalCount.ShouldBeGreaterThanOrEqualTo(3);
        }

        [Fact]
        public async Task GetPageContractFilterStartDateAsync()
        {
            var handler = new GetPageContractQueryHandler(Context, CurrentUserService, AutoMapper);

            var start = Context.Contracts.First().StartDate;
            var filter = new ContractFilter()
            {
                StartDate = new Service.Common.Filters.Filters.FilterFieldDefinition<DateTime>
                {
                    Operation = Service.Common.Filters.Filters.EnumFilterOperations.Equal,
                    Value = start
                }
            };
            var result = await handler.Handle(
                new GetPageContractQuery(new PageContext<ContractFilter>(1, 10, null, null, filter)),
                CancellationToken.None);

            result.ShouldBeOfType<CollectionViewModel<ContractDto>>();
            result.TotalCount.ShouldBeGreaterThan(0);
        }

        [Fact]
        public async Task GetPageContractFilterFinishDateAsync()
        {
            var handler = new GetPageContractQueryHandler(Context, CurrentUserService, AutoMapper);

            var finish = Context.Contracts.First().FinishDate;
            var filter = new ContractFilter()
            {
                FinishDate = new Service.Common.Filters.Filters.FilterFieldDefinition<DateTime>
                {
                    Operation = Service.Common.Filters.Filters.EnumFilterOperations.Equal,
                    Value = finish
                }
            };
            var result = await handler.Handle(
                new GetPageContractQuery(new PageContext<ContractFilter>(1, 10, null, null, filter)),
                CancellationToken.None);

            result.ShouldBeOfType<CollectionViewModel<ContractDto>>();
            result.TotalCount.ShouldBeGreaterThan(0);
        }

        [Fact]
        public async Task GetPageContractFilterCreatedByAsync()
        {
            var handler = new GetPageContractQueryHandler(Context, CurrentUserService, AutoMapper);

            var filter = new ContractFilter()
            {
                CreatedBy = new Service.Common.Filters.Filters.FilterFieldDefinition<string>
                {
                    Operation = Service.Common.Filters.Filters.EnumFilterOperations.Contains,
                    Value = "Менеджер"
                }
            };
            var result = await handler.Handle(
                new GetPageContractQuery(new PageContext<ContractFilter>(1, 10, null, null, filter)),
                CancellationToken.None);

            result.ShouldBeOfType<CollectionViewModel<ContractDto>>();
            result.TotalCount.ShouldBe(3);
        }

        [Fact]
        public async Task GetPageContractFilterCreatedAsync()
        {
            var handler = new GetPageContractQueryHandler(Context, CurrentUserService, AutoMapper);

            var created = Context.Contracts.First().Created;
            var filter = new ContractFilter()
            {
                Created = new Service.Common.Filters.Filters.FilterFieldDefinition<DateTime>
                {
                    Operation = Service.Common.Filters.Filters.EnumFilterOperations.Equal,
                    Value = created
                }
            };
            var result = await handler.Handle(
                new GetPageContractQuery(new PageContext<ContractFilter>(1, 10, null, null, filter)),
                CancellationToken.None);

            result.ShouldBeOfType<CollectionViewModel<ContractDto>>();
            result.TotalCount.ShouldBeGreaterThan(0);
        }

        [Fact]
        public async Task GetPageContractByManagerAsync()
        {
            var handler = new GetPageContractQueryHandler(
                Context,
                MockFactory.CreateCurrentUserServiceMock(2, 1, "manager.module.market"),
                AutoMapper);

            var result = await handler.Handle(
                new GetPageContractQuery(new PageContext<ContractFilter>(1, 10)),
                CancellationToken.None);

            result.ShouldBeOfType<CollectionViewModel<ContractDto>>();
            result.TotalCount.ShouldBe(2);
        }
    }
}