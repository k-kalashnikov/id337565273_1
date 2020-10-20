using System;

namespace SP.Contract.Events.Request.GetContract
{
    public class GetContractRequest
    {
        public GetContractRequest(Guid contractId)
        {
            ContractId = contractId;
        }

        public Guid ContractId { get; set; }
    }
}
