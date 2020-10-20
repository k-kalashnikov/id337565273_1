using System.Linq.Dynamic.Core;
using System.Threading;
using MediatR;
using Moq;

namespace SP.Contract.Persistence.Integration.Tests.Common
{
    public static class MockFactory
    {
        public static IMediator CreateMediatorMock<T>()
        {
            var repository = new MockRepository(MockBehavior.Default);
            var mediator = repository.Create<IMediator>();
            mediator.Setup(x => x.Publish(It.IsAny<T>(), It.IsAny<CancellationToken>()))
                .Verifiable(string.Empty);
            return repository.Of<IMediator>().First();
        }
    }
}
