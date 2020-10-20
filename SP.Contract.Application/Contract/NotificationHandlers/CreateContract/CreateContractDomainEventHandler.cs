using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MassTransit;
using MediatR;
using Serilog;
using SP.Contract.Domains.AggregatesModel.Contract.Notifications;
using SP.Contract.Events.Event.CreateContract;

namespace SP.Contract.Application.Contract.NotificationHandlers.CreateContract
{
    public class CreateContractDomainEventHandler
        : INotificationHandler<CreateContractNotification>
    {
        private readonly IPublishEndpoint _publishEndpoint;
        private readonly IMapper _mapper;

        public CreateContractDomainEventHandler(IPublishEndpoint publishEndpoint, IMapper mapper)
        {
            _publishEndpoint = publishEndpoint ?? throw new ArgumentNullException(nameof(publishEndpoint));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task Handle(CreateContractNotification notification, CancellationToken cancellationToken)
        {
            Log.Information($"Send an event {nameof(CreateContractEvent)}");

            await _publishEndpoint.Publish(
                _mapper.Map<CreateContractEvent>(notification),
                cancellationToken);
        }
    }
}
