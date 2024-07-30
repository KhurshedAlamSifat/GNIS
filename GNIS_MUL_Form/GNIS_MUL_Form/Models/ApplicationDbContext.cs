using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace GNIS_MUL_Form.Models;

public partial class ApplicationDbContext : DbContext
{
    public ApplicationDbContext()
    {
    }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<CorporateCustomerTbl> CorporateCustomerTbls { get; set; }

    public virtual DbSet<IndividualCustomerTbl> IndividualCustomerTbls { get; set; }

    public virtual DbSet<MeetingMinutesDetailsTbl> MeetingMinutesDetailsTbls { get; set; }

    public virtual DbSet<MeetingMinutesMasterTbl> MeetingMinutesMasterTbls { get; set; }

    public virtual DbSet<ProductsServiceTbl> ProductsServiceTbls { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseSqlServer("Server=ASUS-SIFAT;Database=GNIS;Trusted_Connection=True;TrustServerCertificate=True;");
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<CorporateCustomerTbl>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Corporat__3214EC07654263A7");

            entity.ToTable("Corporate_Customer_Tbl");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Name).HasMaxLength(100);
        });

        modelBuilder.Entity<IndividualCustomerTbl>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Individu__3214EC07CA9AA51D");

            entity.ToTable("Individual_Customer_Tbl");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Name).HasMaxLength(100);
        });

        modelBuilder.Entity<MeetingMinutesDetailsTbl>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Meeting___3214EC07FC89738D");

            entity.ToTable("Meeting_Minutes_Details_Tbl");

            entity.Property(e => e.Unit).HasMaxLength(50);
        });

        modelBuilder.Entity<MeetingMinutesMasterTbl>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Meeting___3214EC07C26BAEA9");

            entity.ToTable("Meeting_Minutes_Master_Tbl");

            entity.Property(e => e.CustomerType).HasMaxLength(50);
            entity.Property(e => e.Date).HasColumnType("datetime");
            entity.Property(e => e.MeetingPlace).HasMaxLength(255);
        });

        modelBuilder.Entity<ProductsServiceTbl>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Products__3214EC077B83E7BF");

            entity.ToTable("Products_Service_Tbl");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Name).HasMaxLength(100);
            entity.Property(e => e.Unit).HasMaxLength(50);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
