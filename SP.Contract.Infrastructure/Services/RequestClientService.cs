using System;
using System.Threading;
using System.Threading.Tasks;
using GreenPipes;
using MassTransit;
using SP.Contract.Application;
using SP.Market.Identity.Common.Interfaces;

namespace SP.Contract.Infrastructure.Services
{
    public class RequestClientService<TRequest, TResponse> : IRequestClientService<TRequest, TResponse>
        where TRequest : class
        where TResponse : class
    {
        private readonly IRequestClient<TRequest> _client;
        private readonly ICurrentUserService _currentUserService;

        public RequestClientService(
            IRequestClient<TRequest> client,
            ICurrentUserService currentUserService)
        {
            _client = client ?? throw new ArgumentNullException(nameof(client));
            _currentUserService = currentUserService ?? throw new ArgumentNullException(nameof(currentUserService));
        }

        public async Task<TResponse> GetResponseAsync(TRequest request, CancellationToken cancellationToken)
        {
            var jwt = _currentUserService.GetCurrentUser()?.SecurityToken;

            using var service = _client.Create(request, cancellationToken);
            service.UseExecute(x => x.Headers.Set("Authorization", jwt));

            var response = await service.GetResponse<TResponse>();
            return response.Message;
        }

        public async Task<TResponse[]> GetResponsesAsync(TRequest request, CancellationToken cancellationToken)
        {
            var jwt = _currentUserService.GetCurrentUser()?.SecurityToken;

            using var service = _client.Create(request, cancellationToken);
            service.UseExecute(x => x.Headers.Set("Authorization", jwt));

            var response = await _client.GetResponse<TResponse[]>(request, cancellationToken);
            return response.Message;
        }
    }
}
