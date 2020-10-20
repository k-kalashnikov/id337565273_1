using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using MassTransit;
using Serilog;
using SP.Contract.Application.Contract.Queries.GetPage;
using SP.Contract.Events.Models;
using SP.Contract.Events.Request.GetOrganizationContracts;

namespace SP.Contract.API.Consumers.Contracts.Get
{
  public class GetOrganizationContractsConsumer : ConsumerBase<GetOrganizationContractsRequest>
  {
    private readonly IMapper _autoMapper;

    public GetOrganizationContractsConsumer(MediatR.IMediator mediator, IMapper mapper)
        : base(mediator)
    {
      _autoMapper = mapper;
    }

    public override async Task Consume(ConsumeContext<GetOrganizationContractsRequest> context)
    {
      Log.Information($"Request {nameof(GetOrganizationContractsRequest)} CustomerOrganizationId {context.Message.CustomerOrganizationId}");
      var query = GetPageContractQuery.Create(new Application.Common.Paging.PageContext<Application.Contract.Models.ContractFilter>(
        0,
        999999,
        null,
        null,
        new Application.Contract.Models.ContractFilter()
        {
          CustomerOrganizationId = context.Message.CustomerOrganizationId
        }));
      var contractDtos = await Mediator.Send(query, context.CancellationToken);
      var response = new GetOrganizationContractsResponse(contractDtos.Data.Select(m => _autoMapper.Map<ContractDto>(m)).ToArray());
      await context.RespondAsync(response);
    }
  }
}
