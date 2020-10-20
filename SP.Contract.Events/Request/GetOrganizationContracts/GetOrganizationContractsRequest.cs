using System;
using System.Collections.Generic;
using System.Text;

namespace SP.Contract.Events.Request.GetOrganizationContracts
{
  public class GetOrganizationContractsRequest
  {
    public long CustomerOrganizationId { get; set; }

    public GetOrganizationContractsRequest(long customerOrganizationId)
    {
      CustomerOrganizationId = customerOrganizationId;
    }
  }
}
