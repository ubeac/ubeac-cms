using System.ComponentModel;
using System.Security.Claims;
using Entities;
using Microsoft.AspNetCore.Identity;
using Providers;
using Repositories;
using SignInResult = Entities.SignInResult;

namespace Services;

public interface IUserService<TUser, TUserToken> where TUser : User where TUserToken : UserToken
{
    Task Create(TUser user, string password, CancellationToken cancellationToken = default);
    Task<TUser> Register(string username, string email, string password, CancellationToken cancellationToken = default);
    Task<SignInResult> Authenticate(string username, string password, CancellationToken cancellationToken = default);
    Task<Guid> GetCurrentUserId(CancellationToken cancellationToken = default);
    Task ChangePassword(TUser user, string newPassword, CancellationToken cancellationToken = default);
    Task ChangePassword(ChangePassword changePassword, CancellationToken cancellationToken = default);
    Task ForgotPassword(string username, CancellationToken cancellationToken = default);
    Task ResetPassword(string username, string token, string newPassword, CancellationToken cancellationToken = default);
    Task RevokeTokens(Guid id, CancellationToken cancellationToken = default);
    Task<SignInResult> RefreshToken(string refreshToken, string expiredToken, CancellationToken cancellationToken = default);
    Task<IEnumerable<TUser>> GetAll(CancellationToken cancellationToken = default);
    Task<TUser> GetById(Guid id, CancellationToken cancellationToken = default);
    Task<IEnumerable<Claim>> GetClaims(TUser user, CancellationToken cancellationToken = default);
    Task Update(TUser user, CancellationToken cancellationToken = default);
    Task<bool> ExistsUserName(string userName, CancellationToken cancellationToken = default);
}


public class UserService<TUser, TUserToken> : IUserService<TUser, TUserToken> where TUser : User where TUserToken : UserToken
{
    const string LOCAL_LOGIN_PROVIDER = "local";
    const string REFRESH_TOKEN_NAME = "refreshToken";

    protected readonly UserManager<TUser> UserManager;
    protected readonly ITokenService<TUser> TokenService;
    protected readonly IUserTokenRepository<TUserToken> UserTokenRepository;
    protected readonly IEmailProvider EmailProvider;
    protected readonly IApplicationContext AppContext;
    protected readonly IHttpContextAccessor Accessor;
    protected readonly UserRegisterOptions Options;

    public UserService(UserManager<TUser> userManager, ITokenService<TUser> tokenService, IUserTokenRepository<TUserToken> userTokenRepository, IEmailProvider emailProvider, IApplicationContext appContext, IHttpContextAccessor accessor, Microsoft.Extensions.Options.IOptions<UserRegisterOptions> options)
    {
        UserManager = userManager;
        TokenService = tokenService;
        UserTokenRepository = userTokenRepository;
        EmailProvider = emailProvider;
        AppContext = appContext;
        Accessor = accessor;
        Options = options.Value;
    }

    public virtual async Task Create(TUser user, string password, CancellationToken cancellationToken = default)
    {
        var identityResult = await UserManager.CreateAsync(user, password);
        identityResult.ThrowIfInvalid();
    }

    public virtual async Task<TUser> Register(string username, string email, string password, CancellationToken cancellationToken = default)
    {
        var user = Activator.CreateInstance<TUser>();
        user.UserName = username;
        user.Email = email;
        user.EmailConfirmed = false;
        user.PhoneNumberConfirmed = false;
        user.Enabled = Options.EnableUserOnRegister;
        await Create(user, password, cancellationToken);
        return user;
    }

    public virtual async Task<SignInResult> Authenticate(string username, string password, CancellationToken cancellationToken = default)
    {
        var user = await UserManager.FindByNameAsync(username);

        // Validate user password
        if (user is null || !user.Enabled || !await UserManager.CheckPasswordAsync(user, password)) throw new Exception("User doesn't exist or username/password is not valid!");

        // Generate token
        var token = await TokenService.Generate(user);

        // Store refresh token
        var identityResult = await UserManager.SetAuthenticationTokenAsync(user, LOCAL_LOGIN_PROVIDER, REFRESH_TOKEN_NAME, token.RefreshToken);
        identityResult.ThrowIfInvalid();

        // Update user properties related to login
        user.LastLoginDate = DateTime.Now;
        user.LoginCount++;
        await UserManager.UpdateAsync(user);

        return new SignInResult
        {
            UserId = user.Id,
            Roles = user.Roles,
            Token = token.AccessToken,
            RefreshToken = token.RefreshToken,
            Expiry = token.Expiry
        };
    }

    public Task<Guid> GetCurrentUserId(CancellationToken cancellationToken = default)
    {
        if (Accessor.HttpContext == null) return default;

        var userId = UserManager.GetUserId(Accessor.HttpContext.User);
        return string.IsNullOrEmpty(userId) ? default : Task.FromResult(userId.GetTypedKey<TUserKey>());
    }

    public async Task ChangePassword(TUser user, string newPassword, CancellationToken cancellationToken = default)
    {
        var token = await UserManager.GeneratePasswordResetTokenAsync(user);
        var result = await UserManager.ResetPasswordAsync(user, token, newPassword);

        result.ThrowIfInvalid();
    }

