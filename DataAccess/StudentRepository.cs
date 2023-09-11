using Context;
using DataDomain;
using DataDomain.External;
using Microsoft.AspNetCore.Mvc;

namespace DataAccess;

public class StudentRepository
{
	private readonly SmsDbContext _smsContext;
	private readonly IQueryable<Student> _studentResult;

	public StudentRepository()
	{
		_smsContext = new SmsDbContext();
		_smsContext = new SqliteSmsDbContext();
		_smsContext.Database.EnsureCreated();

		_studentResult = _smsContext
			.Students
			.Select(s => new Student()
			{
				Id = s.Student.Id,
				FName = s.Student.FName,
				LName = s.Student.LName,
				Phone = s.Student.Phone,
				NationalCode = s.Student.NationalCode,
				ParentName = s.Student.ParentName,
				Gender = s.Student.Gender,
				CourseStudent = (ICollection<CourseStudent>)s.Student.CourseStudent.Select(p => new CourseStudent()
				{
					CoursesId = p.CoursesId,
					Course = p.Course,
				})
			});
	}

	public Task<int> CreateStudent(FileViewModel student)
	{
		
		_smsContext.Students.Add(student);
		return _smsContext.SaveChangesAsync();
	}

	public IEnumerable<Student> GetAllStudents()
	{
		return _studentResult.ToList();
	}

	public Student GetStudentById(long id)
	{
		return _studentResult.SingleOrDefault(i => i.Id == id);
	}

	public int RemoveStudent(Student student)
	{
		_smsContext.Attach(student);
		_smsContext.Remove(student);
		return _smsContext.SaveChanges();
	}


	public bool UpdateStudent(StudentDto studentDto)
	{
		var studentToUpdate = _smsContext.Students.FirstOrDefault(s => s.Student.Id == studentDto.Id);
		if (studentToUpdate is not null)
		{
			studentToUpdate.Student.FName = studentDto.FName;
			studentToUpdate.Student.LName = studentDto.LName;
			studentToUpdate.Student.ParentName = studentDto.ParentName;
			studentToUpdate.Student.NationalCode = studentDto.NationalCode;
			studentToUpdate.Student.Phone = studentDto.Phone;
			studentToUpdate.Student.Gender = studentDto.Gender;
		}

		return _smsContext.SaveChanges() > 0;
	}


	public int createCourseStudent(CourseStudent courseStudent)
	{
		_smsContext.CourseStudents.Add(courseStudent);
		return _smsContext.SaveChanges();
	}
}