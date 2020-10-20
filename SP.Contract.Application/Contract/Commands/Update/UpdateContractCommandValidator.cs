using System.Linq;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using SP.Contract.Application.Common.Exceptions;
using SP.Contract.Application.Common.Interfaces;
using SP.Contract.Application.Resources;
using DM = SP.Contract.Domains.AggregatesModel.Contract.Entities;

namespace SP.Contract.Application.Contract.Commands.Update
{
    public class UpdateContractCommandValidator : AbstractValidator<UpdateContractCommand>
    {
        public UpdateContractCommandValidator(IApplicationDbContext applicationDbContext)
        {
            RuleFor(x => x.Id).NotEmpty();
            RuleFor(x => x.ContractStatusId).GreaterThan(0);
            RuleFor(x => x.CustomerOrganizationId).GreaterThan(0);
            RuleFor(x => x.ContractorOrganizationId).GreaterThan(0);
            RuleFor(x => x.Number).NotEmpty();

            RuleFor(x => x.FinishDate).NotEmpty();

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

            RuleFor(x => x).CustomAsync(async (command, validationContext, cancellationToken) =>
            {
                var contract = await applicationDbContext
                    .Set<DM.Contract>()
                    .Include(c => c.ContractStatus)
                    .Include(c => c.ContractorOrganization)
                    .Include(c => c.CustomerOrganization)
                    .Where(pd => pd.Id == command.Id)
                    .SingleOrDefaultAsync(cancellationToken);

                if (contract == null)
                {
                    throw new NotFoundException(nameof(Contract), command.Id);
                }

                if (contract.ContractStatus.Id != DM.ContractStatus.Draft.Id)
                {
                    if (AreFieldsChanged(contract, command))
                    {
                        validationContext.AddFailure(
                            nameof(command.Id),
                            string.Format(Resources.Resource.ValidationError_DraftDenyFieldsEdit));
                    }
                }
            });
        }

        private static bool AreFieldsChanged(DM.Contract contract, UpdateContractCommand command)
        {
            return contract.ContractStatus.Id != command.ContractStatusId ||
                   contract.Number != command.Number ||
                   contract.ContractorOrganization.Id != command.ContractorOrganizationId ||
                   contract.CustomerOrganization.Id != command.CustomerOrganizationId;
        }
    }
}
