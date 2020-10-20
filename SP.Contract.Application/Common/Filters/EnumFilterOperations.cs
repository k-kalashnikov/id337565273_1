using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace SP.Contract.Application.Common.Filters
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum EnumFilterOperations
    {
        [EnumMember(Value = "contains")]
        Contains,

        [EnumMember(Value = "notContains")]
        NotContains,

        [EnumMember(Value = "startsWith")]
        StartsWith,

        [EnumMember(Value = "endsWith")]
        EndsWith,

        [EnumMember(Value = "equal")]
        Equal,

        [EnumMember(Value = "notEqual")]
        NotEqual,

        [EnumMember(Value = "greaterThan")]
        GreaterThan,

        [EnumMember(Value = "greaterThenOrEqual")]
        GreaterThenOrEqual,

        [EnumMember(Value = "lessThan")]
        LessThan,

        [EnumMember(Value = "lessThanOrEqual")]
        LessThanOrEqual
    }
}
