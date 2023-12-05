using Context;
using DataDomain;
using DataDomain.External;
using DataService;
using Microsoft.AspNetCore.Mvc;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;

namespace StudentManagementSystemWebApi.Controllers
{
	[Route("[controller]")]
	[ApiController]
	public class StudentController : ControllerBase
	{
		private readonly StudentDataService _studentDataService;
		private readonly SmsDbContext _smsContext;
		private IWebHostEnvironment _environment;


		public StudentController(IHostingEnvironment  environment)
		{
			_studentDataService = new StudentDataService(environment);
			_smsContext = new SmsDbContext();
		}

		[HttpPost]
		public int AddStudent(StudentDto model)
		{
			return _studentDataService.AddStudent(model);
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

		[HttpDelete("/deleteCourseStudent/{id1}/{id2}")]
		public int DeleteRelation(long id1, long id2)
		{
			return _studentDataService.DeleteRelation(id1, id2);
		}
		
	}
}