using DataDomain;
using Microsoft.EntityFrameworkCore;

namespace Context;

public class SmsDbContext : DbContext
{
	public DbSet<Student> Students { get; set; }
	public DbSet<Course> Courses { get; set; }

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		modelBuilder.Entity<Student>(entity =>
		{
			entity.ToTable("Stuents"); 
			
		});
		modelBuilder.Entity<Course>(entity =>
		{
			entity.ToTable("Courses"); 
			
		});

		modelBuilder
			.Entity<Student>()
			.HasMany(p => p.Courses)
			.WithMany(p => p.Students);
		
		Student_builder(modelBuilder);
	}


	private void Student_builder(ModelBuilder modelBuilder)
	{
	}
}