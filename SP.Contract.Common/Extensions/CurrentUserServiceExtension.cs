using SP.Market.Identity.Common.Interfaces;

namespace SP.Contract.Common.Extensions
{
    public static class CurrentUserServiceExtension
    {
        public static string FullName(this ICurrentUserService currentUserService)
        {
            var currentUser = currentUserService.GetCurrentUser();
            return $"{currentUser.LastName} " +
                $"{currentUser.FirstName} " +
                $"{currentUser.MiddleName}";
        }
    }
}
