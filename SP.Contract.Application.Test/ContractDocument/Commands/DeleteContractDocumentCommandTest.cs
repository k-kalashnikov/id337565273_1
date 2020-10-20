using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Shouldly;
using SP.Contract.Application.Common.Response;
using SP.Contract.Application.ContractDocuments.Commands.DeleteContractDocumentCommand;
using SP.Contract.Application.Test.Common;
using SP.Contract.Persistence;
using SP.Market.Identity.Common.Interfaces;
using Xunit;

namespace SP.Contract.Application.Test.ContractDocument.Commands
{
    [Collection("QueryCollection")]
    public class DeleteContractDocumentCommandTest
    {
        private readonly ApplicationDbContext _context;
        private readonly ICurrentUserService _currentUserService;

        public DeleteContractDocumentCommandTest(QueryTestFixture fixture)
        {
            _context = fixture.Context;
            _currentUserService = fixture.CurrentUserService;
        }

        [Fact]
        public async Task DeleteContractDocumentByContractIdAsync()
        {
            var handler = new DeleteContractDocumentCommandHandler(
                _context,
                _currentUserService);

            var contractDocument = await _context.ContractDocuments.FirstAsync();

            var result = await handler.Handle(
                new DeleteContractDocumentCommand() { ContractDocumentId = contractDocument.Id },
                CancellationToken.None);

            result.ShouldBeOfType<ProcessingResult<bool>>();
        }
    }
}
