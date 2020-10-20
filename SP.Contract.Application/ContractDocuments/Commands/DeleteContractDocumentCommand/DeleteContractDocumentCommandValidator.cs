using FluentValidation;

namespace SP.Contract.Application.ContractDocuments.Commands.DeleteContractDocumentCommand
{
    public class DeleteContractDocumentCommandValidator : AbstractValidator<DeleteContractDocumentCommand>
    {
        public DeleteContractDocumentCommandValidator()
        {
            RuleFor(x => x.ContractDocumentId).NotEmpty();
        }
    }
}
