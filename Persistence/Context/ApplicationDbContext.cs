using Application.Interfaces;
using Domain.Common;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace Persistence.Context
{
    public class ApplicationDbContext : DbContext
    {
        private readonly IDateTimeService _dateTimeService;
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, IDateTimeService dateTime): base(options)
        {
            ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
            _dateTimeService = dateTime;
        }
        public DbSet<Cliente> Clientes { get; set; }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken()) 
        {
            foreach (var entry in ChangeTracker.Entries<AuditableBaseEntity>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.Created = _dateTimeService.NowUtc;
                        break;
                    case EntityState.Modified:
                        entry.Entity.LastModified = _dateTimeService.NowUtc;
                        break;
                    default:
                        break;
                }
            }
            return base.SaveChangesAsync(cancellationToken);
        }
    }
}
