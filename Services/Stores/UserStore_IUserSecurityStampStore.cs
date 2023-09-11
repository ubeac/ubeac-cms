using Microsoft.AspNetCore.Identity;

namespace Services;

public partial class UserStore<TUser> : IUserSecurityStampStore<TUser>
{
    public Task<string?> GetSecurityStampAsync(TUser user, CancellationToken cancellationToken)
    {
        return Task.FromResult(user.SecurityStamp);
    }

    public Task SetSecurityStampAsync(TUser user, string stamp, CancellationToken cancellationToken)
    {
        user.SecurityStamp = stamp;
        return Task.CompletedTask;
    }
}
