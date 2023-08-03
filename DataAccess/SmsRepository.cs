using Context;
using DataDomain;

namespace DataAccess;

public class SmsRepository: ISmsRepository
{
	private readonly SmsDbContext _smsContext;

	public SmsRepository()
	{
		_smsContext = new SmsDbContext();
	}

	public int CreateStudent(Student student)
	{
		_smsContext.Students.Add(student);
		return _smsContext.SaveChanges();
	}

	public IEnumerable<Student> GetAllStudents()
	{
		var result = _smsContext
			.Students
			.Select(s => new Student()
			{
				Id = s.Id,
				FName = s.FName,
				LName = s.LName,
				Phone = s.Phone,
				NationalCode = s.NationalCode,
				ParentName = s.ParentName,
				Gender = s.Gender,
				Courses = (ICollection<Course>)s.Courses.Select(p => new Course()
				{
					Id = p.Id,
					Title = p.Title
				})
			}).ToList();
		return result;
	}


	public int CreateCourse(Course course)
	{
		_smsContext.Courses.Add(course);
		return _smsContext.SaveChanges();
	}

	public IEnumerable<Course> GetAllCourses()
	{
		var result = _smsContext
			.Courses
			.Select(p => new Course()
			{
				Id = p.Id,
				Title = p.Title,
				Students = (ICollection<Student>)p.Students.Select(s => new Student()
				{
					Id = s.Id,
					FName = s.FName,
					LName = s.LName,
					Phone = s.Phone,
					NationalCode = s.NationalCode,
					ParentName = s.ParentName,
					Gender = s.Gender,
				})
			}).ToList();
		return result;
	}
}