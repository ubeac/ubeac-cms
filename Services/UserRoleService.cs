using Entities;
using Microsoft.AspNetCore.Identity;

namespace Services;

public interface IUserRoleService<TUser> where TUser : User
{
    Task<bool> AddRoles(Guid userId, IEnumerable<string> roleNames, CancellationToken cancellationToken = default);
    Task<bool> RemoveRoles(Guid userId, IEnumerable<string> roleNames, CancellationToken cancellationToken = default);
    Task<IList<string>> GetRolesForUser(Guid userId, CancellationToken cancellationToken = default);
    Task<IList<TUser>> GetUsersInRole(string roleNames, CancellationToken cancellationToken = default);
}

public class UserRoleService<TUserKey, TUser> : IUserRoleService<TUser>    where TUser : User
{
    private readonly UserManager<TUser> _userManager;

    public UserRoleService(UserManager<TUser> userManager)
    {
        _userManager = userManager;
    }

    public virtual async Task<bool> AddRoles(Guid userId, IEnumerable<string> roleNames, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var user = await _userManager.FindByIdAsync(userId.ToString());

        var idResult = await _userManager.AddToRolesAsync(user, roleNames);

        idResult.ThrowIfInvalid();

        return true;
    }

    public virtual async Task<IList<string>> GetRolesForUser(Guid userId, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var user = await _userManager.FindByIdAsync(userId.ToString());

        return await _userManager.GetRolesAsync(user);
    }

    public virtual async Task<IList<TUser>> GetUsersInRole(string roleName, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        return await _userManager.GetUsersInRoleAsync(roleName);
    }

    public virtual async Task<bool> RemoveRoles(Guid userId, IEnumerable<string> roleNames, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var user = await _userManager.FindByIdAsync(userId.ToString());

        var idResult = await _userManager.RemoveFromRolesAsync(user, roleNames);

        idResult.ThrowIfInvalid();

        return true;
    }
}