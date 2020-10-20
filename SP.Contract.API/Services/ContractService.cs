using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using MediatR;
using SP.Contract.Application.Contract.Queries.GetAll;

namespace SP.Contract.API.Services
{
    public class ContractService : ContractGrpcService.ContractGrpcServiceBase
    {
        private readonly IMediator _mediator;

        public ContractService(IMediator mediator)
        {
            _mediator = mediator;
        }

        public override async Task<Contracts> GetAllContracts(Empty request, ServerCallContext context)
        {
            var getAllQuery = GetAllContractsQuery.Create();

            var resultGetAllQuery = await _mediator.Send(getAllQuery);

            return new Contracts()
            {
                Items =
                {
                    resultGetAllQuery.Select(m => new Contract()
                    {
                        Id = m.Id.ToString(),
                        FinishDate = m.FinishDate.ToString(),
                        Number = m.Number,
                        Parent = m.ParentId.ToString(),
                        StartDate = m.StartDate.ToString(),
                        CustomerOrganizationId = m.CustomerOrganization.ID,
                        ContractorOrganizationId = m.ContractorOrganization.ID,
                        Account = new Account
                        {
                            Id = m.CreatedBy.Id,
                            FirstName = m.CreatedBy.FirstName ?? string.Empty,
                            LastName = m.CreatedBy.LastName ?? string.Empty,
                            MiddleName = m.CreatedBy.MiddleName ?? string.Empty,
                            FullName = m.CreatedBy.FullName ?? string.Empty,
                            OrganizationId = m.CreatedBy.OrganizationId ?? 0
                        }
                    })
                }
            };
        }
    }
}
