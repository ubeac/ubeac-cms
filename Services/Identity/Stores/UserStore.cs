using Entities;
using Microsoft.AspNetCore.Identity;
using Repositories;

namespace Services;

public partial class UserStore<TUser> : IProtectedUserStore<TUser> where TUser : User
{
    private readonly IUserRepository<TUser> _repository;

    public UserStore(IUserRepository<TUser> repository)
    {
        _repository = repository;
    }
}
