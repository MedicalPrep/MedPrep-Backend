namespace MedPrep.Api.Services;

using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Threading.Tasks;
using MedPrep.Api.Config;
using MedPrep.Api.Exceptions;
using MedPrep.Api.Models;
using MedPrep.Api.Repositories.Common;
using MedPrep.Api.Services.Common;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using static MedPrep.Api.Services.Contracts.AuthServiceContracts;

public class AuthService(
    IUserRepository userRepository,
    ITeacherRepository teacherRepository,
    UserManager<User> userManager,
    UserManager<Teacher> teacherManager,
    IUnitOfWork unitOfWork,
    IJwtService jwtService,
    ILogger<IAuthService> logger,
    IEmailService emailService,
    IOptions<RedirectionConfig> redirectionConfig
) : IAuthService
{
    private readonly UserManager<User> userManager = userManager;
    private readonly UserManager<Teacher> teacherManager = teacherManager;
    private readonly IUserRepository userRepository = userRepository;
    private readonly IJwtService jwtService = jwtService;
    private readonly ILogger<IAuthService> logger = logger;
    private readonly IEmailService emailService = emailService;
    private readonly RedirectionConfig redirectionConfig = redirectionConfig.Value;
    private readonly ITeacherRepository teacherRepository = teacherRepository;

    private readonly IUnitOfWork unitOfWork = unitOfWork;

    public async Task<AuthTeacherResult> LoginTeacher(LoginQuery query)
    {
        var teacher = await this.teacherManager.FindByEmailAsync(query.EmailOrUsername);
        if (
            teacher is null
            || !await this.teacherManager.CheckPasswordAsync(teacher, query.Password)
        )
        {
            throw new ConflictException("Invalid email or password");
        }

        var teacherRoles = await this.teacherManager.GetRolesAsync(teacher);

        var authClaims = new List<Claim>
        {
            new(ClaimTypes.NameIdentifier, teacher.Id.ToString()),
            new(ClaimTypes.Name, teacher.Email!),
            new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
        };

        foreach (var teacherRole in teacherRoles)
        {
            authClaims.Add(new Claim(ClaimTypes.Role, teacherRole));
        }

        var accessToken = this.jwtService.GenerateAccessToken(authClaims);

        var refreshToken = this.jwtService.GenerateRefreshToken(authClaims);

        var refreshTokenModel = new RefreshToken
        {
            Token = refreshToken.RefreshToken,
            ExpiresOn = refreshToken.RefreshTokenExpiration
        };

        teacher.RefreshTokens.Add(refreshTokenModel);
        var result = await this.teacherManager.UpdateAsync(teacher);

        if (result.Succeeded is false)
        {
            var unexpectedErrr = new InternalServerErrorException("An unexpected error occured");

            foreach (var error in result.Errors)
            {
                this.logger.LogError(
                    unexpectedErrr,
                    "Refresh Token Error: {Message}",
                    error.Description
                );
            }
            throw unexpectedErrr;
        }

        await this.unitOfWork.SaveChangesAsync();
        return new AuthTeacherResult(
            teacher.Id,
            teacher.Email!,
            accessToken.AccessToken,
            accessToken.AccessTokenExpiration,
            refreshToken.RefreshToken,
            refreshToken.RefreshTokenExpiration
        );
    }

    public async Task<AuthUserResult> LoginUser(LoginQuery query)
    {
        var user = await this.userRepository.GetbyUsernameOrEmailAsync(query.EmailOrUsername);
        if (user is null || !await this.userManager.CheckPasswordAsync(user, query.Password))
        {
            throw new ConflictException("Invalid email or password");
        }

        var userRoles = await this.userManager.GetRolesAsync(user);

        var authClaims = new List<Claim>
        {
            new(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new(ClaimTypes.Name, user.Email!),
            new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
        };

        foreach (var userRole in userRoles)
        {
            authClaims.Add(new Claim(ClaimTypes.Role, userRole));
        }

        var accessToken = this.jwtService.GenerateAccessToken(authClaims);

        var refreshToken = this.jwtService.GenerateRefreshToken(authClaims);

        var refreshTokenModel = new RefreshToken
        {
            Token = refreshToken.RefreshToken,
            ExpiresOn = refreshToken.RefreshTokenExpiration
        };

        user.RefreshTokens.Add(refreshTokenModel);
        var result = await this.userManager.UpdateAsync(user);

        if (result.Succeeded is false)
        {
            var unexpectedErr = new InternalServerErrorException("An unexpected error occured");

            foreach (var error in result.Errors)
            {
                this.logger.LogError(
                    unexpectedErr,
                    "User Refresh Token Error: {Message}",
                    error.Description
                );
            }
            throw unexpectedErr;
        }

        await this.unitOfWork.SaveChangesAsync();

        return new AuthUserResult(
            user.Id,
            user.Email!,
            user.Username,
            accessToken.AccessToken,
            accessToken.AccessTokenExpiration,
            refreshToken.RefreshToken,
            refreshToken.RefreshTokenExpiration
        );
    }

    /// <summary>
    ///  Method <c>RefreshUserToken</c> refreshes the user refresh token.
    ///  The refresh token is decoded and if everything checks out, the token is
    ///  refreshed.
    ///  </summary>
    ///  <param name="query"> Contains the refresh Token.</param>
    /// <exception cref="UnauthorizedException">Thrown if the refresh token is
    /// invalid.</exception>
    public async Task<AuthUserResult> RefreshUserToken(RefreshQuery query)
    {
        if (await this.jwtService.IsRefreshTokenValid(query.RefreshToken) is false)
        {
            throw new UnauthorizedException("Invalid refresh token");
        }

        var claims =
            await this.jwtService.DecodeRefreshTokenClaims(query.RefreshToken)
            ?? throw new UnauthorizedException("Invalid refresh token");

        var userIdClaim =
            claims
                .Where(x => x.Type == ClaimTypes.NameIdentifier)
                .Select(x => x.Value)
                .FirstOrDefault() ?? throw new UnauthorizedException("Invalid refresh token");

        var userId = Guid.Parse(userIdClaim);

        var refreshTokenModel =
            await this.userRepository.GetRefreshTokenAsync(userId, query.RefreshToken)
            ?? throw new UnauthorizedException("Invalid refresh token");

        if (refreshTokenModel.Account is not User user)
        {
            throw new UnauthorizedException("Invalid refresh token");
        }

        if (refreshTokenModel.IsRevoked)
        {
            foreach (var token in user.RefreshTokens)
            {
                token.RevokedOn = DateTime.Now;
            }
            await this.unitOfWork.SaveChangesAsync();
            throw new UnauthorizedException("Invalid refresh token");
        }

        if (refreshTokenModel.IsExpired)
        {
            refreshTokenModel.RevokedOn = DateTime.Now;
            await this.unitOfWork.SaveChangesAsync();
            throw new UnauthorizedException("Invalid refresh token");
        }

        refreshTokenModel.RevokedOn = DateTime.Now;
        var claimsList = claims.ToList();
        var accessToken = this.jwtService.GenerateAccessToken(claimsList);
        var refreshToken = this.jwtService.GenerateRefreshToken(claimsList);

        var newRefreshTokenModel = new RefreshToken
        {
            Token = refreshToken.RefreshToken,
            ExpiresOn = refreshToken.RefreshTokenExpiration
        };

        user!.RefreshTokens.Add(newRefreshTokenModel);
        var result = await this.userManager.UpdateAsync(user);
        await this.unitOfWork.SaveChangesAsync();

        return new AuthUserResult(
            user.Id,
            user.Email!,
            user.Username,
            accessToken.AccessToken,
            accessToken.AccessTokenExpiration,
            refreshToken.RefreshToken,
            refreshToken.RefreshTokenExpiration
        );
    }

    public async Task<AuthTeacherResult> RefreshTeacherToken(RefreshQuery query)
    {
        if (await this.jwtService.IsRefreshTokenValid(query.RefreshToken) is false)
        {
            throw new UnauthorizedException("Invalid refresh token");
        }

        var claims =
            await this.jwtService.DecodeRefreshTokenClaims(query.RefreshToken)
            ?? throw new UnauthorizedException("Invalid refresh token");

        var teacherIdClaims =
            claims
                .Where(x => x.Type == ClaimTypes.NameIdentifier)
                .Select(x => x.Value)
                .FirstOrDefault() ?? throw new UnauthorizedException("Invalid refresh token");

        var teacherId = Guid.Parse(teacherIdClaims);

        var refreshTokenModel =
            await this.teacherRepository.GetRefreshTokenAsync(teacherId, query.RefreshToken)
            ?? throw new UnauthorizedException("Invalid refresh token");

        if (refreshTokenModel.Account is not Teacher teacher)
        {
            throw new UnauthorizedException("Invalid refresh token");
        }

        if (refreshTokenModel.IsRevoked)
        {
            foreach (var token in teacher.RefreshTokens)
            {
                token.RevokedOn = DateTime.Now;
            }
            await this.unitOfWork.SaveChangesAsync();
            throw new UnauthorizedException("Invalid refresh token");
        }

        if (refreshTokenModel.IsExpired)
        {
            refreshTokenModel.RevokedOn = DateTime.Now;
            await this.unitOfWork.SaveChangesAsync();
            throw new UnauthorizedException("Invalid refresh token");
        }

        refreshTokenModel.RevokedOn = DateTime.Now;
        var claimsList = claims.ToList();
        var accessToken = this.jwtService.GenerateAccessToken(claimsList);
        var refreshToken = this.jwtService.GenerateRefreshToken(claimsList);

        var newRefreshTokenModel = new RefreshToken
        {
            Token = refreshToken.RefreshToken,
            ExpiresOn = refreshToken.RefreshTokenExpiration
        };

        teacher!.RefreshTokens.Add(newRefreshTokenModel);
        var result = await this.teacherManager.UpdateAsync(teacher);
        await this.unitOfWork.SaveChangesAsync();

        return new AuthTeacherResult(
            teacher.Id,
            teacher.Email!,
            accessToken.AccessToken,
            accessToken.AccessTokenExpiration,
            refreshToken.RefreshToken,
            refreshToken.RefreshTokenExpiration
        );
    }

    public async Task RegisterTeacher(RegisterTeacherCommand query)
    {
        if (await this.teacherRepository.CheckEmailAsync(query.Email))
        {
            throw new ConflictException("Email already exists");
        }

        this.unitOfWork.BeginTransaction();

        try
        {
            var teacher = new Teacher()
            {
                Email = query.Email,
                Firstname = query.Firstname,
                Lastname = query.Lastname,
            };
            var result = await this.teacherManager.CreateAsync(teacher, query.Password);

            if (result.Succeeded is false)
            {
                var unexpectedErr = new InternalServerErrorException("An unexpected error occured");
                foreach (var error in result.Errors)
                {
                    this.logger.LogError(
                        unexpectedErr,
                        "User Registration Error: {Message}",
                        error.Description
                    );
                }
                throw unexpectedErr;
            }
            var token = await this.teacherManager.GenerateEmailConfirmationTokenAsync(teacher);

            var confirmationLink =
                $"{this.redirectionConfig.ConfirmationUrl}/?token={token}&id={teacher.Id}";

            await this.SendConfirmationEmailAsync(teacher.Email, confirmationLink);
            _ = await this.teacherManager.AddToRoleAsync(teacher, TeacherRoles.Teacher.ToString());

            await this.unitOfWork.SaveChangesAsync();
            this.unitOfWork.CommitTransaction();
        }
        catch (Exception)
        {
            this.unitOfWork.RollbackTransaction();
            throw;
        }
    }

    public async Task RegisterUser(RegisterUserCommand query)
    {
        if (await this.userRepository.CheckEmailAsync(query.Email))
        {
            throw new ConflictException("Email already exists");
        }

        if (await this.userRepository.CheckUsernameAsync(query.Username))
        {
            throw new ConflictException("Username already exists");
        }
        this.unitOfWork.BeginTransaction();

        try
        {
            var user = new User()
            {
                Username = query.Username,
                Email = query.Email,
                Firstname = query.Firstname,
                Lastname = query.Lastname,
            };
            var result = await this.userManager.CreateAsync(user, query.Password);

            if (result.Succeeded is false)
            {
                var unexpectedErr = new InternalServerErrorException("An unexpected error occured");
                foreach (var error in result.Errors)
                {
                    this.logger.LogError(
                        unexpectedErr,
                        "User Registration Error: {Message}",
                        error.Description
                    );
                }
                throw unexpectedErr;
            }
            var token = await this.userManager.GenerateEmailConfirmationTokenAsync(user);

            var confirmationLink =
                $"{this.redirectionConfig.ConfirmationUrl}/?token={token}&id={user.Id}";

            await this.SendConfirmationEmailAsync(user.Email, confirmationLink);
            _ = await this.userManager.AddToRoleAsync(user, UserRoles.User.ToString());

            await this.unitOfWork.SaveChangesAsync();
            this.unitOfWork.CommitTransaction();
        }
        catch (Exception)
        {
            this.unitOfWork.RollbackTransaction();
            throw;
        }
    }

    private async Task SendConfirmationEmailAsync(string email, string confirmationLink)
    {
        await this.emailService.SendEmailAsync(
            email,
            "Confirm Email",
            $"Please confirm your account by <a href='{confirmationLink}'>clicking here</a>;."
        );
        return;
    }

    public async Task ConfirmUser(ConfirmAccountQuery query)
    {
        var user =
            await this.userManager.FindByIdAsync(query.UserId.ToString())
            ?? throw new NotFoundException("Account not found");

        var result = await this.userManager.ConfirmEmailAsync(user, query.Token);

        if (result.Succeeded is false)
        {
            throw new UnauthorizedException("Invalid Token");
        }
        return;
    }

    public async Task ConfirmTeacher(ConfirmAccountQuery query)
    {
        var teacher =
            await this.teacherManager.FindByIdAsync(query.UserId.ToString())
            ?? throw new NotFoundException("Account not found");

        var result = await this.teacherManager.ConfirmEmailAsync(teacher, query.Token);

        if (result.Succeeded is false)
        {
            throw new UnauthorizedException("Invalid Token");
        }
        return;
    }
}
