using FluentValidation;
using SP.Contract.Application.Resources;

namespace SP.Contract.Application.Contract.Commands.Create
{
    public class CreateContractCommandValidator : AbstractValidator<CreateContractCommand>
    {
        public CreateContractCommandValidator()
        {
            RuleFor(x => x.ContractTypeId).GreaterThan(0);
            RuleFor(x => x.CustomerOrganizationId).GreaterThan(0);
            RuleFor(x => x.ContractorOrganizationId).GreaterThan(0);
            RuleFor(x => x.Number).NotEmpty();

            RuleFor(x => x.StartDate)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotEmpty()
                .WithMessage(Resource.ValidationError_NotBeNull)
                .LessThanOrEqualTo(x => x.FinishDate)
                .WithMessage(Resource.ValidationError_ContractStartDate);

            RuleFor(x => x.FinishDate)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotNull()
                .WithMessage(Resource.ValidationError_NotBeNull)
                .GreaterThanOrEqualTo(x => x.StartDate)
                .WithMessage(Resource.ValidationError_ContractFinishDate);
        }
    }
}
