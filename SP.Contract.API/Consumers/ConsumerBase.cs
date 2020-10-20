using System.Threading.Tasks;
using MassTransit;
using IMediator = MediatR.IMediator;

namespace SP.Contract.API.Consumers
{
    public abstract class ConsumerBase<T> : IConsumer<T>
        where T : class
    {
        protected IMediator Mediator { get; }

        protected ConsumerBase(MediatR.IMediator mediator)
        {
            Mediator = mediator;
        }

        public abstract Task Consume(ConsumeContext<T> context);
    }
}
