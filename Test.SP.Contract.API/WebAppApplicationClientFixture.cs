using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace SP.Contract.API
{
    [CollectionDefinition(WebAppApplicationClientFixture.Id)]
    public class WebAppApplicationClientFixture : ICollectionFixture<WebApplicationClient<Startup>>
    {
        internal const string Id = "{C88A0AB5-407F-4867-92BD-598762E361E6}";
    }
}
