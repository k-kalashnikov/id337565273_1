using Xunit;

namespace SP.Contract.Application.Test.Common
{
    [CollectionDefinition("QueryCollection")]
    public class QueryCollection : ICollectionFixture<QueryTestFixture>
    {
    }
}
