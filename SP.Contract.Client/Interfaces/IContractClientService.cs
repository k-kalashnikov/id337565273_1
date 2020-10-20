using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using SP.Contract.Client.Models;

namespace SP.Contract.Client.Interfaces
{
    public interface IContractClientService
    {
        Task<IEnumerable<ContractDto>> GetContractsAsync(CancellationToken cancellationToken = default);
    }
}
