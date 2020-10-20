using System;
using System.Collections.Generic;
using System.Text;
using SP.Contract.Events.Models;

namespace SP.Contract.Events.Request.GetOrganizationContracts
{
  public class GetOrganizationContractsResponse
  {
    public ContractDto[] Contracts { get; set; }

    public GetOrganizationContractsResponse(ContractDto[] contracts)
    {
      Contracts = contracts;
    }
  }
}
