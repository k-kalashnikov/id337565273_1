using System;
using System.Collections.Generic;

namespace SP.Contract.Application.Common.Paging
{
    public class PageContext<TFilter> : IPageContext<TFilter>
        where TFilter : class, new()
    {
        public PageContext(
            int pageIndex,
            int pageSize,
            IEnumerable<SortDescriptor> listSort = null,
            IEnumerable<GroupDescriptor> listGroup = null,
            TFilter filter = null)
        {
            PageIndex = pageIndex;
            PageSize = pageSize;
            Filter = filter ?? new TFilter();
            ListSort = listSort ?? Array.Empty<SortDescriptor>();
        }

        public int PageIndex { get; set; }

        public int PageSize { get; set; }

        public TFilter Filter { get; set; }

        public IEnumerable<SortDescriptor> ListSort { get; set; }

        public bool IsValid()
        {
            return PageIndex > 0 && PageSize > 0 &&
                Filter != null && ListSort != null;
        }
    }
}
