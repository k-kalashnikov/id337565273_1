using FluentValidation;

namespace SP.Contract.Application.Organization.Commands.CreateOrUpdate
{
    public class CreateOrUpdateOrganizationValidator : AbstractValidator<CreateOrUpdateOrganization>
    {
        public CreateOrUpdateOrganizationValidator()
        {
            RuleFor(x => x.Organizations).NotEmpty();
        }
    }
}
