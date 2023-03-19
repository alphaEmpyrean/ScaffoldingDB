using System;
using System.Collections.Generic;

namespace ConsoleDB;

public partial class Staker
{
    public int Id { get; set; }

    public string Username { get; set; } = null!;

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string PasswordHash { get; set; } = null!;

    public virtual ICollection<ContainerMembership> ContainerMemberships { get; } = new List<ContainerMembership>();

    public virtual ICollection<StakerDailyStake> StakerDailyStakes { get; } = new List<StakerDailyStake>();
}
