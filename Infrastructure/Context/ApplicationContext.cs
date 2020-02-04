
using DomainModel.IntegrationEventModels;
using DomainModel.Model;
using Infrastructure.EntityConfiguration;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Context
{
    public class ApplicationContext : DbContext
    { 
        public const string DEFAULT_SCHEMA = "alter";

        public DbSet<OrderAlteration> OrderAlteraions { get; private set; }
        public DbSet<LocalIntegrationEvent> LocalIntegrationEvents { get; private set; }

        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {
            //_mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            System.Diagnostics.Debug.WriteLine("Context::ctor ->" + this.GetHashCode());
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<OrderAlteration>()
             .Ignore(b => b.OrderStatus);

            builder.Entity<OrderAlteration>().ToTable("OrderAlterations", "dbo");
            builder.ApplyConfiguration(new OrderAlterationEntityTypeConfiguration());
        }
    }
}
