using Microsoft.AspNetCore.Identity;

namespace Entities;

public class User : IdentityUser<Guid>, IBaseEntity
{
    public Guid? CreateBy { get; set; }
    public DateTime CreateDate { get; set; }
    public Guid? LastUpdateBy { get; set; }
    public DateTime? LastUpdateDate { get; set; }

    public DateTime? LastLoginDate { get; set; }
    public int LoginCount { get; set; } = 0;
    public DateTime? LastPasswordChangeDate { get; set; }
    public string? LastPasswordChangeBy { get; set; }
    public bool Enabled { get; set; } = false;
    public string? AuthenticatorKey { get; set; }
    public List<IdentityUserClaim<Guid>> Claims { get; set; } = new();
    public List<IdentityUserLogin<Guid>> Logins { get; set; } = new();
    public List<IdentityUserToken<Guid>> Tokens { get; set; } = new();
    public List<TwoFactorRecoveryCode> RecoveryCodes { get; set; } = new();
    public List<Guid> Roles { get; set; } = new();

    public User()
    {
    }

    public User(string userName) : base(userName)
    {
    }
}


public class TwoFactorRecoveryCode
{
    public string Code { get; set; } = string.Empty;
    public bool Redeemed { get; set; }
}

public class SignInResult
{
    public virtual Guid UserId { get; set; }
    public virtual List<Guid> Roles { get; set; } = new();
    public virtual string Token { get; set; } = string.Empty;
    public virtual string RefreshToken { get; set; } = string.Empty;
    public virtual DateTime Expiry { get; set; }
}

public class TokenResult
{
    public virtual string AccessToken { get; set; } = string.Empty;
    public virtual string RefreshToken { get; set; } = string.Empty;
    public virtual DateTime Expiry { get; set; }
}

public class ChangePassword
{
    public Guid UserId { get; set; }
    public string CurrentPassword { get; set; } = string.Empty;
    public string NewPassword { get; set; } = string.Empty;
}

public class UserRegisterOptions
{
    public bool EnableUserOnRegister { get; set; } = false;
}
