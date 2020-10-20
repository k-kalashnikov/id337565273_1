using System;
using MediatR;
using SP.Contract.Application.Contract.Models;

namespace SP.Contract.Application.Contract.Queries.Get
{
    public class GetContractQuery : IRequest<ContractDto>
    {
        public GetContractQuery(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; }

        public static GetContractQuery Create(Guid id) => new GetContractQuery(id);
    }
}
