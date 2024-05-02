namespace MedPrep.Api.Services.Interface;

using static MedPrep.Api.Services.Contracts.AuthServiceContracts;

public interface IAuthService
{
    Task<AuthUserResult> LoginUser(LoginQuery query);
    Task<AuthTeacherResult> LoginTeacher(LoginQuery query);
    Task<AuthUserResult> RefrehsUserToken(RefreshQuery query);
    Task<AuthTeacherResult> RefreshTeacherToken(RefreshQuery query);
    Task RegisterUser(RegisterUserQuery query);
    Task RegisterTeacher(RegisterTeacherQuery query);
}
