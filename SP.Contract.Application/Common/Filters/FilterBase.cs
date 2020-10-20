using System.Collections.Generic;

namespace SP.Contract.Application.Common.Filters
{
    public abstract class FilterBase
    {
        public List<FilterFieldValue> TextFilters { get; set; }
    }
}
