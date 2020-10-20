namespace SP.Contract.Application.Common.Filters
{
    public class FilterFieldValue
    {
        public FilterFieldValue()
        {
        }

        public FilterFieldValue(string field, EnumFilterOperations? operation, string value)
        {
            Field = field;
            Operation = operation;
            Value = value;
        }

        public string Field { get; set; }

        public EnumFilterOperations? Operation { get; set; }

        public string Value { get; set; }
    }
}
