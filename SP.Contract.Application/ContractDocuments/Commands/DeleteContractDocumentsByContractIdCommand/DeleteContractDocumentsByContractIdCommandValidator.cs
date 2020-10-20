using FluentValidation;

namespace SP.Contract.Application.ContractDocuments.Commands.DeleteContractDocumentsByContractIdCommand
{
    public class DeleteContractDocumentsByContractIdCommandValidator : AbstractValidator<DeleteContractDocumentsByContractIdCommand>
    {
        public DeleteContractDocumentsByContractIdCommandValidator()
        {
            RuleFor(x => x.ContractId).NotEmpty();
        }
    }
}
