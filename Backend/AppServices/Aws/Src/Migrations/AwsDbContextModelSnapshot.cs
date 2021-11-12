﻿// <auto-generated />
using CloudYourself.Backend.AppServices.Aws.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace CloudYourself.Backend.AppServices.Aws.Migrations
{
    [DbContext(typeof(AwsDbContext))]
    partial class AwsDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.3")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("CloudYourself.Backend.AppServices.Aws.Aggregates.Account.Account", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("AwsAccountId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("CloudAccountId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("TenantId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Accounts");
                });

            modelBuilder.Entity("CloudYourself.Backend.AppServices.Aws.Aggregates.TennantSettings.TenantSettings", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("TenantSettings");
                });

            modelBuilder.Entity("CloudYourself.Backend.AppServices.Aws.Aggregates.TennantSettings.TenantSettings", b =>
                {
                    b.OwnsOne("CloudYourself.Backend.AppServices.Aws.Aggregates.TennantSettings.IamAccount", "IamAccount", b1 =>
                        {
                            b1.Property<int>("TenantSettingsId")
                                .HasColumnType("int");

                            b1.Property<string>("AccessKeyId")
                                .HasColumnType("nvarchar(max)");

                            b1.Property<string>("Secret")
                                .HasColumnType("nvarchar(max)");

                            b1.HasKey("TenantSettingsId");

                            b1.ToTable("TenantSettings");

                            b1.WithOwner()
                                .HasForeignKey("TenantSettingsId");
                        });

                    b.Navigation("IamAccount");
                });
#pragma warning restore 612, 618
        }
    }
}