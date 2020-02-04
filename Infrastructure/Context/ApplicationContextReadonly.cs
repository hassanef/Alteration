

using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using DomainModel.Context;
using DomainModel.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Alteration.Infrastructure.Context
{

    public class ApplicationContextReadOnly: DbContext, IContextReadOnly
    {
         IConfiguration _configuration;
         private string _connectionString;

        
        public virtual DbSet<OrderAlteration> _orderAlteration { get;}

        public ApplicationContextReadOnly(IConfiguration configuration)
        {
          
            _configuration = configuration;
            _connectionString = configuration["ConnectionStrings:ConnectionString"];

            _orderAlteration =  Set<OrderAlteration>();
        }
     
        public IQueryable<OrderAlteration> OrderAlterations
        {
            get { return this._orderAlteration; }
        }
  

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
             optionsBuilder.UseSqlServer(_connectionString);
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);


            builder.Entity<OrderAlteration>().OwnsOne(o => o.ShortenSleeves);
            builder.Entity<OrderAlteration>().OwnsOne(o => o.ShortenTrousers);
            builder.Entity<OrderAlteration>().ToTable("OrderAlterations", "dbo");
        }

        public override int SaveChanges()
        {
            throw new InvalidOperationException();
        }
        public override int SaveChanges(bool acceptAllChangesOnSuccess)
        {
            throw new InvalidOperationException();
        }
        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default(CancellationToken))
        {
            throw new InvalidOperationException();
        }
        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            throw new InvalidOperationException();
        }
    }
}