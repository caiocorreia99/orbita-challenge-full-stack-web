using Student.Portal.Models.Api;
using Student.Portal.Models.Binder;

namespace Student_Portal_API.Controllers.v1.Interface
{
    public interface IUserService
    {
        Task<List<UserResponse>> GetUser(int idUser);
        Task CreateUser(UserRequest userRequest);
    }
}
