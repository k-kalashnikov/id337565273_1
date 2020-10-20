using FluentValidation;

namespace SP.Contract.Application.ContractDocuments.Commands.LoadContractDocumentByContractId
{
    public class LoadContractDocumentByContractIdCommandValidator : AbstractValidator<LoadContractDocumentByContractIdCommand>
    {
        public LoadContractDocumentByContractIdCommandValidator()
        {
            RuleFor(x => x.ContractId).NotEmpty();
            RuleFor(x => x.Data).NotEmpty();
            RuleFor(x => x.Name).NotEmpty();
        }
    }
}
