using DomainModel.IntegrationEventModels;
using Infrastructure.EntityConfiguration;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Context
{
    public class BackgroundTaskDbContext : DbContext
    {
        public DbSet<LocalIntegrationEvent>  LocalIntegrationEvents { get; set; }


        public BackgroundTaskDbContext(DbContextOptions<BackgroundTaskDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new LocalIntegrationEventConfiguration());
        }
    }
}
