using RediRND.App.Interfaces;
using System;
using System.Collections.Generic;

namespace RediRND.App.Entities.ContainerAggregate;

public partial class Container : IAggregateRoot
{
    public int Id { get; set; }

    public int? ParentId { get; set; }

    public string Name { get; set; } = null!;

    public decimal Stake { get; set; }

    public int Weight { get; set; }

    public decimal LocalStake { get; set; }

    public virtual IList<ContainerMembership> ContainerMemberships { get; } = new List<ContainerMembership>();

    public virtual ICollection<Container> InverseParent { get; } = new List<Container>();

    public virtual Container? Parent { get; set; }
}
