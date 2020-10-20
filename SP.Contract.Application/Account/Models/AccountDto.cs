namespace SP.Contract.Application.Account.Models
{
    public class AccountDto
    {
        public long Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string MiddleName { get; set; }

        public string FullName { get; set; }

        public long? OrganizationId { get; set; }
    }
}
