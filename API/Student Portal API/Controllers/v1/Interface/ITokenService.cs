using Student.Portal.Models.DataBase;

namespace Student_Portal_API.Controllers.v1.Interface
{
    public interface ITokenService
    {
        string CreateJwtToken(User user, DateTime? expiration = null);
    }

}
