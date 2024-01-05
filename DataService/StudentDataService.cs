using System.Drawing;
using System.Text.RegularExpressions;
using Context;
using DataAccess;
using DataDomain;
using DataDomain.External;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json.Serialization;


namespace DataService;

public class StudentDataService
{
	private readonly SmsDbContext _smsContext;
	private readonly StudentRepository _studentRepository;
	private IHostingEnvironment  _environment;

	public StudentDataService(IHostingEnvironment  environment)
	{
		_smsContext = new SmsDbContext();
		_smsContext = new SqliteSmsDbContext();
		_smsContext.Database.EnsureCreated();
		_studentRepository = new StudentRepository();
		_environment = environment;
	}
	public int  AddStudent(StudentDto model)
	{
		var student = new Student();
		
		student.FName = model.FName;
		student.LName = model.LName;
		student.Phone = model.Phone;
		student.ParentName = model.ParentName;
		student.NationalCode = model.NationalCode;
		student.Gender = model.Gender;
		student.imagePath = model.imagePath;
		
		Console.WriteLine(student.imagePath);
		Console.WriteLine(model.imagePath);

		if (student.imagePath != "string" & student.imagePath != "undefiend" & student.imagePath != null)
		{
			String[] substrings = student.imagePath.Split(',');

			
			string imgData = substrings[1];
			byte[] imageBytes = Convert.FromBase64String(imgData);
			string imageName = student.NationalCode + ".jpg";
		
			var path = Path.Combine(_environment.WebRootPath, "Uploads/profilepics", imageName);

			File.WriteAllBytes(path, imageBytes);	
		}
		else
		{
			Console.WriteLine("add");
		}


		return _studentRepository.CreateStudent(student);
	}

	public IEnumerable<Student> GetStudents()
	{
		return _studentRepository.GetAllStudents();
	}

	public Student GetStudentById(long id)
	{
		return _studentRepository.GetStudentById(id);
	}

	public int DeleteStudent(long id)
	{
		var student = _studentRepository.GetStudentById(id);
		return _studentRepository.RemoveStudent(student);
	}

	public bool UpdateStudent(StudentDto studentDto)
	{
		return _studentRepository.UpdateStudent(studentDto);
		
	}
	
	public int AddCourseStudent(CourseStudentDto courseStudentDto)
	{
		var coursestudent = new CourseStudent();
		coursestudent.CoursesId = courseStudentDto.CoursesId;
		coursestudent.StudentsId = courseStudentDto.StudentsId;

		return _studentRepository.createCourseStudent(coursestudent);
	}
	
	public int DeleteRelation(long studentId, long courseId)
	{
		var stu = _smsContext.CourseStudents.SingleOrDefault(s => s.StudentsId == studentId && s.CoursesId == courseId);

		return _studentRepository.RemoveCourseStudentRelations(stu);
	}
}