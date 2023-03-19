using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ScaffoldingDB.Entities;

namespace ScaffoldingDB.Persistence.Configuration
{
    public class StakerEntityTypeConfiguration : IEntityTypeConfiguration<Staker>
    {
        public void Configure(EntityTypeBuilder<Staker> builder)
        {
            builder.HasKey(e => e.Id).HasName("PK__Staker__3214EC278ADB90EC");

            builder.ToTable("Staker");

            builder.Property(e => e.Id).HasColumnName("ID");
            builder.Property(e => e.FirstName).HasMaxLength(50);
            builder.Property(e => e.LastName).HasMaxLength(50);
        }
    }
}
