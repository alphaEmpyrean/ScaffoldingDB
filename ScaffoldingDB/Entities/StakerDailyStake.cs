using System;
using System.Collections.Generic;

namespace ScaffoldingDB.Entities;

public partial class StakerDailyStake
{
    public int StakerId { get; set; }

    public DateTime Date { get; set; }

    public decimal? Stake { get; set; }

    public virtual Staker Staker { get; set; } = null!;
}
