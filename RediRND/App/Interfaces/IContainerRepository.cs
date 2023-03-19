using RediRND.App.Entities;
using RediRND.App.Entities.ContainerAggregate;

namespace RediRND.App.Interfaces;

public interface IContainerRepository : IRepository<Container>
{

    public Task AddChildContainerAsync(int parentId, int newWeight, int containerId);
    public Task AddChildStakerAsync(int parentId, int weight, int stakerId);
    public Task<List<Container>> GetAllIncludeParentAsync();
    public Task<List<Staker>> GetAllNonChildStakersAsync(int parentId);
    public Task<List<Staker>> GetAllChildStakersAsync(int parentId);
    public Task<Container> GetByIdIncludeParentAsync(int id);
    public Task<Container> GetByIdIncludeAllThenIncludeStaker(int id);
    public Task<Staker> GetChildStakerByIdAsync(int parentId, int stakerId);
    public Task RemoveChildContainerAsync(int parentId, int childId);
    public Task RemoveChildStakerAsync(int parentId, int stakerId);
    public Task UpdateChildContainerAsync(int parentId, int newWeight, int childId);
    public Task UpdateChildStakerAsync(int parentId, int newWeight, int childId);
    public Task UpdateStakesAsync(int id);
}
