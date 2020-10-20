using System.Collections.Generic;
using SP.Contract.Domains.Common;

namespace SP.Contract.Domains.AggregatesModel.Contract.Entities
{
    public class ContractType : ReadOnlyEnumeration
    {
        public const string TypeFramework = "Рамочный";
        public const string TypeRegular = "Прейскурантный";

        public static ContractType Framework { get; } = new ContractType(1, TypeFramework);

        public static ContractType Regular { get; } = new ContractType(2, TypeRegular);

        public static IEnumerable<ContractType> List() =>
            new[] { Framework, Regular };

        public ContractType(int id, string name)
            : base(id, name)
        {
        }
    }
}
