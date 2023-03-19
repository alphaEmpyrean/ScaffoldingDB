using Microsoft.EntityFrameworkCore;
using RediRND.App.Entities;
using RediRND.App.Entities.ContainerAggregate;
using RediRND.App.Exceptions;
using RediRND.App.Interfaces;
using RediRND.Persistence;

namespace RediRND.App.Repositories;

public class ContainerRepository
{
    private readonly RediRndContext _context;
    public ContainerRepository(RediRndContext dbContext)
    {
        _context = dbContext;
    }

    public async Task AddAsync(Container container)
    {
        if (_context.Containers == null)
            throw new NullDbSetException(nameof(Container));

        // Save new Container
        _context.Containers.Add(container);
        await _context.SaveChangesAsync();

        // Update Local Stakes
        if (container.ParentId != null)
            await UpdateStakesAsync(container.ParentId.Value);

        return;
    }

    public async Task AddChildContainerAsync(int parentId, int newWeight, Container newChild)
    {
        newChild.ParentId = parentId;
        newChild.Weight = newWeight;
        await UpdateAsync(newChild);

        // Update local stakes
        await UpdateStakesAsync(parentId);
    }

    public async Task AddChildStakerAsync(int parentId, int weight, int stakerId)
    {
        if (!ContainerExists(parentId))
            throw new KeyNotFoundException();

        _context.ContainerMemberships.Add(new ContainerMembership
        {
            ContainerId = parentId,
            Weight = weight,
            StakerId = stakerId
        });

        await _context.SaveChangesAsync();

        // Update local stakes
        await UpdateStakesAsync(parentId);
    }

    public async Task DeleteByIdAsync(int id)
    {

        if (_context.Containers == null)
            throw new NullDbSetException(nameof(Container));

        var container = await _context.Containers.FindAsync(id);

        if (container != null)
        {
            _context.Containers.Remove(container);
            await _context.SaveChangesAsync();

            // Update local stakes
            if (container.ParentId != null)
                await UpdateStakesAsync(container.ParentId.Value);
        }
    }

    public async Task<List<Container>> GetAllAsync()
    {
        if (_context.Containers == null)
            throw new NullDbSetException(nameof(Container));

        return await _context.Containers.OrderBy(c => c.Name).ToListAsync();
    }

    public async Task<List<Container>> GetAllWithParentAsync()
    {
        if (_context.Containers == null)
            throw new NullDbSetException(nameof(Container));

        return await _context.Containers
            .Include(c => c.Parent)
            .OrderBy(c => c.ParentId)
            .ThenBy(c => c.Name)
            .ToListAsync();
    }

    public async Task<Container> GetByIdAsync(int id)
    {
        if (_context.Containers == null)
            throw new NullDbSetException(nameof(Container));

        var container = await _context.Containers.FirstOrDefaultAsync(c => c.Id == id);

        return container == null ? throw new KeyNotFoundException(id.ToString()) : container;
    }

    public async Task<Container> GetByIdWithParentAsync(int id)
    {
        if (_context.Containers == null)
            throw new NullDbSetException(nameof(Container));

        var container = await _context.Containers.Include(c => c.Parent).FirstOrDefaultAsync(c => c.Id == id);

        return container == null ? throw new KeyNotFoundException(id.ToString()) : container;
    }

    public async Task<Container> GetByIdWithAllThenWithStaker(int id)
    {
        if (_context.Containers == null)
            throw new NullDbSetException(nameof(Container));

        // Get details for container and eager load all related data
        var container = await _context.Containers
            .Include(c => c.Parent)
            .Include(c => c.InverseParent)
            .Include(c => c.ContainerMemberships)
            .ThenInclude(cm => cm.Staker)
            .OrderBy(c => c.Name)
            .FirstOrDefaultAsync(c => c.Id == id);

        return container == null ? throw new KeyNotFoundException(id.ToString()) : container;
    }

    public async Task<ContainerMembership> GetContainerMembershipByIdWithStaker(int id, int childId)
    {
        // Get the appropriate container memebership so that we can edit the weight
        var containermembership = await _context.ContainerMemberships
            .Where(cm => cm.ContainerId == id && cm.StakerId == childId)
            .Include(cm => cm.Staker)
            .FirstOrDefaultAsync();

        if (containermembership == null)
            throw new KeyNotFoundException();

        return containermembership;        
    }

    public async Task<List<Container>> GetNonChildrenContainersByContainerIdAsync(int id)
    {
        return await _context.Containers.Where(c => c.ParentId != id).ToListAsync();
    }

