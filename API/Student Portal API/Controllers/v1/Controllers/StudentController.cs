using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static Student.Portal.Models.Helpers.Constants;
using Student.Portal.Models.Api;
using Student.Portal.Models.Binder;
using Student_Portal_API.Controllers.v1.Interface;
using Student_Portal_API.Helpers;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MySqlX.XDevAPI;
using Student.Portal.Models.DataBase;

namespace Student_Portal_API.Controllers.v1.Controllers
{
    [Authorize]
    [ApiController]
    [Route(Constants.UserRoute)]
    public class StudentController : ControllerBase
    {
        private readonly IStudentService studentService;

        public StudentController(IStudentService StudentService)
        {
            studentService = StudentService;
        }
        
        [HttpGet]
        [Route(Constants.StudentRoute + "/get-student")]
        public async Task<ApiResponse<List<StudentResponse>>> Get([FromQuery] int idStudent)
        {
            try
            {
                var result = await studentService.GetStudent(idStudent);
                return ApiResponse<List<StudentResponse>>.GetSuccessResponse(data: result);
            }
            catch (Exception ex)
            {
                return ApiResponse<List<StudentResponse>>.GetErrorResponse(InternalCode.Catch_Generic, System.Net.HttpStatusCode.InternalServerError, ex.Message, ex); ;
            }
        }
        
        [HttpPost]
        [Route(Constants.StudentRoute + "/create-student")]
        public async Task<ApiResponse<Response>> Post([FromBody] StudentRequest studentRequest)
        {
            try
            {
                await studentService.CreateStudent(studentRequest);
                return ApiResponse<Response>.GetSuccessResponse(message: $"Student created.");
            }
            catch (Exception ex)
            {
                return ApiResponse<Response>.GetErrorResponse(InternalCode.Catch_Generic, System.Net.HttpStatusCode.InternalServerError, ex.Message, exception: ex); ;
            }
        }
        
        [HttpPut]
        [Route(Constants.StudentRoute + "/update-student")]
        public async Task<ApiResponse<Response>> Put([FromBody] StudentRequest studentRequest)
        {
            try
            {
                await studentService.UpdateStudent(studentRequest);
                return ApiResponse<Response>.GetSuccessResponse(message: $"Student Updated.");
            }
            catch (Exception ex)
            {
                return ApiResponse<Response>.GetErrorResponse(InternalCode.Catch_Generic, System.Net.HttpStatusCode.InternalServerError, ex.Message, exception: ex); ;
            }
        }

        [HttpDelete]
        [Route(Constants.StudentRoute + "/disable-student")]
        public async Task<ApiResponse<Response>> Delete([FromQuery] int idStudent)
        {
            try
            {
                await studentService.DisableStudent(idStudent);
                return ApiResponse<Response>.GetSuccessResponse(message: $"Student deleted.");
            }
            catch (Exception ex)
            {
                return ApiResponse<Response>.GetErrorResponse(InternalCode.Catch_Generic, System.Net.HttpStatusCode.InternalServerError, ex.Message, exception: ex); ;
            }
        }

        [HttpGet]
        [Route(Constants.StudentRoute + "/list-students")]
        public async Task<ApiResponse<PaggedList<Students>>> ListStudents([FromQuery] int page = 1, int pageSize = 0, string? search = null)
        {
            try
            {
                var result = await studentService.ListStudent(page, pageSize, search);
                return ApiResponse<PaggedList<Students>>.GetSuccessResponse(data: result);
            }
            catch (Exception ex)
            {
                return ApiResponse<PaggedList<Students>>.GetErrorResponse(InternalCode.Catch_Generic, System.Net.HttpStatusCode.InternalServerError, ex.Message, ex);
            }
        }
    }
}
