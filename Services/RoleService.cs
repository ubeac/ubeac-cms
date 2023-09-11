using Entities;
using Microsoft.AspNetCore.Identity;

namespace Services;

public interface IRoleService<TRole> : IBaseEntityService<TRole> where TRole : Role
{
}

public class RoleService<TKey, TRole> : IRoleService<TRole> where TRole : Role
{
    protected readonly RoleManager<TRole> RoleManager;

    public RoleService(RoleManager<TRole> roleManager)
    {
        RoleManager = roleManager;
    }

    public async Task<bool> Delete(Guid id, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var role = await RoleManager.FindByIdAsync(id.ToString()) ?? throw new Exception("Role not found");
        
        var idResult = await RoleManager.DeleteAsync(role);

        idResult.ThrowIfInvalid();

        return true;
    }

    public async Task<IList<TRole>> GetAll(CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();
        var result = await Task.FromResult(RoleManager.Roles);
        return result.ToList();
    }

    public Task<TRole?> GetById(Guid id, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();
        return Task.FromResult(RoleManager.Roles.AsEnumerable().SingleOrDefault(r => r.Id.Equals(id)));
    }

    public async Task<TRole> Insert(TRole role, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var idResult = await RoleManager.CreateAsync(role);
        idResult.ThrowIfInvalid();
        return role;
    }

    public async Task<TRole> Update(TRole role, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var idResult = await RoleManager.UpdateAsync(role);
        idResult.ThrowIfInvalid();

        return role;
    }
}