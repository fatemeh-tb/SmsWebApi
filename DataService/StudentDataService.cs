using System.Collections;
using System.Globalization;
using Context;
using DataAccess;
using DataDomain;
using DataDomain.External;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Hosting.Internal;

namespace DataService;

public class StudentDataService
{
	private readonly StudentRepository _studentRepository;
	private IWebHostEnvironment _environment;

	public StudentDataService(IWebHostEnvironment environment)
	{
		_studentRepository = new StudentRepository();
		_environment = environment;

	}

	public int AddCourseStudent(CourseStudentDto courseStudentDto)
	{
		var coursestudent = new CourseStudent();
		coursestudent.CoursesId = courseStudentDto.CoursesId;
		coursestudent.StudentsId = courseStudentDto.StudentsId;

		return _studentRepository.createCourseStudent(coursestudent);
	}

	public async Task<int> AddStudent(FileViewModel model,IFormFile photo)
	{
		if (photo == null || photo.Length == 0)
		{
			Console.WriteLine("Eroor");
		}

		var path = Path.Combine(_environment.WebRootPath, "Uploads/profilepics", photo.FileName);
		using (FileStream stream = new FileStream(path, FileMode.Create))
		{
			await photo.CopyToAsync(stream);
			stream.Close();
		}

		model.Student.ImageName = photo.FileName;

		if (model != null)
		{
			var student = new Student();
			student.FName = model.Student.FName;
			student.LName = model.Student.LName;
			student.Phone = model.Student.Phone;
			student.ParentName = model.Student.ParentName;
			student.NationalCode = model.Student.NationalCode;
			student.Gender = model.Student.Gender;
		}
		return await _studentRepository.CreateStudent(model);

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
}