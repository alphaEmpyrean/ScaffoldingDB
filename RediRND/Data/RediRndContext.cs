using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using RediRND.App.Entities;
using RediRND.App.Entities.ContainerAggregate;

namespace RediRND.Persistence;

public partial class RediRndContext : DbContext
{
    public RediRndContext()
    {
    }

    public RediRndContext(DbContextOptions<RediRndContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Container> Containers { get; set; }

    public virtual DbSet<ContainerMembership> ContainerMemberships { get; set; }

    public virtual DbSet<DailyProfit> DailyProfits { get; set; }

    public virtual DbSet<Staker> Stakers { get; set; }

    public virtual DbSet<StakerDailyProfit> StakerDailyProfits { get; set; }
  

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Name=ConnectionStrings:rediRND");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Container>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Containe__3214EC279957CAAE");

            entity.ToTable("Container");

            entity.HasIndex(e => e.Name, "IX_Container_Name");

            entity.HasIndex(e => e.ParentId, "IX_Container_ParentID");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.LocalStake)
                .HasDefaultValueSql("((0))")
                .HasColumnType("decimal(18, 17)");
            entity.Property(e => e.Name).HasMaxLength(75);
            entity.Property(e => e.ParentId).HasColumnName("ParentID");
            entity.Property(e => e.Stake)
                .HasDefaultValueSql("((0))")
                .HasColumnType("decimal(18, 17)");
            entity.Property(e => e.Weight).HasDefaultValueSql("((1))");

            entity.HasOne(d => d.Parent).WithMany(p => p.InverseParent)
                .HasForeignKey(d => d.ParentId)
                .HasConstraintName("FK__Container__Paren__3A81B327");
        });

        modelBuilder.Entity<ContainerMembership>(entity =>
        {
            entity.HasKey(e => new { e.ContainerId, e.StakerId }).HasName("PK__Containe__5D8C1B7B9692C7E0");

            entity.ToTable("ContainerMembership");

            entity.HasIndex(e => e.StakerId, "IX_ContainerMembership_StakerID");

            entity.Property(e => e.ContainerId).HasColumnName("ContainerID");
            entity.Property(e => e.StakerId).HasColumnName("StakerID");
            entity.Property(e => e.LocalStake)
                .HasDefaultValueSql("((0))")
                .HasColumnType("decimal(18, 17)");
            entity.Property(e => e.Stake)
                .HasDefaultValueSql("((0))")
                .HasColumnType("decimal(18, 17)");
            entity.Property(e => e.Weight).HasDefaultValueSql("((1))");

            entity.HasOne(d => d.Container).WithMany(p => p.ContainerMemberships)
                .HasForeignKey(d => d.ContainerId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__Container__Conta__4222D4EF");

            entity.HasOne(d => d.Staker).WithMany(p => p.ContainerMemberships)
                .HasForeignKey(d => d.StakerId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__Container__Stake__4316F928");
        });

        modelBuilder.Entity<Staker>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Staker__3214EC272BE2A30B");

            entity.ToTable("Staker");

            entity.HasIndex(e => e.Username, "UQ__Staker__536C85E4A76FBB02").IsUnique();

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.FirstName).HasMaxLength(50);
            entity.Property(e => e.LastName).HasMaxLength(50);
            entity.Property(e => e.PasswordHash)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Username).HasMaxLength(50);
        });


        modelBuilder.Entity<DailyProfit>(entity =>
        {
            entity.HasKey(e =>  e.Date ).HasName("PK__DailyPro__77387D06460662A5");

            entity.ToTable("DailyProfit");

            entity.Property(e => e.Date).HasColumnType("date");
            entity.Property(e => e.Profit)
                .HasDefaultValueSql("((0))")
                .HasColumnType("decimal(18, 2)");
        });

        modelBuilder.Entity<StakerDailyProfit>(entity =>
        {
            entity.HasKey(e => new { e.StakerId, e.Date }).HasName("PK__StakerDa__982439D45C0635F7");

            entity.ToTable("StakerDailyProfit");

            entity.HasIndex(e => e.Date, "IX_StakerDailyProfit_Date");

            entity.Property(e => e.StakerId).HasColumnName("StakerID");
            entity.Property(e => e.Date).HasColumnType("date");
            entity.Property(e => e.Profit).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.Stake).HasColumnType("decimal(18, 17)");

            entity.HasOne(d => d.DateNavigation).WithMany(p => p.StakerDailyProfits)
                .HasForeignKey(d => d.Date)
                .HasConstraintName("FK__StakerDail__Date__1EA48E88");

            entity.HasOne(d => d.Staker).WithMany(p => p.StakerDailyProfits)
                .HasForeignKey(d => d.StakerId)
                .HasConstraintName("FK__StakerDai__Stake__1DB06A4F");
        });


        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
