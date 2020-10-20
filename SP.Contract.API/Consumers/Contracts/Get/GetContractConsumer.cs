using System.Threading.Tasks;
using AutoMapper;
using MassTransit;
using Serilog;
using SP.Contract.Application.Contract.Queries.Get;
using SP.Contract.Events.Request.GetContract;
using ContractDto = SP.Contract.Events.Models.ContractDto;

namespace SP.Contract.API.Consumers.Contracts.Get
{
    internal class GetContractConsumer : ConsumerBase<GetContractRequest>
    {
        private readonly IMapper _autoMapper;

        public GetContractConsumer(MediatR.IMediator mediator, IMapper mapper)
            : base(mediator)
        {
            _autoMapper = mapper;
        }

        public override async Task Consume(ConsumeContext<GetContractRequest> context)
        {
            Log.Information($"Request {nameof(GetContractRequest)} received {context.Message.ContractId}");

            var query = GetContractQuery.Create(context.Message.ContractId);
            var contractDto = await Mediator.Send(query, context.CancellationToken);
            var response = new GetContractResponse(_autoMapper.Map<ContractDto>(contractDto));
            await context.RespondAsync(response);
        }
    }
}
