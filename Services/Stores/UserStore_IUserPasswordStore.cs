using Microsoft.AspNetCore.Identity;

namespace Services;

public partial class UserStore<TUser> : IUserPasswordStore<TUser>
{
    public Task<string?> GetPasswordHashAsync(TUser user, CancellationToken cancellationToken)
    {
        return Task.FromResult(user.PasswordHash);
    }

    public Task<bool> HasPasswordAsync(TUser user, CancellationToken cancellationToken)
    {
        return Task.FromResult(user.PasswordHash != null);
    }

    public Task SetPasswordHashAsync(TUser user, string? passwordHash, CancellationToken cancellationToken)
    {
        if (user != null)
            user.PasswordHash = passwordHash;

        return Task.CompletedTask;
    }
}
