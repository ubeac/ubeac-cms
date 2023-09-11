using Entities;
using System.Security.Claims;

namespace Repositories;

public interface IUserRepository<TUser> : IBaseEntityRepository<TUser> where TUser : User
{
    Task<IList<TUser>> FindByClaim(Claim claim, CancellationToken cancellationToken);
    Task<TUser?> FindByEmail(string normalizedEmail, CancellationToken cancellationToken);
    Task<TUser?> FindByLogin(string loginProvider, string providerKey, CancellationToken cancellationToken);
    Task<TUser?> FindByUserName(string normalizedUserName, CancellationToken cancellationToken);
    Task<IList<TUser>> FindByRoleId(string roleId, CancellationToken cancellationToken);
}
