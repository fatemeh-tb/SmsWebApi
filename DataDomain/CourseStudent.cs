namespace DataDomain;

public class CourseStudent
{
	public int CoursesId { get; set; }
	public Course Course { get; set; }
	
	public int StudentsId { get; set; }
	public Student Student { get; set; }
}