namespace MedPrep.Api.Services.Common;

using static MedPrep.Api.Services.Contracts.AuthServiceContracts;

public interface IAuthService
{
    Task<AuthUserResult> LoginUser(LoginQuery query);
    Task<AuthTeacherResult> LoginTeacher(LoginQuery query);
    Task<AuthUserResult> RefreshUserToken(RefreshQuery query);
    Task<AuthTeacherResult> RefreshTeacherToken(RefreshQuery query);
    Task RegisterUser(RegisterUserCommand query);
    Task RegisterTeacher(RegisterTeacherCommand query);
    Task ConfirmUser(ConfirmAccountQuery query);
    Task ConfirmTeacher(ConfirmAccountQuery query);
}
