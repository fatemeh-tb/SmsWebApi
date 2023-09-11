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


		public StudentController(IWebHostEnvironment environment)
		{
			_studentDataService = new StudentDataService(environment);
			_smsContext = new SmsDbContext();
		}

		[HttpPost]
		public Task<int> AddStudent(FileViewModel model, IFormFile photo)
		{
			return _studentDataService.AddStudent(model, photo);
		}

		[HttpGet("/students")]
		public IEnumerable<Student> GetAllStudents()
		{
			return _studentDataService.GetStudents();
		}

		[HttpGet("/Student/{id}")]
		public Student GetStudentbyId(long id)
		{
			return _studentDataService.GetStudentById(id);
		}


		[HttpDelete("/deleteStudent/{id}")]
		public int DeleteStudent(long id)
		{
			return _studentDataService.DeleteStudent(id);
		}

		[HttpPut]
		public bool UpdateStudent(StudentDto studentDto)
		{
			return _studentDataService.UpdateStudent(studentDto);
		}

		
		[HttpPost("/CourseStudent")]
		public int AddCourseStudent(CourseStudentDto courseStudentDto)
		{
			return _studentDataService.AddCourseStudent(courseStudentDto);
		}


		
	}
}