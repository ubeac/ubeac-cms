using Entities;

namespace Repositories;

public interface IUserRepository<TUser> : IBaseEntityRepository<TUser> where TUser : User
{
}
