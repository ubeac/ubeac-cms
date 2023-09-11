using Entities;
using Microsoft.AspNetCore.Identity;

namespace Services;

public partial class UserStore<TUser> : IUserRoleStore<TUser> where TUser : User
{

    public Task AddToRoleAsync(TUser user, string roleId, CancellationToken cancellationToken)
    {
        if (Guid.TryParse(roleId, out var id))
            user.Roles.Add(id);
        return Task.CompletedTask;
    }

    public Task RemoveFromRoleAsync(TUser user, string roleId, CancellationToken cancellationToken)
    {
        if (Guid.TryParse(roleId, out var id))
            user.Roles.Remove(id);
        return Task.CompletedTask;
    }

    public async Task<IList<TUser>> GetUsersInRoleAsync(string roleId, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        return await _repository.FindByRoleId(roleId, cancellationToken);
    }

    public Task<IList<string>> GetRolesAsync(TUser user, CancellationToken cancellationToken)
    {
        return Task.FromResult((IList<string>)user.Roles);
    }

    public Task<bool> IsInRoleAsync(TUser user, string roleId, CancellationToken cancellationToken)
    {
        if (Guid.TryParse(roleId, out var id))
            return Task.FromResult(user.Roles.Contains(id));

        return Task.FromResult(false);
    }
}
