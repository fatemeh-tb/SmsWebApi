using DataAccess;
using DataDomain;
using DataDomain.External;

namespace DataService;

public class CourseDataService
{
	private readonly ISmsRepository _smsRepository;

	public CourseDataService()
	{
		_smsRepository = new SmsRepository();
	}

	public int AddCourse(CourseDto courseDto)
	{
		var course = new Course();
		course.Title = courseDto.Title;
		return _smsRepository.CreateCourse(course);
	}

	public IEnumerable<Course> GetCourses()
	{
		return _smsRepository.GetAllCourses();
	}
}