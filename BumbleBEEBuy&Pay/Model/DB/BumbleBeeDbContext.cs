using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace BumbleBEEBuy_Pay.Model.DB;

public partial class BumbleBeeDbContext : DbContext
{
    public BumbleBeeDbContext()
    {
    }

    public BumbleBeeDbContext(DbContextOptions<BumbleBeeDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<AdminLogin> AdminLogins { get; set; }

    public virtual DbSet<TblCategory> TblCategories { get; set; }

    public virtual DbSet<TblCustomer> TblCustomers { get; set; }

    public virtual DbSet<TblInstallmentDetail> TblInstallmentDetails { get; set; }

    public virtual DbSet<TblProduct> TblProducts { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=DESKTOP-GBDHANN\\SQLSERVER2017;User Id=sa;Password=Sqlserver2017;\nInitial Catalog=Bumble_beeDB;Integrated Security=True;Encrypt=False;TrustServerCertificate=True;\nConnect Timeout=300");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AdminLogin>(entity =>
        {
            entity.HasKey(e => e.AdminId);

            entity.ToTable("AdminLogin");

            entity.Property(e => e.AdminId)
                .ValueGeneratedNever()
                .HasColumnName("AdminID");
            entity.Property(e => e.UserName)
                .HasMaxLength(250)
                .IsUnicode(false);
            entity.Property(e => e.Userpassword)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        modelBuilder.Entity<TblCategory>(entity =>
        {
            entity.HasKey(e => e.CategoryId);

            entity.ToTable("tbl_Category");

            entity.Property(e => e.CategoryId).HasColumnName("Category_ID");
            entity.Property(e => e.CategoryName)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("Category_Name");
        });

        modelBuilder.Entity<TblCustomer>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("tbl_Customer");

            entity.Property(e => e.CusDateofBirth)
                .HasColumnType("date")
                .HasColumnName("Cus_DateofBirth");
            entity.Property(e => e.CusEmail)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("Cus_Email");
            entity.Property(e => e.CusFirstName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Cus_FirstName");
            entity.Property(e => e.CusGender)
                .HasMaxLength(10)
                .IsFixedLength()
                .HasColumnName("Cus_Gender");
            entity.Property(e => e.CusId)
                .ValueGeneratedOnAdd()
                .HasColumnName("Cus_ID");
            entity.Property(e => e.CusIsActive).HasColumnName("Cus_IsActive");
            entity.Property(e => e.CusLastName)
                .HasMaxLength(150)
                .IsUnicode(false)
                .HasColumnName("Cus_LastName");
            entity.Property(e => e.CusMobileNo)
                .HasMaxLength(12)
                .IsFixedLength()
                .HasColumnName("Cus_MobileNo");
            entity.Property(e => e.CusNic)
                .HasMaxLength(11)
                .IsFixedLength()
                .HasColumnName("Cus_NIC");
            entity.Property(e => e.CusPassword)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("Cus_Password");
            entity.Property(e => e.CusRegistrationDate)
                .HasColumnType("date")
                .HasColumnName("Cus_RegistrationDate");
        });

        modelBuilder.Entity<TblInstallmentDetail>(entity =>
        {
            entity.HasKey(e => e.InstallId);

            entity.ToTable("tbl_InstallmentDetails");

            entity.Property(e => e.InstallId).HasColumnName("Install_ID");
            entity.Property(e => e.CusId).HasColumnName("Cus_ID");
            entity.Property(e => e.InstallmentPlan).HasColumnName("Installment_Plan");
            entity.Property(e => e.LoanBalance)
                .HasColumnType("decimal(18, 0)")
                .HasColumnName("Loan_balance");
            entity.Property(e => e.ProductId).HasColumnName("Product_ID");
            entity.Property(e => e.UsedAmount)
                .HasColumnType("decimal(18, 0)")
                .HasColumnName("Used_amount");
        });

        modelBuilder.Entity<TblProduct>(entity =>
        {
            entity.HasKey(e => e.ProductId);

            entity.ToTable("tbl_Product");

            entity.Property(e => e.ProductId).HasColumnName("ProductID");
            entity.Property(e => e.Category)
                .HasMaxLength(250)
                .IsUnicode(false);
            entity.Property(e => e.CategoryId).HasColumnName("Category_ID");
            entity.Property(e => e.CusId).HasColumnName("Cus_ID");
            entity.Property(e => e.ProductBrand)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("Product_brand");
            entity.Property(e => e.ProductName)
                .HasMaxLength(250)
                .IsUnicode(false);
            entity.Property(e => e.UnitPrice).HasColumnType("decimal(18, 0)");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