    public async Task<List<Staker>> GetNonChildrenStakersByContainerIdAsync(int id)
    {
        if (_context.ContainerMemberships == null)
            throw new NullDbSetException(nameof(ContainerMembership));
        if (_context.Stakers == null)
            throw new NullDbSetException(nameof(Staker));

        // Get all stakers that are not already in the container
        var containerMembershipsQuery =
            (from cm in _context.ContainerMemberships
             where (cm.ContainerId == id)
             select cm);

        var stakersNotInContainerQuery =
            from s in _context.Stakers
            where !containerMembershipsQuery.Any(cm => cm.StakerId == s.Id)
            orderby s.LastName, s.FirstName
            select s;

        return await stakersNotInContainerQuery.ToListAsync();
    }

    public async Task RemoveChildContainer(int parentId, Container container)
    {
        if (parentId != container.ParentId)
            return;

        container.ParentId = null;
        await UpdateAsync(container);
    }

    public async Task RemoveChildStaker(int parentId, int stakerId)
    {
        var containerMembership = await _context.ContainerMemberships.FindAsync(parentId, stakerId);

        if (containerMembership == null) return;

        _context.ContainerMemberships.Remove(containerMembership);
        await _context.SaveChangesAsync();

        await UpdateStakesAsync(parentId);
    }

    public async Task UpdateAsync(Container targetContainer)
    {
        // Get old parent for potential stake updates
        var oldParentId = await _context.Containers.Where(c => c.Id == targetContainer.Id).Select(c => c.ParentId).FirstOrDefaultAsync();

        // Persist updated target container
        _context.Attach(targetContainer).State = EntityState.Modified;
        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!ContainerExists(targetContainer.Id))
                throw new KeyNotFoundException(targetContainer.Id.ToString());
            else
                throw;
        }

        // Update stakes for old and new parent
        if (targetContainer.ParentId == null)
            await UpdateStakesAsync(targetContainer.Id);
        else
            await UpdateStakesAsync(targetContainer.ParentId.Value);
        if (oldParentId != null)
            await UpdateStakesAsync(oldParentId.Value);

    }

    public async Task UpdateChildContainerAsync(int parentId, int newWeight, Container childContainer)
    {
        if (parentId != childContainer.ParentId)
            return;

        childContainer.Weight = newWeight;
        await UpdateAsync(childContainer);
    }

    public async Task UpdateChildStakerAsync(int parentId, int newWeight, int stakerId)
    {
        var containerMembership = await _context.ContainerMemberships.FindAsync(parentId, stakerId);

        if (containerMembership == null)
            throw new KeyNotFoundException();

        containerMembership.Weight = newWeight;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!ContainerMembershipExists(containerMembership.ContainerId, containerMembership.StakerId))
            {
                throw new KeyNotFoundException();
            }
            else
            {
                throw;
            }
        }

        await UpdateStakesAsync(containerMembership.ContainerId);
    }

    private bool ContainerExists(int id)
    {
        if (_context.Containers == null)
            throw new NullDbSetException(nameof(Container));

        return (_context.Containers?.Any(e => e.Id == id)).GetValueOrDefault();
    }

    private bool ContainerMembershipExists(int containerId, int stakerId)
    {
        return (_context.ContainerMemberships?.Any(cm => cm.ContainerId == containerId && cm.StakerId == stakerId)).GetValueOrDefault();
    }

    public async Task UpdateStakesAsync(int id)
    {

        // Get parent with child entities
        Container? targetContainer = await _context.Containers
            .Include(c => c.InverseParent)
            .Include(c => c.ContainerMemberships)
            .FirstOrDefaultAsync(c => c.Id == id);

        if (targetContainer == null) throw new KeyNotFoundException();

        // Update stake and local stakes for heirarchy base nodes
        if (targetContainer.ParentId == null)
        {
            // If true root set Stake to 1
            if (targetContainer.Name.ToLower() == "root")
                targetContainer.Stake = 1;
            else
                targetContainer.Stake = 0;

            targetContainer.LocalStake = 1;
        }

        // Calculate total weight of all child entities of the updated parent
        decimal totalWeight = 0;
        foreach (var childContainer in targetContainer.InverseParent)
        {
            totalWeight += childContainer.Weight;
        }
        foreach (var childStaker in targetContainer.ContainerMemberships)
        {
            totalWeight += childStaker.Weight;
        }

        // Calculate stake for all child entities of updated parent
        foreach (var childContainer in targetContainer.InverseParent)
        {
            childContainer.LocalStake = childContainer.Weight / totalWeight;            
            childContainer.Stake = targetContainer.Stake * childContainer.LocalStake;
        }
        foreach (var childStaker in targetContainer.ContainerMemberships)
        {
            childStaker.LocalStake = childStaker.Weight / totalWeight;
            childStaker.Stake = targetContainer.Stake * childStaker.LocalStake;
        }

        // Save changes
        _context.Containers.UpdateRange(targetContainer);
        await _context.SaveChangesAsync();

        // Recursivly update structure
        foreach (var childContainer in targetContainer.InverseParent)
            await UpdateStakesAsync(childContainer.Id);
    }
}
