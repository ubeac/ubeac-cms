using Microsoft.AspNetCore.Identity;

namespace Services;

public partial class UserStore<TUser> : IUserAuthenticationTokenStore<TUser>
{
    public Task SetTokenAsync(TUser user, string loginProvider, string name, string? value, CancellationToken cancellationToken)
    {
        if (user?.Tokens == null)
            return Task.CompletedTask;

        var token = user.Tokens.FirstOrDefault(x => x.LoginProvider == loginProvider && x.Name == name);

        if (token == null)
        {
            token = new IdentityUserToken<Guid>
            {
                LoginProvider = loginProvider,
                Name = name,
                Value = value,
                UserId = user.Id
            };
            user.Tokens.Add(token);
        }
        else
        {
            token.Value = value;
        }
        return Task.CompletedTask;
    }

    public Task RemoveTokenAsync(TUser user, string loginProvider, string name, CancellationToken cancellationToken)
    {
        var userTokens = user.Tokens ?? new List<IdentityUserToken<Guid>>();
        userTokens.RemoveAll(x => x.LoginProvider == loginProvider && x.Name == name);

        return Task.CompletedTask;
    }

    public Task<string?> GetTokenAsync(TUser user, string loginProvider, string name, CancellationToken cancellationToken)
    {
        var userTokens = user.Tokens ?? new List<IdentityUserToken<Guid>>();
        var token = userTokens.FirstOrDefault(x => x.LoginProvider == loginProvider && x.Name == name);
        return Task.FromResult(token == null ? string.Empty : token.Value);
    }
}
