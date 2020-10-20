namespace SP.Contract.Application.Common.Filters
{
    public class FilterFieldDefinition<T>
    {
        public T Value { get; set; }

        public T[] Values { get; set; }

        public EnumFilterOperations Operation { get; set; }

        public FilterRangeDefinition<T> Range { get; set; }
    }
}
