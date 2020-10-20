using SP.Contract.Events.Models;

namespace SP.Contract.Events.Request.GetContract
{
    public class GetContractResponse
    {
        public GetContractResponse(ContractDto contract)
        {
            Contract = contract;
        }

        public ContractDto Contract { get; set; }
    }
}
