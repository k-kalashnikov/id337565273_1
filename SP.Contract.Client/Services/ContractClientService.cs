using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Grpc.Net.Client;
using SP.Contract.API;
using SP.Contract.Client.Interfaces;
using SP.Contract.Client.Models;

namespace SP.Contract.Client.Services
{
    public class ContractClientService : IContractClientService
    {
        private readonly IContractClientOptionsService _options;
        private ContractGrpcService.ContractGrpcServiceClient _client;

        public ContractClientService(IContractClientOptionsService options)
        {
            AppContext.SetSwitch("System.Net.Http.SocketsHttpHandler.Http2UnencryptedSupport", true);

            _options = options;

            var httpClientHandler = new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator
            };
            var httpClient = new HttpClient(httpClientHandler);
            var channel = GrpcChannel.ForAddress(
                _options.ContractClientOptions.Uri,
                new GrpcChannelOptions
                {
                    HttpClient = httpClient,
                    MaxReceiveMessageSize = _options.ContractClientOptions.MaxReceiveMessageSize,
                    MaxSendMessageSize = _options.ContractClientOptions.MaxSendMessageSize
                });

            _client = new ContractGrpcService.ContractGrpcServiceClient(channel);
        }

        public async Task<IEnumerable<ContractDto>> GetContractsAsync(CancellationToken cancellationToken = default)
        {
            var request = new Google.Protobuf.WellKnownTypes.Empty();

            var organizationTypes = await _client.GetAllContractsAsync(request, null, null, cancellationToken);

            return organizationTypes.Items.Select(m => new ContractDto()
            {
                Id = m.Id,
                FinishDate = m.FinishDate,
                Number = m.Number,
                ParentId = m.Parent,
                StartDate = m.StartDate,
                CustomerOrganizationId = m.CustomerOrganizationId,
                ContractorOrganizationId = m.ContractorOrganizationId,
                CreatedBy = new AccountDto
                {
                    Id = m.Account.Id,
                    FirstName = m.Account.FirstName,
                    LastName = m.Account.LastName,
                    MiddleName = m.Account.MiddleName,
                    OrganizationId = m.Account.OrganizationId
                }
            });
        }
    }
}
