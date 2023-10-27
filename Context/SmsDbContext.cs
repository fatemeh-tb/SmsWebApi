using DataDomain;
using DataDomain.External;
using Microsoft.EntityFrameworkCore;

namespace Context;

public class SmsDbContext : DbContext
{
	public DbSet<Student> Students { get; set; }
	public DbSet<Course> Courses { get; set; }
	public DbSet<CourseStudent> CourseStudents { get; set; }

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		modelBuilder.Entity<Student>(entity => { entity.ToTable("Students"); });
		modelBuilder.Entity<Course>(entity => { entity.ToTable("Courses"); });

		modelBuilder.Entity<CourseStudent>()
			.HasKey(bc => new { bc.StudentsId, bc.CoursesId });  
		modelBuilder.Entity<CourseStudent>()
			.HasOne(bc => bc.Student)
			.WithMany(b => b.CourseStudent)
			.HasForeignKey(bc => bc.StudentsId);  
		modelBuilder.Entity<CourseStudent>()
			.HasOne(bc => bc.Course)
			.WithMany(c => c.CourseStudent)
			.HasForeignKey(bc => bc.CoursesId);
		
		Student_builder(modelBuilder);
	}


	private void Student_builder(ModelBuilder modelBuilder)
	{
	}
}