using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Shouldly;
using SP.Contract.Application.Common.Response;
using SP.Contract.Application.ContractDocuments.Commands.DeleteContractDocumentsByContractIdCommand;
using SP.Contract.Application.Test.Common;
using Xunit;

namespace SP.Contract.Application.Test.ContractDocument.Commands
{
    [Collection("QueryCollection")]
    public class DeleteContractDocumentsByContractIdCommandTest : CommandTestBase
    {
        public DeleteContractDocumentsByContractIdCommandTest(QueryTestFixture fixture)
            : base(fixture)
        {
        }

        [Fact]
        public async Task DeleteContractDocumentsByContractIdAsync()
        {
            var contract = await Context.Contracts.FirstOrDefaultAsync(c => c.Number == "Д-112");

            var handler = new DeleteContractDocumentsByContractIdCommandHandler(Context, CurrentUserService);
            {
                var result = await handler.Handle(
                    new DeleteContractDocumentsByContractIdCommand(
                        contract.Id),
                    CancellationToken.None);

                result.ShouldBeOfType<ProcessingResult<bool>>();
                result.ShouldNotBeNull();
                result.Errors.ShouldBeEmpty();
            }
        }
    }
}
