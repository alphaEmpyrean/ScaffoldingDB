using RediRND.App.Entities;
using RediRND.App.Entities.ContainerAggregate;

namespace RediRND.App.Interfaces;

public interface IStakerRepository : IRepository<Staker>
{
    public Task<List<StakerDailyProfit>> GetDailyProfitsAsync(int id);

    public Task<List<Container>> GetParentContainers(int id);
}
