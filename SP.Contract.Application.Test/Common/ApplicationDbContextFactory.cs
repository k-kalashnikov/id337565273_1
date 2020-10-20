using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using SP.Contract.Infrastructure.Services;
using SP.Contract.Persistence;
using DMContract = SP.Contract.Domains.AggregatesModel.Contract.Entities;

namespace SP.Contract.Application.Test.Common
{
    public class ApplicationDbContextFactory
    {
        public static ApplicationDbContext Create()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .UseLoggerFactory(GetDbLoggerFactory())
                .Options;

            var context = new ApplicationDbContext(
                options,
                MockFactory.CreateCurrentUserServiceMock(),
                new MachineDateTime(),
                MockFactory.CreateMediatorMock());

            context.Database.EnsureCreated();

            DataSeed(context).ContinueWith(x =>
                context.SaveChangesAsync());

            return context;
        }

        public static void Destroy(ApplicationDbContext context)
        {
            context.Database.EnsureDeleted();
            context.Dispose();
        }

        private static async Task DataSeed(ApplicationDbContext context)
        {
            var organizations = new List<Domains.AggregatesModel.Misc.Entities.Organization>()
            {
                new Domains.AggregatesModel.Misc.Entities.Organization(1, "Сев"),
                new Domains.AggregatesModel.Misc.Entities.Organization(2, "Поставщик"),
                new Domains.AggregatesModel.Misc.Entities.Organization(3, "Поставщик2"),
                new Domains.AggregatesModel.Misc.Entities.Organization(4, "Покупатель2")
            };
            await context.Organizations.AddRangeAsync(organizations);
            await context.SaveChangesAsync();

            var accounts = new List<Domains.AggregatesModel.Misc.Entities.Account>()
            {
                new Domains.AggregatesModel.Misc.Entities.Account(1, "Иванов", "Админ", "Иванович", null),
                new Domains.AggregatesModel.Misc.Entities.Account(2, "Иванов", "Менеджер", "Иванович", 1),
                new Domains.AggregatesModel.Misc.Entities.Account(3, "Иванов", "Поставщик", "Иванович", 2),
                new Domains.AggregatesModel.Misc.Entities.Account(4, "Иванов", "Менеджер2", "Иванович", 4),
            };
            await context.Accounts.AddRangeAsync(accounts);
            await context.SaveChangesAsync();

            var contracts = new List<DMContract.Contract>()
            {
                new DMContract.Contract(Guid.NewGuid(), 1, null, 1, 2, "Д-112", DateTime.Now, DateTime.Now.AddDays(10))
                {
                    CreatedBy = await context.Accounts.SingleAsync(a => a.Id == 2)
                },
                new DMContract.Contract(Guid.NewGuid(), 1, null, 1, 3, "Д-113", DateTime.Now, DateTime.Now.AddDays(10))
                {
                    CreatedBy = await context.Accounts.SingleAsync(a => a.Id == 2)
                },
                new DMContract.Contract(Guid.NewGuid(), 1, null, 4, 3, "Д-143", DateTime.Now, DateTime.Now.AddDays(10))
                {
                    CreatedBy = await context.Accounts.SingleAsync(a => a.Id == 4)
                }
            };
            await context.Contracts.AddRangeAsync(contracts);

            // Вызываем синхронную версию имплементацию чтобы не переписывало CreatedBy
            context.SaveChanges();

            var contractDocuments = new List<DMContract.ContractDocument>()
            {
                new DMContract.ContractDocument(contracts.FirstOrDefault(c => c.Number == "Д-112"), "Test", "Протокол голосования комиссии №768.112.pdf", "ContractDocuments/xzpntflueps_637250646975470270"),
                new DMContract.ContractDocument(contracts.FirstOrDefault(c => c.Number == "Д-113"), "Test", "Протокол голосования комиссии №768.113.pdf", "ContractDocuments/xzpntflueps_637250646975470270"),
                new DMContract.ContractDocument(contracts.FirstOrDefault(c => c.Number == "Д-143"), "Test", "Протокол голосования комиссии №768.143.pdf", "ContractDocuments/xzpntflueps_637250646975470270")
            };

            await context.ContractDocuments.AddRangeAsync(contractDocuments);
            await context.SaveChangesAsync();

            await context.ContractStatuses.LoadAsync();
            await context.ContractTypes.LoadAsync();
        }

        private static ILoggerFactory GetDbLoggerFactory()
        {
            IServiceCollection serviceCollection = new ServiceCollection();
            serviceCollection.AddLogging(builder =>
                builder.AddDebug()
                    .AddFilter(DbLoggerCategory.Database.Command.Name, LogLevel.Information));
            return serviceCollection.BuildServiceProvider().GetService<ILoggerFactory>();
        }
    }
}
