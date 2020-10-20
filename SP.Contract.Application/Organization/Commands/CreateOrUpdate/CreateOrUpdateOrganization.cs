using System.Collections.Generic;
using MediatR;
using SP.Contract.Application.Common.Response;

namespace SP.Contract.Application.Organization.Commands.CreateOrUpdate
{
    public class CreateOrUpdateOrganization : IRequest<ProcessingResult<bool>>
    {
        public CreateOrUpdateOrganization(IEnumerable<long> organizations)
        {
            Organizations = organizations;
        }

        public IEnumerable<long> Organizations { get; set; }
    }
}
