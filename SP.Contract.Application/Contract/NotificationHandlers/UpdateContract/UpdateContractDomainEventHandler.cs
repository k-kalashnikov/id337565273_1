using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MassTransit;
using MediatR;
using Serilog;
using SP.Contract.Domains.AggregatesModel.Contract.Notifications;
using SP.Contract.Events.Event.UpdateContract;

namespace SP.Contract.Application.Contract.NotificationHandlers.UpdateContract
{
    public class CreateContractDomainEventHandler
        : INotificationHandler<UpdateContractNotification>
    {
        private readonly IPublishEndpoint _publishEndpoint;
        private readonly IMapper _mapper;

        public CreateContractDomainEventHandler(IPublishEndpoint publishEndpoint, IMapper mapper)
        {
            _publishEndpoint = publishEndpoint ?? throw new ArgumentNullException(nameof(publishEndpoint));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task Handle(UpdateContractNotification notification, CancellationToken cancellationToken)
        {
            Log.Information($"Send an event {nameof(UpdateContractEvent)}");

            await _publishEndpoint.Publish(
                _mapper.Map<UpdateContractEvent>(notification),
                cancellationToken);
        }
    }
}
