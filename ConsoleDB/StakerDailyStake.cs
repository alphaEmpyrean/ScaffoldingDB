using System;
using System.Collections.Generic;

namespace ConsoleDB;

public partial class StakerDailyStake
{
    public int StakerId { get; set; }

    public DateTime Date { get; set; }

    public decimal? Stake { get; set; }

    public virtual Staker Staker { get; set; } = null!;
}
