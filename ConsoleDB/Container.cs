using System;
using System.Collections.Generic;

namespace ConsoleDB;

public partial class Container
{
    public int Id { get; set; }

    public int? ParentId { get; set; }

    public string Name { get; set; } = null!;

    public decimal? Stake { get; set; }

    public int? Weight { get; set; }

    public decimal? LocalStake { get; set; }

    public virtual ICollection<ContainerMembership> ContainerMemberships { get; } = new List<ContainerMembership>();

    public virtual ICollection<Container> InverseParent { get; } = new List<Container>();

    public virtual Container? Parent { get; set; }
}
