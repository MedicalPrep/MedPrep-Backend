namespace MedPrep.Api.Services;

using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Threading.Tasks;
using MedPrep.Api.Config;
using MedPrep.Api.Exceptions;
using MedPrep.Api.Models;
using MedPrep.Api.Repositories.Interfaces;
using MedPrep.Api.Services.Interface;
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
            new(ClaimTypes.Name, teacher.Email!),
            new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
        };

        foreach (var teacherRole in teacherRoles)
        {
            authClaims.Add(new Claim(ClaimTypes.Role, teacherRole));
        }

        var accessToken = this.jwtService.GenerateAccessToken(authClaims);

        var refreshToken = this.jwtService.GenerateRefreshToken(authClaims);

        var result = await this.teacherManager.SetAuthenticationTokenAsync(
            teacher,
            "RefreshToken",
            "",
            refreshToken.RefreshToken
        );

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
            new(ClaimTypes.Name, user.Email!),
            new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
        };

        foreach (var userRole in userRoles)
        {
            authClaims.Add(new Claim(ClaimTypes.Role, userRole));
        }

        var accessToken = this.jwtService.GenerateAccessToken(authClaims);

        var refreshToken = this.jwtService.GenerateRefreshToken(authClaims);

        var result = await this.userManager.SetAuthenticationTokenAsync(
            user,
            "RefreshToken",
            "",
            refreshToken.RefreshToken
        );

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

    public Task<AuthUserResult> RefrehsUserToken(RefreshQuery query)
    {
        throw new NotImplementedException();
    }

    public Task<AuthTeacherResult> RefreshTeacherToken(RefreshQuery query) =>
        throw new NotImplementedException();

    public async Task RegisterTeacher(RegisterTeacherQuery query)
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

    public async Task RegisterUser(RegisterUserQuery query)
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
}
