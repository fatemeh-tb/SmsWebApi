namespace DataDomain;

public class CourseStudent
{
	public Course Course { get; set; }
	public int CoursesId { get; set; }
	
	public Student Student { get; set; }
	public int StudentsId { get; set; }
}