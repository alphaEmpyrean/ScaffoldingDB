using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace ConsoleDB;

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

    public virtual DbSet<Staker> Stakers { get; set; }

    public virtual DbSet<StakerDailyStake> StakerDailyStakes { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Data Source=aedesktop;Initial Catalog=rediRND;Integrated Security=True;TrustServerCertificate=True");

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
            entity.Property(e => e.Weight).HasDefaultValueSql("((1))");

            entity.HasOne(d => d.Container).WithMany(p => p.ContainerMemberships)
                .HasForeignKey(d => d.ContainerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Container__Conta__4222D4EF");

            entity.HasOne(d => d.Staker).WithMany(p => p.ContainerMemberships)
                .HasForeignKey(d => d.StakerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
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

        modelBuilder.Entity<StakerDailyStake>(entity =>
        {
            entity.HasKey(e => new { e.StakerId, e.Date }).HasName("PK__StakerDa__982439D4E4841E8C");

            entity.ToTable("StakerDailyStake");

            entity.HasIndex(e => e.Date, "IX_StakerDailyStake_Date");

            entity.Property(e => e.StakerId).HasColumnName("StakerID");
            entity.Property(e => e.Date).HasColumnType("date");
            entity.Property(e => e.Stake)
                .HasDefaultValueSql("((0))")
                .HasColumnType("decimal(18, 17)");

            entity.HasOne(d => d.Staker).WithMany(p => p.StakerDailyStakes)
                .HasForeignKey(d => d.StakerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__StakerDai__Stake__48CFD27E");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
