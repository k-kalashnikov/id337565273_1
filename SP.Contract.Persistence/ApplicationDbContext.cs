using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using SP.Contract.Application.Common.Extensions;
using SP.Contract.Application.Common.Interfaces;
using SP.Contract.Common;
using SP.Contract.Domains.AggregatesModel.Contract.Entities;
using SP.Contract.Domains.AggregatesModel.Misc.Entities;
using SP.Contract.Domains.Common;
using SP.Contract.Infrastructure.Extensions;
using SP.Market.Identity.Common.Interfaces;

namespace SP.Contract.Persistence
{
    public class ApplicationDbContext : DbContext, IApplicationDbContext
    {
        private readonly ICurrentUserService _currentUserService;
        private readonly IDateTime _dateTime;
        private readonly IMediator _mediator;

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
           : base(options)
        {
        }

        public ApplicationDbContext(
            DbContextOptions<ApplicationDbContext> options,
            ICurrentUserService currentUserService,
            IDateTime dateTime,
            IMediator mediator)
            : base(options)
        {
            _currentUserService = currentUserService;
            _dateTime = dateTime;
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        public DbContext AppDbContext => this;

        public DbSet<Domains.AggregatesModel.Contract.Entities.Contract> Contracts { get; set; }

        public DbSet<Account> Accounts { get; set; }

        public DbSet<Organization> Organizations { get; set; }

        public DbSet<ContractStatus> ContractStatuses { get; set; }

        public DbSet<ContractDocument> ContractDocuments { get; set; }

        public DbSet<ContractType> ContractTypes { get; set; }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var currentUser = _currentUserService.GetCurrentUser();

            UpdateEntities(currentUser);
            UpdateReadOnlyEntities(currentUser);

            var result = await base.SaveChangesAsync(cancellationToken);
            await DispatchDomainEventsAsync();

            return result;
        }

        private static void UpdateState<T>(EntityEntry<T> entry)
            where T : class, IEntity
        {
            if (entry.Entity.IsNew)
            {
                entry.State = EntityState.Added;
            }
        }

        private async Task DispatchDomainEventsAsync()
        {
            var domainEntities = ChangeTracker
               .Entries<IEntity>()
               .Where(x => x.Entity.DomainEvents != null && x.Entity.DomainEvents.Any());
            await _mediator.DispatchDomainEventsAsync(domainEntities);
        }

        private void UpdateEntities(ICurrentUser currentUser)
        {
            foreach (var entry in ChangeTracker.Entries<Entity>())
            {
                UpdateState(entry);

                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.SetCreatedBy(currentUser.Id);
                        entry.Entity.Created = _dateTime.Now;
                        break;
                    case EntityState.Modified:
                        entry.Entity.SetModifiedBy(currentUser.Id);
                        entry.Entity.Modified = _dateTime.Now;
                        break;
                }
            }
        }

        private void UpdateReadOnlyEntities(ICurrentUser currentUser)
        {
            foreach (var entry in ChangeTracker.Entries<ReadOnlyEntity>())
            {
                UpdateState(entry);

                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.SetCreatedBy(currentUser.Id);
                        entry.Entity.Created = _dateTime.Now;
                        break;
                }
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
#if DEBUG
            optionsBuilder.UseLoggerFactory(GetDbLoggerFactory()).EnableSensitiveDataLogging();
#endif
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
