using SP.Contract.Domains.AggregatesModel.Misc.Entities;
using SP.Market.Identity.Common.Interfaces;

namespace SP.Contract.Infrastructure.Extensions
{
    public static class CurrentUserExtension
    {
        public static Account ToAccount(this ICurrentUser currentUser)
        {
            return new Account(
                currentUser.Id,
                currentUser.FirstName,
                currentUser.LastName,
                currentUser.MiddleName,
                currentUser.OrganizationId);
        }
    }
}
