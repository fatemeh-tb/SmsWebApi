using DataDomain;
using Microsoft.EntityFrameworkCore;

namespace Context;

public class SmsDbContext : DbContext
{
	public DbSet<Student> Students { get; set; }
	public DbSet<Course> Courses { get; set; }
	public DbSet<CourseStudent> CourseStudents { get; set; }

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		modelBuilder.Entity<Student>(entity => { entity.ToTable("Stuents"); });
		modelBuilder.Entity<Course>(entity => { entity.ToTable("Courses"); });
		modelBuilder.Entity<CourseStudent>(entity => { entity.ToTable("CourseStudent"); });


		modelBuilder
			.Entity<Student>()
			.HasMany(p => p.Courses)
			.WithMany(p => p.Students)
			.UsingEntity(j => j.ToTable("StudentCourse"));
		Student_builder(modelBuilder);
	}


	private void Student_builder(ModelBuilder modelBuilder)
	{
	}
}