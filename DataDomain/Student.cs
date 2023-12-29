
namespace DataDomain;

public class Student
{
	public int Id { get; set; }
	public string? FName { get; set; }
	public string? LName { get; set; }
	public string? Phone { get; set; }
	public string? NationalCode { get; set; }
	public string? ParentName { get; set; }
	public string? Gender { get; set; }
  public string? imagePath { get; set; }
public virtual ICollection<CourseStudent>? CourseStudent { get; set; }
public virtual ICollection<Scores>? Scores { get; set; }
}