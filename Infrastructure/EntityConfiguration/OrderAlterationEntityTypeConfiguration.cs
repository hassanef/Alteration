using DomainModel.Model;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.EntityConfiguration
{
    public class OrderAlterationEntityTypeConfiguration : IEntityTypeConfiguration<OrderAlteration>
    {
        
        public void Configure(EntityTypeBuilder<OrderAlteration> orderAlterationConfiguration)
        {
            orderAlterationConfiguration.HasKey(o => o.Id);

            orderAlterationConfiguration.Property(o => o.Id)
               .ForSqlServerUseSequenceHiLo("orderalterationseq", ApplicationContext.DEFAULT_SCHEMA);

            orderAlterationConfiguration.Ignore(b => b.DomainEvents);

            orderAlterationConfiguration.Property<short>(x => x.OrderStatusId).IsRequired();

            orderAlterationConfiguration.OwnsOne(o => o.ShortenSleeves).Property<short>(x => x.Left).IsRequired();
            orderAlterationConfiguration.OwnsOne(o => o.ShortenSleeves).Property<short>(x => x.Right).IsRequired();
            orderAlterationConfiguration.OwnsOne(o => o.ShortenTrousers).Property<short>(x => x.Left).IsRequired();
            orderAlterationConfiguration.OwnsOne(o => o.ShortenTrousers).Property<short>(x => x.Right).IsRequired();

        }
    }
}
