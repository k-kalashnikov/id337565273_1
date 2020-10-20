using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Shouldly;
using SP.Contract.Application.Common.Response;
using SP.Contract.Application.Contract.Commands.Delete;
using SP.Contract.Application.Contract.Commands.Update;
using SP.Contract.Application.Test.Common;
using Xunit;

namespace SP.Contract.Application.Test.Contract.Commands.Delete
{
    [Collection("QueryCollection")]
    public class DeleteContractCommandTest : CommandTestBase
    {
        public DeleteContractCommandTest(QueryTestFixture fixture)
            : base(fixture)
        {
        }

        [Fact]
        public async Task DeleteContractAsync()
        {
            var contract = await Context.Contracts.FirstOrDefaultAsync(c => c.Number == "Д-112");

            var handler = new DeleteContractCommandHandler(Context, CurrentUserService, MockFactory.CreateMediatorMock());
            {
                var result = await handler.Handle(
                    new DeleteContractCommand(
                        contract.Id),
                    CancellationToken.None);

                result.ShouldBeOfType<ProcessingResult<bool>>();
                result.ShouldNotBeNull();
            }
        }
    }
}
