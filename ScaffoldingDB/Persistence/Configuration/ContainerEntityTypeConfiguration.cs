using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ScaffoldingDB.Entities;
using System.Reflection.Emit;

namespace ScaffoldingDB.Persistence.Configuration
{
    public class ContainerEntityTypeConfiguration : IEntityTypeConfiguration<Container>
    {
        public void Configure(EntityTypeBuilder<Container> builder)
        {

                builder.HasKey(e => e.Id).HasName("PK__Containe__3214EC2708AE6587");

                builder.ToTable("Container");

                builder.HasIndex(e => e.Name, "IX_Container_Name");

                builder.HasIndex(e => e.ParentId, "IX_Container_ParentID");

                builder.Property(e => e.Id).HasColumnName("ID");
                builder.Property(e => e.LocalStake)
                    .HasDefaultValueSql("((0))")
                    .HasColumnType("decimal(18, 17)");
                builder.Property(e => e.Name).HasMaxLength(75);
                builder.Property(e => e.ParentId).HasColumnName("ParentID");
                builder.Property(e => e.Stake)
                    .HasDefaultValueSql("((0))")
                    .HasColumnType("decimal(18, 17)");
                builder.Property(e => e.Weight).HasDefaultValueSql("((1))");

                builder.HasOne(d => d.Parent).WithMany(p => p.InverseParent)
                    .HasForeignKey(d => d.ParentId)
                    .HasConstraintName("FK__Container__Paren__398D8EEE");
        }
    }
}
