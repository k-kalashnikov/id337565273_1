using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using SP.Contract.Application.Settings;
using SP.FileStorage.Client.Services;

namespace SP.Contract.API.Services
{
    public class BucketCheck : BackgroundService
    {
        private readonly IEnumerable<string> _buckets;
        private readonly IServiceScopeFactory _serviceScopeFactory;

        public BucketCheck(IOptions<ContractSettings> options, IServiceScopeFactory serviceScopeFactory)
        {
            _buckets = options?.Value?.Buckets.AsEnumerable();

            _serviceScopeFactory = serviceScopeFactory
                                        ?? throw new ArgumentNullException(nameof(serviceScopeFactory));
        }

        protected async override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            using (var scope = _serviceScopeFactory.CreateScope())
            {
                IFileStorageClientService fileStorageClientService = scope.ServiceProvider.GetRequiredService<IFileStorageClientService>();

                var loadBuckets = await fileStorageClientService.GetCollectionBucketAsync(stoppingToken);
                foreach (var bucket in _buckets)
                {
                    // проверяем на существование бакетов, если нет создаем
                    if (!loadBuckets.Any(x => x.Name == bucket))
                    {
                        await fileStorageClientService.CreateBucketAsync(bucket, stoppingToken);
                    }
                }
            }
        }
    }
}
