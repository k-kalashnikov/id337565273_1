using System;

namespace SP.Contract.Application.ContractDocuments.Models
{
    public class ContractDocumentDto
    {
        public Guid Id { get; set; }

        public Guid ContractId { get; set; }

        public string Name { get; set; }

        public string FileName { get; set; }

        public string Link { get; set; }

        public DateTime Created { get; set; }
    }
}
