using System.Threading.Tasks;

namespace SP.Contract.Persistence
{
    public class DbInitializer
    {
        public static Task SeedAsync(ApplicationDbContext context)
        {
            return Task.CompletedTask;
        }
    }
}
