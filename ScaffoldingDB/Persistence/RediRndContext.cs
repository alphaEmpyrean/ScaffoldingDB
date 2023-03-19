using System;
using System.Collections.Generic;
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using ScaffoldingDB.Entities;

namespace ScaffoldingDB.Data;

public partial class RediRndContext : DbContext
{
    public RediRndContext() { }

    public RediRndContext(DbContextOptions<RediRndContext> options)
        : base(options)
    { }

    public virtual DbSet<Container> Containers { get; set; }

    public virtual DbSet<ContainerMembership> ContainerMemberships { get; set; }

    public virtual DbSet<Staker> Stakers { get; set; }

    public virtual DbSet<StakerDailyStake> StakerDailyStakes { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        base.OnModelCreating(modelBuilder);
    }
}
