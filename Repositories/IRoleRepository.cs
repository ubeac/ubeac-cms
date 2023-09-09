using Entities;

namespace Repositories;

public interface IRoleRepository<TRole> : IBaseEntityRepository<TRole> where TRole : Role
{
}
