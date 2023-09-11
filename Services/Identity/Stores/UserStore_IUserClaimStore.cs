using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace Services;

public partial class UserStore<TUser> : IUserClaimStore<TUser>
{
    public Task AddClaimsAsync(TUser user, IEnumerable<Claim> claims, CancellationToken cancellationToken)
    {
        foreach (var claim in claims)
        {
            var identityClaim = new IdentityUserClaim<Guid>()
            {
                ClaimType = claim.Type,
                ClaimValue = claim.Value,
                UserId = user.Id
            };

            user.Claims.Add(identityClaim);
        }
        return Task.CompletedTask;
    }

    public Task ReplaceClaimAsync(TUser user, Claim claim, Claim newClaim, CancellationToken cancellationToken)
    {
        user.Claims.RemoveAll(x => x.ClaimType == claim.Type && x.ClaimValue == claim.Value);

        var identityClaim = new IdentityUserClaim<Guid>()
        {
            ClaimType = newClaim.Type,
            ClaimValue = newClaim.Value,
            UserId = user.Id
        };

        user.Claims.Add(identityClaim);

        return Task.CompletedTask;
    }

    public Task RemoveClaimsAsync(TUser user, IEnumerable<Claim> claims, CancellationToken cancellationToken)
    {
        foreach (var claim in claims)
            user.Claims.RemoveAll(x => x.ClaimType == claim.Type && x.ClaimValue == claim.Value);

        return Task.CompletedTask;
    }

    public async Task<IList<TUser>> GetUsersForClaimAsync(Claim claim, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        return (await _repository.FindByClaim(claim, cancellationToken)).ToList();
    }
}
