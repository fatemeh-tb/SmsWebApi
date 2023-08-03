namespace DataDomain;

public class Course
{
	public int Id { get; set; }
	public string Title { get; set; }
	public virtual ICollection<Student> Students { get; set; }
}