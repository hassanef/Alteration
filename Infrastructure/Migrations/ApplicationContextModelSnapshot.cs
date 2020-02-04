﻿// <auto-generated />
using System;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Infrastructure.Migrations
{
    [DbContext(typeof(ApplicationContext))]
    partial class ApplicationContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.1-servicing-10028")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("Relational:Sequence:alter.orderalterationseq", "'orderalterationseq', 'alter', '1', '10', '', '', 'Int64', 'False'")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("DomainModel.IntegrationEventModels.LocalIntegrationEvent", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<byte[]>("BinaryBody");

                    b.Property<DateTime>("CreatedAt");

                    b.Property<string>("JsonBoby");

                    b.Property<string>("ModelName");

                    b.Property<string>("ModelNamespace");

                    b.Property<int>("Status");

                    b.Property<Guid?>("UniqueId");

                    b.HasKey("Id");

                    b.ToTable("LocalIntegrationEvents");
                });

            modelBuilder.Entity("DomainModel.Model.OrderAlteration", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:HiLoSequenceName", "orderalterationseq")
                        .HasAnnotation("SqlServer:HiLoSequenceSchema", "alter")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.SequenceHiLo);

                    b.Property<string>("CustomerName");

                    b.Property<byte>("OrderStatusId");

                    b.HasKey("Id");

                    b.ToTable("OrderAlterations","dbo");
                });

            modelBuilder.Entity("DomainModel.Model.OrderAlteration", b =>
                {
                    b.OwnsOne("DomainModel.Model.Shorten", "ShortenSleeves", b1 =>
                        {
                            b1.Property<int>("OrderAlterationId");

                            b1.Property<short>("Left");

                            b1.Property<short>("Right");

                            b1.HasKey("OrderAlterationId");

                            b1.ToTable("OrderAlterations","dbo");

                            b1.HasOne("DomainModel.Model.OrderAlteration")
                                .WithOne("ShortenSleeves")
                                .HasForeignKey("DomainModel.Model.Shorten", "OrderAlterationId")
                                .OnDelete(DeleteBehavior.Cascade);
                        });

                    b.OwnsOne("DomainModel.Model.Shorten", "ShortenTrousers", b1 =>
                        {
                            b1.Property<int>("OrderAlterationId");

                            b1.Property<short>("Left");

                            b1.Property<short>("Right");

                            b1.HasKey("OrderAlterationId");

                            b1.ToTable("OrderAlterations","dbo");

                            b1.HasOne("DomainModel.Model.OrderAlteration")
                                .WithOne("ShortenTrousers")
                                .HasForeignKey("DomainModel.Model.Shorten", "OrderAlterationId")
                                .OnDelete(DeleteBehavior.Cascade);
                        });
                });
#pragma warning restore 612, 618
        }
    }
}
