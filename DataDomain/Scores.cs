namespace DataDomain;

public class Scores
{
	public int Id { get; set; }
	public int? Score1 { get; set; }
	public int? Score2 { get; set; }
	public int? Score3 { get; set; }
	public int? Score4 { get; set; }
	public int? FinalScore { get; set; }
	public int StudentId { get; set; }
	public int CourseId { get; set; }
	public Student Student { get; set;}
	public Course Course { get; set; }
}