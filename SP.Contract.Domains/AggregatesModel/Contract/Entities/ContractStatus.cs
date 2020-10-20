using System.Collections.Generic;
using SP.Contract.Domains.Common;

namespace SP.Contract.Domains.AggregatesModel.Contract.Entities
{
    public class ContractStatus : ReadOnlyEnumeration
    {
        public const string StatusDraft = "Черновик";
        public const string StatusSigned = "Подписан";

        public static ContractStatus Draft { get; } = new ContractStatus(1, StatusDraft);

        public static ContractStatus Signed { get; } = new ContractStatus(2, StatusSigned);

        public static IEnumerable<ContractStatus> List() =>
            new[] { Draft, Signed };

        public ContractStatus(int id, string name)
            : base(id, name)
        {
        }
    }
}
