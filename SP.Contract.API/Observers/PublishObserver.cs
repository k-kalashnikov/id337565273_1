using System;
using System.Threading.Tasks;
using MassTransit;
using Serilog;
using SP.Market.Identity.Common.Interfaces;

namespace SP.Contract.API.Observers
{
    public class PublishObserver : IPublishObserver
    {
        private readonly ICurrentUserService _currentUserService;

        public PublishObserver(ICurrentUserService currentUserService)
        {
            _currentUserService = currentUserService;
        }

        public Task PostPublish<T>(PublishContext<T> context)
            where T : class
        {
            return Task.CompletedTask;
        }

        public Task PrePublish<T>(PublishContext<T> context)
            where T : class
        {
            var jwt = _currentUserService.GetCurrentUser()?.SecurityToken;
            context.Headers.Set("Authorization", jwt);

            Log.Information($"**** {nameof(PublishObserver)} {nameof(PrePublish)} JWT: {jwt}");
            return Task.CompletedTask;
        }

        public Task PublishFault<T>(PublishContext<T> context, Exception exception)
            where T : class
        {
            return Task.CompletedTask;
        }
    }
}
