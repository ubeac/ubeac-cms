using Microsoft.AspNetCore.Identity;

namespace Services;

public partial class UserStore<TUser> : IQueryableUserStore<TUser>
{
    public IQueryable<TUser> Users => _repository.AsQueryable();
}
