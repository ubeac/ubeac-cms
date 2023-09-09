using Entities;
using Microsoft.AspNetCore.Identity;

namespace Services;

public interface IRoleService<TRole> : IBaseEntityService<TRole> where TRole : Role
{
    Task<bool> Exists(string roleName, CancellationToken cancellationToken = default);
}

public class RoleService<TKey, TRole> : IRoleService<TRole> where TRole : Role
{
    protected readonly RoleManager<TRole> RoleManager;

    public RoleService(RoleManager<TRole> roleManager)
    {
        RoleManager = roleManager;
    }

    public async Task Delete(Guid id, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var role = await RoleManager.FindByIdAsync(id.ToString()) ?? throw new Exception("Role not found");
        
        var idResult = await RoleManager.DeleteAsync(role);

        idResult.ThrowIfInvalid();
    }

    public Task<bool> Exists(string roleName, CancellationToken cancellationToken = default)
    {
        return RoleManager.RoleExistsAsync(roleName);
    }

    public Task<List<TRole>> GetAll(CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();
        return Task.FromResult(RoleManager.Roles.ToList());
    }

    public Task<TRole> GetById(Guid id, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();
        return Task.FromResult(RoleManager.Roles.AsEnumerable().Single(r => r.Id.Equals(id)));
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