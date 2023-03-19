using System;
using System.Collections.Generic;

namespace RediRND.App.Entities.ContainerAggregate;

public partial class ContainerMembership
{
    public int ContainerId { get; set; }

    public int StakerId { get; set; }

    public int Weight { get; set; }

    public decimal LocalStake { get; set; }

    public decimal Stake { get; set; }

    public virtual Container Container { get; set; } = null!;

    public virtual Staker Staker { get; set; } = null!;
}
