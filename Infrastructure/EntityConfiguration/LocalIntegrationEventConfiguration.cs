using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using DomainModel.IntegrationEventModels;
using Infrastructure.Context;

namespace Infrastructure.EntityConfiguration
{
    class LocalIntegrationEventConfiguration : IEntityTypeConfiguration<LocalIntegrationEvent>
    {
        public void Configure(EntityTypeBuilder<LocalIntegrationEvent> entityTypeBuilder)
        {


            entityTypeBuilder.HasKey(o => o.Id);

            entityTypeBuilder.Property(o => o.Id)
               .ForSqlServerUseSequenceHiLo("localIntegrationEventseq", ApplicationContext.DEFAULT_SCHEMA);

            entityTypeBuilder.Property(e => e.BinaryBody).IsRequired();
            entityTypeBuilder.Property(e => e.JsonBoby).IsRequired();
            entityTypeBuilder.Property(e => e.ModelName)
                .IsRequired()
                .HasMaxLength(256);
            entityTypeBuilder.Property(e => e.ModelNamespace)
                .IsRequired()
                .HasMaxLength(512);

            entityTypeBuilder.Property(e=>e.CreatedAt).HasColumnType("datetime2").IsRequired();

        }
    }
}
