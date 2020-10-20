using MediatR;

namespace SP.Contract.Application.Common.Paging
{
    public class PagingQuery<TM, TF> : IRequest<TM>
        where TF : class, new()
    {
        public PagingQuery(IPageContext<TF> pageContext)
        {
            PageContext = pageContext;
        }

        public IPageContext<TF> PageContext { get; set; }
    }
}
