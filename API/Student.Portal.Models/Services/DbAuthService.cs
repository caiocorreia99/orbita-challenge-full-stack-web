namespace Student.Portal.Models.Services
{
    public interface IDbAuthService
    {
        string GetAccessToken();
    }

    public class DbAuthService : IDbAuthService
    {

        public DbAuthService()
        {
            throw new NotImplementedException();
        }

        string IDbAuthService.GetAccessToken()
        {
            throw new NotImplementedException();
        }
    }
}
