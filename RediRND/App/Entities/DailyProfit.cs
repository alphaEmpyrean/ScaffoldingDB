namespace RediRND.App.Entities;

public partial class DailyProfit
{
    public DateTime Date { get; set; }

    public decimal Profit { get; set; }

    public virtual ICollection<StakerDailyProfit> StakerDailyProfits { get; } = new List<StakerDailyProfit>();
}

