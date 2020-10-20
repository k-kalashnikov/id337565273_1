using System;
using System.Collections.Generic;
using System.Text;
using MediatR;
using SP.Contract.Application.Contract.Models;

namespace SP.Contract.Application.Contract.Queries.GetAll
{
    public class GetAllContractsQuery : IRequest<IEnumerable<ContractDto>>
    {
        public static GetAllContractsQuery Create() => new GetAllContractsQuery();
    }
}
