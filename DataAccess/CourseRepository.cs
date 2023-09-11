using Context;
using DataDomain;
using DataDomain.External;

namespace DataAccess;

public class CourseRepository
{
	private readonly SmsDbContext _smsContext;
	private readonly IQueryable<Course> _courseResult;

	public CourseRepository()
	{
		_smsContext = new SmsDbContext();
		_smsContext = new SqliteSmsDbContext();
		_smsContext.Database.EnsureCreated();

		_courseResult = _smsContext
			.Courses
			.Select(p => new Course()
			{
				Id = p.Id,
				Title = p.Title,
				CourseCode = p.CourseCode,
				CourseStudent = (ICollection<CourseStudent>)p.CourseStudent.Select(p => new CourseStudent()
				{
					StudentsId = p.StudentsId,
					Student = p.Student
				})
			});
	}

	public int CreateCourse(Course course)
	{
		_smsContext.Courses.Add(course);
		return _smsContext.SaveChanges();
	}

	public IEnumerable<Course> GetAllCourses()
	{
		return _courseResult.ToList();
	}

	public Course GetCourseById(long id)
	{
		return _courseResult.SingleOrDefault(i => i.Id == id);
	}

	public int RemoveCourse(Course course)
	{
		_smsContext.Attach(course);
		_smsContext.Remove(course);
		return _smsContext.SaveChanges();
	}
	
	public bool UpdateCourse(CourseDto courseDto)
	{
		var courseToUpdate = _smsContext.Courses.FirstOrDefault(s => s.Id == courseDto.Id);
		if (courseToUpdate is not null)
		{
			courseToUpdate.Title = courseDto.Title;
			courseToUpdate.CourseCode = courseDto.CourseCode;
		}
		return _smsContext.SaveChanges() > 0;
	}
}