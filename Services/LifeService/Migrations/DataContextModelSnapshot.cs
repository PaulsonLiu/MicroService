using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using LifeService;

namespace LifeService.Migrations
{
    [DbContext(typeof(DataContext))]
    partial class DataContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.2")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("MicroService.Models.MR0001_USER_MSTR", b =>
                {
                    b.Property<Guid>("MR0001_PK")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(50);

                    b.Property<Guid>("MR0001_Company_RK");

                    b.Property<string>("MR0001_Email");

                    b.Property<int>("MR0001_ID")
                        .HasColumnType("int");

                    b.Property<string>("MR0001_Name");

                    b.Property<string>("MR0001_PassWord");

                    b.HasKey("MR0001_PK");

                    b.HasIndex("MR0001_Company_RK");

                    b.ToTable("MR0001_USER_MSTR");
                });

            modelBuilder.Entity("MicroService.Models.MR0003_COMPANY", b =>
                {
                    b.Property<Guid>("MR0003_PK")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(50);

                    b.Property<string>("MR0003_Name");

                    b.HasKey("MR0003_PK");

                    b.ToTable("MR0003_COMPANY");
                });

            modelBuilder.Entity("MicroService.Models.MR0001_USER_MSTR", b =>
                {
                    b.HasOne("MicroService.Models.MR0003_COMPANY", "MR0001_Company")
                        .WithMany()
                        .HasForeignKey("MR0001_Company_RK")
                        .OnDelete(DeleteBehavior.Cascade);
                });
        }
    }
}
