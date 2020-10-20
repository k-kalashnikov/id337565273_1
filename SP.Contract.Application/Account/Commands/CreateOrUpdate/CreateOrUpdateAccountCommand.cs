using System.Collections;
using System.Collections.Generic;
using MediatR;
using SP.Contract.Application.Common.Response;

namespace SP.Contract.Application.Account.Commands.CreateOrUpdate
{
    public class CreateOrUpdateAccountCommand : IRequest<ProcessingResult<bool>>
    {
        public CreateOrUpdateAccountCommand(IEnumerable<long> accounts)
        {
            Accounts = accounts;
        }

        public IEnumerable<long> Accounts { get; set; }
    }
}
