using System;
using SP.Service.Common.Filters.Filters;

namespace SP.Contract.Application.Contract.Models
{
    public class ContractFilter
    {
        public FilterFieldDefinition<string> Number { get; set; }

        public FilterFieldDefinition<string> ContractorName { get; set; }

        public FilterFieldDefinition<string> CustomerName { get; set; }

        public FilterFieldDefinition<string> ContractTypeName { get; set; }

        public FilterFieldDefinition<string> ContractStatusName { get; set; }

        public FilterFieldDefinition<DateTime> StartDate { get; set; }

        public FilterFieldDefinition<DateTime> FinishDate { get; set; }

        public FilterFieldDefinition<string> CreatedBy { get; set; }

        public FilterFieldDefinition<DateTime> Created { get; set; }

        public long? ContractorOrganizationId { get; set; }

        public long? CustomerOrganizationId { get; set; }

        public long? ContractTypeId { get; set; }

        public long? ContractStatusId { get; set; }
    }
}
