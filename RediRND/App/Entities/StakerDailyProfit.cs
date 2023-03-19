using System;
using System.Collections.Generic;

namespace RediRND.App.Entities;

public partial class StakerDailyProfit
{
    public int StakerId { get; set; }

    public DateTime Date { get; set; }

    public decimal Stake { get; set; }

    public decimal Profit { get; set; }

    public virtual DailyProfit DateNavigation { get; set; } = null!;

    public virtual Staker Staker { get; set; } = null!;
}
