using DataAccess;
using DataDomain;
using DataDomain.External;

namespace DataService;

public class CourseDataService
{
	private readonly CourseRepository _courseRepository;

	public CourseDataService()
	{
		_courseRepository = new CourseRepository();
	}

	public int AddCourse(CourseDto courseDto)
	{
		var course = new Course();
		course.Title = courseDto.Title;
		course.CourseCode = courseDto.CourseCode;
		return _courseRepository.CreateCourse(course);
	}

	public IEnumerable<Course> GetCourses()
	{
		return _courseRepository.GetAllCourses();
	}

	public Course GetCourseById(long id)
	{
		return _courseRepository.GetCourseById(id);
	}

	public int DeleteCourse(long id)
	{
		var course = _courseRepository.GetCourseById(id);
		return _courseRepository.RemoveCourse(course);
	}

	public bool UpdateCourse(CourseDto courseDto)
	{
		return _courseRepository.UpdateCourse(courseDto);
	}
}