using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Caching.Memory;
using Moq;
using SP.Market.Identity.Common.Interfaces;

namespace SP.Contract.Application.Test.Common
{
    public static class MockFactory
    {
        private static readonly Lazy<MockRepository> MockRepository = new Lazy<MockRepository>(() => new MockRepository(MockBehavior.Default));

        public static MockRepository MockRepositoryInstance => MockRepository.Value;

        public static IMemoryCache CreateMemoryCacheMock<T>()
        {
            var value = new object();
            var memoryCache = MockRepositoryInstance.Create<IMemoryCache>();
            memoryCache.Setup(x => x.TryGetValue(It.IsAny<string>(), out value)).Returns(null);

            // TODO Moq не умеет мокать статические методы, сделать врапер
            // https://stackoverflow.com/questions/2295960/mocking-extension-methods-with-moq
            return MockRepositoryInstance.Of<IMemoryCache>().First();
        }

        public static ICurrentUserService CreateCurrentUserServiceMock(
            long id = 1,
            long? organizationId = null,
            string role = "superuser.module.platform",
            string secretKey = null)
        {
            ICurrentUser currentUser = CurrentUser.Create(
                "stec.superuser@mail.ru",
                "test",
                "test",
                "test",
                id,
                organizationId,
                secretKey,
                new[] { role });

            return MockRepositoryInstance.Of<ICurrentUserService>()
                .Where(x => x.GetCurrentUser() == currentUser).First();
        }

        public static IMediator CreateMediatorMock()
        {
            var mediator = MockRepositoryInstance.Create<IMediator>();
            mediator.Setup(x => x.Publish(It.IsAny<object>(), It.IsAny<CancellationToken>()))
                .Verifiable(string.Empty);

            return MockRepositoryInstance.Of<IMediator>().First();
        }

        public static IRequestClientService<TRequest, TResponse> CreateRequestClientService<TRequest, TResponse>(TResponse result)
        {
            var mock = new Mock<IRequestClientService<TRequest, TResponse>>();
            mock.Setup(x => x.GetResponseAsync(It.IsAny<TRequest>(), It.IsAny<CancellationToken>())).Returns(Task.FromResult(result));
            return mock.Object;
        }
    }
}
