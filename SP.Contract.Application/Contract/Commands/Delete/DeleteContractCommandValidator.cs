using FluentValidation;

namespace SP.Contract.Application.Contract.Commands.Delete
{
    public class DeleteContractCommandValidator : AbstractValidator<DeleteContractCommand>
    {
        public DeleteContractCommandValidator()
        {
            RuleFor(x => x.ContractId).NotEmpty();
        }
    }
}
