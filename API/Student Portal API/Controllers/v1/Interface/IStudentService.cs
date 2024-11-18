using Student.Portal.Models.Api;
using Student.Portal.Models.Binder;
using Student.Portal.Models.DataBase;

namespace Student_Portal_API.Controllers.v1.Interface
{
    public interface IStudentService
    {
        Task<List<StudentResponse>> GetStudent(int idStudent);
        Task<PaggedList<Students>> ListStudent(int page, int pageSize, string? name = null);
        Task UpdateStudent(StudentRequest studentRequest);
        Task DisableStudent(int idStudent);
        Task CreateStudent(StudentRequest studentRequest);
    }
}
