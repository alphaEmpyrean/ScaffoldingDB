using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ScaffoldingDB.Entities;

namespace ScaffoldingDB.Persistence.Configuration
{
    public class StakerDailyStakeEntityTypeConfiguration : IEntityTypeConfiguration<StakerDailyStake>
    {
        public void Configure(EntityTypeBuilder<StakerDailyStake> builder)
        {
            builder.HasKey(e => new { e.StakerId, e.Date }).HasName("PK__StakerDa__982439D490A019AD");

            builder.ToTable("StakerDailyStake");

            builder.HasIndex(e => e.Date, "IX_StakerDailyStake_Date");

            builder.Property(e => e.StakerId).HasColumnName("StakerID");
            builder.Property(e => e.Date).HasColumnType("date");
            builder.Property(e => e.Stake)
                .HasDefaultValueSql("((0))")
                .HasColumnType("decimal(18, 17)");

            builder.HasOne(d => d.Staker).WithMany(p => p.StakerDailyStakes)
                .HasForeignKey(d => d.StakerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__StakerDai__Stake__47DBAE45");
        }
    }
}
