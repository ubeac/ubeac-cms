using Entities;

namespace Repositories;

public interface IUserTokenRepository<TUserToken> : IBaseEntityRepository<TUserToken> where TUserToken : UserToken
{
}
