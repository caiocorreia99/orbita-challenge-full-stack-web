using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Student.Portal.Models.Api;
using Student.Portal.Models.Binder;
using Student.Portal.Models.DataBase;
using Student_Portal_API.Controllers.v1.Interface;
using Student_Portal_API.Controllers.v1.Model;
using Student_Portal_API.Helpers;
using System.Xml.Linq;
using System;

namespace Student_Portal_API.Controllers.v1.Services
{
    public class StudentService : IStudentService
    {

        private readonly IDatabaseFactory<DatabaseConnection> databaseFactory;
        private readonly APIEnvironment env;

        public StudentService(
            IDatabaseFactory<DatabaseConnection> databaseFactory,
            IOptions<APIEnvironment> env)
        {
            this.databaseFactory = databaseFactory;
            this.env = env.Value;
        }

        public async Task<StudentResponse> GetStudent(int IdStudent)
        {
            using var db = databaseFactory.Create();

            var result = await db.Students.FirstOrDefaultAsync(m => m.IdStudent == IdStudent);

            var studentResponse = new StudentResponse {
                IdStudent = result.IdStudent,
                Name = result.Name,
                Email = result.Email,
                Active = result.Active,
                CPF = result.CPF,
                RA = result.RA                
            };

            return studentResponse;
        }

        public async Task<PaggedList<Students>> ListStudent(int page, int pageSize, string? search = null)
        {
            using var db = databaseFactory.Create();

            IQueryable<Students> query = db.Students;

            if (!string.IsNullOrEmpty(search))
                query = query.Where(m => m.Name.Contains(search) || m.Email.Contains(search) || m.RA.ToString().Contains(search));

            var totalCount = await query.CountAsync();
            if (pageSize == 0) pageSize = totalCount;
            var pageRange = (int)Math.Ceiling(totalCount / (decimal)pageSize);

            var result = await query
                    .OrderBy(s => s.Name)
                    .Skip(pageSize * (page - 1))
                    .Take(pageSize)
                    .ToListAsync();

            return new PaggedList<Students>(page, pageSize, pageRange, totalCount, result);
        }

        public async Task UpdateStudent(StudentRequest studentRequest)
        {

            if (studentRequest == null)
                throw new Exception("object not sent");

            using var db = databaseFactory.Create();

            var entity = db.Students.FirstOrDefault(s => s.IdStudent == studentRequest.IdStudent) ??
                throw new Exception("student not found");

            var checkEmail = await db.Students.FirstOrDefaultAsync(u => u.Email == studentRequest.Email);
            if (checkEmail != null) {
                if (checkEmail.Email == studentRequest.Email && checkEmail.IdStudent != studentRequest.IdStudent)
                    throw new Exception($"Email already in use");
            }
            
            entity.Name = studentRequest.Name;            
            entity.Email = studentRequest.Email;

            db.Students.Update(entity);
            await db.SaveChangesAsync();
        }

        public async Task DisableStudent(int idStudent)
        {

            if (idStudent == null)
                throw new Exception("object not sent");

            using var db = databaseFactory.Create();

            var entity = db.Students.FirstOrDefault(s => s.IdStudent == idStudent) ??
                throw new Exception("student not found");
            
            entity.Active = false;

            db.Students.Update(entity);
            await db.SaveChangesAsync();
        }

        public async Task CreateStudent(StudentRequest studentRequest)
        {

            using var db = databaseFactory.Create();

            if (studentRequest == null)
                throw new Exception("object not sent");

            if (studentRequest.Name == null)
                throw new Exception("Name not sent");

            if (studentRequest.Email == null)
                throw new Exception("Email not sent");

            if (studentRequest.CPF == null)
                throw new Exception("CPF not sent");

            if (!APIHelper.ValidMail(studentRequest.Email))
                throw new Exception("Invalid Email");

            var checkEmail = await db.Students.FirstOrDefaultAsync(u => u.Email == studentRequest.Email);
            if (checkEmail != null)
                throw new Exception($"Email already in use");

            var checkCPF = await db.Students.FirstOrDefaultAsync(u => u.CPF == studentRequest.CPF);
            if (checkCPF != null)
                throw new Exception($"CPF already in use");

            var checkRA = await db.Students.FirstOrDefaultAsync(u => u.RA == studentRequest.RA);
            if (checkRA != null)
                throw new Exception($"RA already in use");

            var student = new Students()
            {                
                Name = studentRequest.Name,
                Email = studentRequest.Email,
                Active = studentRequest.Active,
                CPF = studentRequest.CPF,
                RA = studentRequest.RA,
            };

            await db.Students.AddAsync(student);
            await db.SaveChangesAsync();

        }
    }
}