    public async Task<IEnumerable<Claim>> GetClaims(TUser user, CancellationToken cancellationToken = default)
    {
        var claims = new List<Claim>
        {
            new(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new(ClaimTypes.Name, user.NormalizedUserName??"")
        };
        if (!string.IsNullOrWhiteSpace(user.NormalizedEmail)) claims.Add(new Claim(ClaimTypes.Email, user.NormalizedEmail));
        if (!string.IsNullOrWhiteSpace(user.PhoneNumber)) claims.Add(new Claim(ClaimTypes.Email, user.PhoneNumber));

        var userRoles = await UserManager.GetRolesAsync(user);
        var userRoleClaims = userRoles.Select(userRole => new Claim(ClaimTypes.Role, userRole));
        claims.AddRange(userRoleClaims);

        return claims;
    }

    public async Task Update(TUser entity, CancellationToken cancellationToken = default)
    {
        await UserManager.UpdateAsync(entity);
    }

    public async Task<bool> ExistsUserName(string userName, CancellationToken cancellationToken = default)
    {
        userName = userName.ToUpperInvariant();
        return await UserManager.FindByNameAsync(userName) != null;
    }

    public async Task<bool> Delete(Guid id, CancellationToken cancellationToken = default)
    {
        var user = await UserManager.FindByIdAsync(id.ToString());
        if (user is null) throw new Exception("User doesn't exist!");

        var userRemoveResult = await UserManager.DeleteAsync(user);
        userRemoveResult.ThrowIfInvalid();

        return true;
    }

    public async Task<IEnumerable<TUser>> GetAll(CancellationToken cancellationToken = default)
    {
        // TODO: Check this: ToListAsync() is not working - throws exception! For this reason, the ToList() method is used
        return await Task.Run(() => UserManager.Users.ToList(), cancellationToken);
    }

    public Task<TUser> GetById(Guid id, CancellationToken cancellationToken = default)
    {
        return UserManager.FindByIdAsync(id.ToString());
    }

    public async Task ChangePassword(ChangePassword changePassword, CancellationToken cancellationToken = default)
    {
        var user = await GetById(changePassword.UserId, cancellationToken);

        if (changePassword.UserId == null || user == null || !await UserManager.CheckPasswordAsync(user, changePassword.CurrentPassword))
            throw new Exception("User doesn't exist or username/password is not valid!");

        var idResult = await UserManager.ChangePasswordAsync(user, changePassword.CurrentPassword, changePassword.NewPassword);

        idResult.ThrowIfInvalid();

        // Update user properties related to password changing
        user.LastPasswordChangeDate = DateTime.Now;
        user.LastPasswordChangeBy = AppContext.UserName;
        await UserManager.UpdateAsync(user);
    }

    public async Task ForgotPassword(string username, CancellationToken cancellationToken = default)
    {
        var user = await UserManager.FindByNameAsync(username);
        if (user == null) throw new Exception("User does not exist!");

        var resetPasswordToken = await UserManager.GeneratePasswordResetTokenAsync(user);
        await SendResetPasswordToken(user, resetPasswordToken);
    }

    protected virtual async Task SendResetPasswordToken(TUser user, string token)
    {
        // Send email in background to prevent in interruptions
        var backgroundWorker = new BackgroundWorker();
        backgroundWorker.DoWork += async (_, _) => await EmailProvider.Send(user.Email, ForgotPasswordMessage.Subject, ForgotPasswordMessage.GetBodyWithReplaces(token));
        backgroundWorker.RunWorkerAsync();
        await Task.CompletedTask;
    }

    public async Task ResetPassword(string username, string token, string newPassword, CancellationToken cancellationToken = default)
    {
        var user = await UserManager.FindByNameAsync(username);
        if (user == null) throw new Exception("User does not exist!");

        var result = await UserManager.ResetPasswordAsync(user, token, newPassword);
        result.ThrowIfInvalid();

        // Update user properties related to password changing
        user.LastPasswordChangeDate = DateTime.Now;
        user.LastPasswordChangeBy = AppContext.UserName;
        await UserManager.UpdateAsync(user);
    }

    public async Task RevokeTokens(Guid id, CancellationToken cancellationToken = default)
    {
        var user = await UserManager.FindByIdAsync(id.ToString());
        if (user == null) return;

        var identityResult = await UserManager.ResetAuthenticatorKeyAsync(user);
        identityResult.ThrowIfInvalid();

    }

    public async Task<SignInResult> RefreshToken(string refreshToken, string expiredToken, CancellationToken cancellationToken = default)
    {
        var userId = await TokenService.ValidateExpiredToken(expiredToken);
        var user = await UserManager.FindByIdAsync(userId.ToString());
        if (!user.Enabled) throw new Exception("User is disabled!");

        var storedRefreshToken = await UserManager.GetAuthenticationTokenAsync(user, LOCAL_LOGIN_PROVIDER, REFRESH_TOKEN_NAME);
        if (storedRefreshToken != refreshToken) throw new Exception("Token expired!");

        var newToken = await TokenService.Generate(user);

        // Update user properties related to login
        user.LastLoginDate = DateTime.Now;
        user.LoginCount++;
        await UserManager.UpdateAsync(user);

        return new SignInResult
        {
            UserId = user.Id,
            Roles = user.Roles,
            Token = newToken.AccessToken,
            RefreshToken = refreshToken,
            Expiry = newToken.Expiry
        };
    }
}
