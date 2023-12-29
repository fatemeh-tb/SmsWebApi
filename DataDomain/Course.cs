namespace DataDomain;

public class Course
{
	public int Id { get; set; }
	public string Title { get; set; }
	public string CourseCode { get; set; }
	public virtual ICollection<CourseStudent> CourseStudent { get; set; }
	public virtual List<Scores> Scores { get; set; }
}