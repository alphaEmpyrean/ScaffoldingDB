using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ScaffoldingDB.Entities;

namespace ScaffoldingDB.Persistence.Configuration
{
    public class ContainerMembershipEntityTypeConfiguration : IEntityTypeConfiguration<ContainerMembership>
    {
        public void Configure(EntityTypeBuilder<ContainerMembership> builder)
        {
            builder.HasKey(e => new { e.ContainerId, e.StakerId }).HasName("PK__Containe__5D8C1B7B0678ED0E");

            builder.ToTable("ContainerMembership");

            builder.HasIndex(e => e.StakerId, "IX_ContainerMembership_StakerID");

            builder.Property(e => e.ContainerId).HasColumnName("ContainerID");
            builder.Property(e => e.StakerId).HasColumnName("StakerID");
            builder.Property(e => e.LocalStake)
                .HasDefaultValueSql("((0))")
                .HasColumnType("decimal(18, 17)");
            builder.Property(e => e.Weight).HasDefaultValueSql("((1))");

            builder.HasOne(d => d.Container).WithMany(p => p.ContainerMemberships)
                .HasForeignKey(d => d.ContainerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Container__Conta__412EB0B6");

            builder.HasOne(d => d.Staker).WithMany(p => p.ContainerMemberships)
                .HasForeignKey(d => d.StakerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Container__Stake__4222D4EF");
        }
    }
}
