using System.Threading;
using System.Threading.Tasks;

namespace SP.Contract.Application
{
    public interface IRequestClientService<in TRequest, TResponse>
    {
        Task<TResponse> GetResponseAsync(TRequest request, CancellationToken cancellationToken);

        Task<TResponse[]> GetResponsesAsync(TRequest request, CancellationToken cancellationToken);
    }
}
