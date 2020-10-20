using FluentValidation;

namespace SP.Contract.Application.Contract.Queries.Get
{
    public class GetContractQueryValidator : AbstractValidator<GetContractQuery>
    {
        public GetContractQueryValidator()
        {
            RuleFor(x => x.Id).NotEmpty();
        }
    }
}
