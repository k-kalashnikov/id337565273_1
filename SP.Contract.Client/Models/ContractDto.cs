using System;
using System.Collections.Generic;
using System.Text;

namespace SP.Contract.Client.Models
{
    public class ContractDto
    {
        public string Id { get; set; }

        public string ParentId { get; set; }

        public string Number { get; set; }

        public string StartDate { get; set; }

        public string FinishDate { get; set; }

        public AccountDto CreatedBy { get; set; }

        public string Created { get; set; }

        public long CustomerOrganizationId { get; set; }

        public long ContractorOrganizationId { get; set; }
    }
}
