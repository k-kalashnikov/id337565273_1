using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using SP.Contract.Application.Common.Paging;

namespace SP.Contract.Application.Common.Extensions
{
    public static class LinqExtensions
    {
        public static IOrderedQueryable<TSource> CustomOrderBy<TSource>(
            this IQueryable<TSource> source, ICollection<SortDescriptor> listSort)
        {
            if (!listSort.Any())
            {
                return (IOrderedQueryable<TSource>)source;
            }

            var ordering = listSort.FirstOrDefault()?.ToString();
            var first = source.OrderBy(ParsingConfig.Default, ordering);
            listSort.Remove(listSort.FirstOrDefault());

            IOrderedQueryable<TSource> then = first;
            foreach (var listSortItem in listSort)
            {
                then = then.ThenBy(listSortItem.ToString());
            }

            return then;
        }

        public static IEnumerable<TSource> DistinctBy<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector)
        {
            HashSet<TKey> seenKeys = new HashSet<TKey>();
            foreach (TSource element in source)
            {
                if (seenKeys.Add(keySelector(element)))
                {
                    yield return element;
                }
            }
        }
    }
}
