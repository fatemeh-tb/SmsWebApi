using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Context;
using DataDomain;
using DataDomain.External;
using DataService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace StudentManagementSystemWebApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly StudentDataService _studentDataService;
        private readonly SmsDbContext _smsContext;


        public StudentController()
        {
            _studentDataService = new StudentDataService();
        }

        [HttpPost]
        public int AddStudent(StudentDto studentDto)
        {
            return _studentDataService.AddStudent(studentDto);
        }

        [HttpGet("/students")]
        public IEnumerable<Student> GetAllStudents()
        {
            return _studentDataService.GetStudents();
        }
    }
}
