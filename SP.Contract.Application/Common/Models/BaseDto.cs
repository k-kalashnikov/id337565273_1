using System;

namespace SP.Contract.Application.Common.Models
{
    public class BaseDto
    {
        public string CreatedBy { get; set; }

        public DateTime Created { get; set; }

        public string ModifiedBy { get; set; }

        public DateTime? Modified { get; set; }

        public DateTime? Deleted { get; set; }
    }
}
