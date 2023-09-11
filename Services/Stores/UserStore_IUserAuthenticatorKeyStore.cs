using Microsoft.AspNetCore.Identity;


namespace Services;

public partial class UserStore<TUser> : IUserAuthenticatorKeyStore<TUser>
{
    public Task<string?> GetAuthenticatorKeyAsync(TUser user, CancellationToken cancellationToken)
    {
        return Task.FromResult(user.AuthenticatorKey);
    }

    public Task SetAuthenticatorKeyAsync(TUser user, string key, CancellationToken cancellationToken)
    {
        if (user != null)
            user.AuthenticatorKey = key;

        return Task.CompletedTask;
    }
}
