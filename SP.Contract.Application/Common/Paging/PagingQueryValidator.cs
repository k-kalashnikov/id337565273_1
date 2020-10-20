using FluentValidation;

namespace SP.Contract.Application.Common.Paging
{
    public class PagingQueryValidator<T, TM, TF> : AbstractValidator<T>
        where T : PagingQuery<TM, TF>
        where TF : class, new()
    {
        public PagingQueryValidator()
        {
            RuleFor(x => x.PageContext).NotNull();
            RuleFor(x => x.PageContext.PageIndex).GreaterThan(0);
            RuleFor(x => x.PageContext.PageSize).GreaterThan(0);
        }
    }
}
