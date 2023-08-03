using DataDomain;

namespace DataAccess;

public interface ISmsRepository
{
	public int CreateStudent(Student student);
	public IEnumerable<Student> GetAllStudents();
	public int CreateCourse(Course course);
	public IEnumerable<Course> GetAllCourses ();
}