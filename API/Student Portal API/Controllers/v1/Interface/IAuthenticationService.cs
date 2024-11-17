using Student.Portal.Models.Binder;

namespace Student_Portal_API.Controllers.v1.Interface
{
    public interface IAuthenticationService
    {
        Task<LoginResponse> Login(LoginRequest login);
        Task<LoginResponse> RefreshToken(int idUser);
        Task<bool> Logout(int IdUser);

    }
}
