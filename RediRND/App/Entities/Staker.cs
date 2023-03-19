using System;
using System.Collections.Generic;
using RediRND.App.Entities.ContainerAggregate;
using RediRND.App.Interfaces;

namespace RediRND.App.Entities;

public partial class Staker : IAggregateRoot
{
    public int Id { get; set; }

    public string Username { get; set; } = null!;

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string PasswordHash { get; set; } = null!;

    public virtual ICollection<ContainerMembership> ContainerMemberships { get; } = new List<ContainerMembership>();

    public virtual ICollection<StakerDailyProfit> StakerDailyProfits { get; } = new List<StakerDailyProfit>();
}
