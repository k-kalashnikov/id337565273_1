namespace SP.Contract.Application.Common.Filters
{
    /// <summary>
    /// Определение для фильтра для значений в диапазоне от и до
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class FilterRangeDefinition<T>
    {
        /// <summary>
        /// От какого значения
        /// </summary>
        public T FromValue { get; set; }

        /// <summary>
        /// До какого значения
        /// </summary>
        public T ToValue { get; set; }

        /// <summary>
        /// Строгое или нестрогое соответствие
        /// </summary>
        public bool Strict { get; set; }
    }
}
