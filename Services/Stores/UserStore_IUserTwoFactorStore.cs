using Microsoft.AspNetCore.Identity;

namespace Services;

public partial class UserStore<TUser> : IUserTwoFactorStore<TUser>
{
    public Task<bool> GetTwoFactorEnabledAsync(TUser user, CancellationToken cancellationToken)
    {
        return Task.FromResult(user.TwoFactorEnabled);
    }

    public Task SetTwoFactorEnabledAsync(TUser user, bool enabled, CancellationToken cancellationToken)
    {
        user.TwoFactorEnabled = enabled;
        return Task.CompletedTask;
    }
}
