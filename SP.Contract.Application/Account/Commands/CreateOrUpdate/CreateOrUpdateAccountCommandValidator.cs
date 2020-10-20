using FluentValidation;

namespace SP.Contract.Application.Account.Commands.CreateOrUpdate
{
    public class CreateOrUpdateAccountCommandValidator : AbstractValidator<CreateOrUpdateAccountCommand>
    {
        public CreateOrUpdateAccountCommandValidator()
        {
            RuleFor(x => x.Accounts).NotEmpty();
        }
    }
}
