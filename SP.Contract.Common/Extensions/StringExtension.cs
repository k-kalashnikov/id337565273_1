namespace SP.Contract.Common.Extensions
{
    public static class StringExtension
    {
        public static string RemoveWhitespace(this string str) =>
            string.Join(" ", str.Split(new[] { '\r', '\n', '\t' }));

        public static string LikeWildcardBoth(this string item)
            => string.IsNullOrEmpty(item) ? null : $"%{item}%";

        public static string LikeWildcardStarts(this string item)
            => string.IsNullOrEmpty(item) ? null : $"{item}%";

        public static string LikeWildcardEnds(this string item)
            => string.IsNullOrEmpty(item) ? null : $"%{item}";
    }
}
