using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace BlogApplication.Models;

public partial class BlogContext : DbContext
{
    public BlogContext()
    {
    }

    public BlogContext(DbContextOptions<BlogContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Blog> Blogs { get; set; }

    public virtual DbSet<Comment> Comments { get; set; }

    public virtual DbSet<Registration> Registrations { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=5CG2281ZYX\\SQLSERVER2019;Database=Blog;Trusted_Connection=True;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Blog>(entity =>
        {
            entity.HasKey(e => e.Bid).HasName("PK__Blog__C6DE0D2111997670");

            entity.ToTable("Blog");

            entity.Property(e => e.Bid).HasColumnName("BID");
            //entity.Property(e => e.Description).HasMaxLength(maxLength);
            entity.Property(e => e.Title).HasMaxLength(50);

            entity.HasOne(d => d.UidNavigation).WithMany(p => p.Blogs)
                .HasForeignKey(d => d.Uid)
                .HasConstraintName("FK__Blog__Uid__49C3F6B7");
        });

        modelBuilder.Entity<Comment>(entity =>
        {
            entity.HasKey(e => e.Cid).HasName("PK__Comment__C1F8DC59EA97CEF1");

            entity.ToTable("Comment");

            entity.Property(e => e.Cid).HasColumnName("CID");
            entity.Property(e => e.Bid).HasColumnName("BID");
            entity.Property(e => e.Comment1)
                .HasMaxLength(50)
                .HasColumnName("Comment");

            entity.HasOne(d => d.BidNavigation).WithMany(p => p.Comments)
                .HasForeignKey(d => d.Bid)
                .HasConstraintName("FK__Comment__BID__4D94879B");

            entity.HasOne(d => d.UidNavigation).WithMany(p => p.Comments)
                .HasForeignKey(d => d.Uid)
                .HasConstraintName("FK__Comment__Uid__4CA06362");
        });

        modelBuilder.Entity<Registration>(entity =>
        {
            entity.HasKey(e => e.Rid).HasName("PK__Registra__CAF055CA81A2B425");

            entity.ToTable("Registration");

            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .HasColumnName("email");
            entity.Property(e => e.Password)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("password");
            entity.Property(e => e.Uname)
                .HasMaxLength(255)
                .IsUnicode(false);
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Users__3214EC078386C9AE");

            entity.HasIndex(e => e.Email, "UQ__Users__A9D105348C9E886E").IsUnique();

            entity.Property(e => e.Email).HasMaxLength(256);
            entity.Property(e => e.IsActive).HasColumnName("isActive");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
