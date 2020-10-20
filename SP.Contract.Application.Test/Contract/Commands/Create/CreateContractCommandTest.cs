using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Shouldly;
using SP.Contract.Application.Common.Response;
using SP.Contract.Application.Contract.Commands.Create;
using SP.Contract.Application.Contract.Commands.Delete;
using SP.Contract.Application.Test.Common;
using SP.Market.EventBus.RMQ.Shared.Models.Platform.Contracts.UpdatePurchaseContract;
using SP.Market.EventBus.RMQ.Shared.Models.Platform.Purchases;
using SP.Market.EventBus.RMQ.Shared.Models.Platform.Purchases.GetPurchases;
using Xunit;

namespace SP.Contract.Application.Test.Contract.Commands.Create
{
    [Collection("QueryCollection")]
    public class CreateContractCommandTest : CommandTestBase
    {
        private readonly IRequestClientService<GetPurchasesRequest, GetPurchasesResponse> _purchaseRequestClientService;
        private readonly IRequestClientService<UpdatePurchaseContractRequest, UpdatePurchaseContractResponse> _purchaseUpdateContractRequestClientService;

        public CreateContractCommandTest(QueryTestFixture fixture)
            : base(fixture)
        {
            _purchaseRequestClientService =
                MockFactory.CreateRequestClientService<GetPurchasesRequest, GetPurchasesResponse>(
                    new GetPurchasesResponse(new[]
                    {
                        new PurchaseDto(
                            Guid.Parse("e52e44ff-91dc-4257-aaac-65ef0ec1c70d"),
                            "тест закупка",
                            2041,
                            "2041",
                            DateTime.Now,
                            DateTime.Now,
                            5,
                            10,
                            11,
                            12,
                            438915,
                            6,
                            7,
                            null,
                            null,
                            new PurchaseStatusDto(
                                7,
                                "Победитель определен",
                                false,
                                "document.purchase.status.hasWinner"))
                    }));
            _purchaseUpdateContractRequestClientService =
                MockFactory.CreateRequestClientService<UpdatePurchaseContractRequest, UpdatePurchaseContractResponse>(
                    new UpdatePurchaseContractResponse(true, new string[] { }));
        }

        // Создание прейскурантного договора
        [Fact]
        public async Task CreateContractTypePriceListAsync()
        {
            var handler = new CreateContractCommandHandler(
                Context,
                CurrentUserService,
                MockFactory.CreateMediatorMock(),
                _purchaseRequestClientService,
                _purchaseUpdateContractRequestClientService);

            var command = new CreateContractCommand(
                Guid.Parse("e52e44ff-91dc-4257-aaac-65ef0ec1c70d"),
                null,
                2,
                438915,
                420,
                "11134122",
                DateTime.Now,
                DateTime.Now);

            var result = await handler.Handle(
                command,
                CancellationToken.None);

            result.ShouldNotBeNull();
            result.Errors.Length.ShouldBe(0);
        }

        // Создание рамочного договора
        [Fact]
        public async Task CreateContractTypeFrameAsync()
        {
            var handler = new CreateContractCommandHandler(
                Context,
                CurrentUserService,
                MockFactory.CreateMediatorMock(),
                _purchaseRequestClientService,
                _purchaseUpdateContractRequestClientService);

            var command = new CreateContractCommand(
                null,
                null,
                1,
                438915,
                420,
                "11134122",
                DateTime.Now,
                DateTime.Now);

            var result = await handler.Handle(
                command,
                CancellationToken.None);

            result.ShouldNotBeNull();
            result.Errors.Length.ShouldBe(0);
        }
    }
}
