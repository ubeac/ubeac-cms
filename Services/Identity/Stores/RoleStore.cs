using Entities;
using Microsoft.AspNetCore.Identity;
using Repositories;
using System.Security.Claims;

namespace Services;

public class RoleStore<TRole> : IRoleClaimStore<TRole>, IQueryableRoleStore<TRole> where TRole : Role
{

    private readonly IRoleRepository<TRole> _repository;

    public IQueryable<TRole> Roles => _repository.AsQueryable();

    public RoleStore(IRoleRepository<TRole> repository)
    {
        _repository = repository;
    }

    public async Task<IdentityResult> CreateAsync(TRole role, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        await _repository.Insert(role, cancellationToken: cancellationToken);
        return IdentityResult.Success;
    }

    public async Task<IdentityResult> UpdateAsync(TRole role, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        await _repository.Update(role, cancellationToken: cancellationToken);
        return IdentityResult.Success;
    }

    public async Task<IdentityResult> DeleteAsync(TRole role, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        await _repository.Delete(role.Id, cancellationToken: cancellationToken);
        return IdentityResult.Success;
    }

    public Task<string> GetRoleIdAsync(TRole role, CancellationToken cancellationToken)
    {
        return Task.FromResult(role.Id.ToString());
    }

    public Task<string?> GetRoleNameAsync(TRole role, CancellationToken cancellationToken)
    {
        return Task.FromResult(role.Name);
    }

    public Task SetRoleNameAsync(TRole role, string? roleName, CancellationToken cancellationToken)
    {
        role.Name = roleName;
        return Task.CompletedTask;
    }

    public Task<string?> GetNormalizedRoleNameAsync(TRole role, CancellationToken cancellationToken)
    {
        return Task.FromResult(role.NormalizedName);
    }

    public Task SetNormalizedRoleNameAsync(TRole role, string? normalizedRoleName, CancellationToken cancellationToken)
    {
        role.NormalizedName = normalizedRoleName;
        return Task.CompletedTask;
    }

    public async Task<TRole?> FindByIdAsync(string roleId, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        if (Guid.TryParse(roleId, out Guid id))
            return await _repository.GetById(id, cancellationToken);

        return default;
    }

    public async Task<TRole?> FindByNameAsync(string normalizedRoleName, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        return await _repository.FindByName(normalizedRoleName, cancellationToken);
    }

    public async Task<IList<Claim>> GetClaimsAsync(TRole role, CancellationToken cancellationToken = default)
    {
        if (role.Claims is null)
            return await Task.FromResult(new List<Claim>());

        return role.Claims.Select(e => new Claim(e.ClaimType ?? string.Empty, e.ClaimValue ?? string.Empty)).ToList();
    }

    public Task AddClaimAsync(TRole role, Claim claim, CancellationToken cancellationToken = default)
    {
        if (role.Claims is null)
            role.Claims = new List<IdentityRoleClaim<Guid>>();

        var currentClaim = role.Claims.FirstOrDefault(c => c.ClaimType == claim.Type && c.ClaimValue == claim.Value);

        if (currentClaim == null)
        {
            var identityRoleClaim = new IdentityRoleClaim<Guid>()
            {
                ClaimType = claim.Type,
                ClaimValue = claim.Value,
                RoleId = role.Id,
                Id = 0
            };

            role.Claims.Add(identityRoleClaim);
        }
        return Task.CompletedTask;
    }

    public Task RemoveClaimAsync(TRole role, Claim claim, CancellationToken cancellationToken = default)
    {
        role.Claims.RemoveAll(x => x.ClaimType == claim.Type && x.ClaimValue == claim.Value);
        return Task.CompletedTask;
    }

    protected virtual void Dispose(bool disposing)
    {
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
}