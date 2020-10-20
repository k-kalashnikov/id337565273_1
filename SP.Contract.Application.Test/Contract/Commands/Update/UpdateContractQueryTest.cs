using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Shouldly;
using SP.Contract.Application.Common.Response;
using SP.Contract.Application.Contract.Commands.Update;
using SP.Contract.Application.Contract.Models;
using SP.Contract.Application.Contract.Queries.Get;
using SP.Contract.Application.Test.Common;
using Xunit;

namespace SP.Contract.Application.Test.Contract.Commands.Update
{
    [Collection("QueryCollection")]
    public class UpdateContractQueryTest : CommandTestBase
    {
        public UpdateContractQueryTest(QueryTestFixture fixture)
            : base(fixture)
        {
        }

        [Fact]
        public async Task UpdateContractContractAsync()
        {
            var contract = await Context.Contracts.SingleAsync(c => c.Number == "Д-112");

            var handler = new UpdateContractCommandHandler(Context, CurrentUserService, MockFactory.CreateMediatorMock());
            {
                var result = await handler.Handle(
                    new UpdateContractCommand(
                        contract.Id,
                        null,
                        Domains.AggregatesModel.Contract.Entities.ContractStatus.Draft.Id,
                        1,
                        2,
                        "Д-112",
                        DateTime.Now,
                        DateTime.Now.AddDays(10)),
                    CancellationToken.None);

                result.ShouldBeOfType<ProcessingResult<bool>>();
                result.ShouldNotBeNull();
                result.Result.ShouldBeTrue();
            }
        }
    }
}
