using Context;
using DataDomain;
using DataDomain.External;

namespace DataAccess;

public class StudentRepository
{
	private readonly SmsDbContext _smsContext;
	private readonly IQueryable<Student> _studentResult;

	public StudentRepository()
	{
		_smsContext = new SmsDbContext();
		_smsContext = new SqliteSmsDbContext();
		_smsContext.Database.EnsureCreated();

		_studentResult = _smsContext
			.Students
			.Select(s => new Student()
			{
				Id = s.Id,
				FName = s.FName,
				LName = s.LName,
				Phone = s.Phone,
				NationalCode = s.NationalCode,
				ParentName = s.ParentName,
				Gender = s.Gender,
				imagePath = s.imagePath,
				Scores = (ICollection<Scores>)s.Scores.Select(p => new Scores()
				{
					Score1 = p.Score1,
					Score2 = p.Score2,
					Score3 = p.Score3,
					Score4 = p.Score4,
					FinalScore = p.FinalScore
				}),
				CourseStudent = (ICollection<CourseStudent>)s.CourseStudent.Select(p => new CourseStudent()
				{
					CoursesId = p.CoursesId,
					Course = p.Course,
				})
			});
	}

	public int CreateStudent(Student student)
	{
		_smsContext.Students.Add(student);
		return _smsContext.SaveChanges();
	}

	public IEnumerable<Student> GetAllStudents()
	{
		return _studentResult.ToList();
	}

	public Student GetStudentById(long id)
	{
		return _studentResult.SingleOrDefault(i => i.Id == id);
	}

	public int RemoveStudent(Student student)
	{
		_smsContext.Attach(student);
		_smsContext.Remove(student);
		return _smsContext.SaveChanges();
	}


	public bool UpdateStudent(StudentDto studentDto)
	{
		var studentToUpdate = _smsContext.Students.FirstOrDefault(s => s.Id == studentDto.Id);
		if (studentToUpdate is not null)
		{
			studentToUpdate.FName = studentDto.FName;
			studentToUpdate.LName = studentDto.LName;
			studentToUpdate.ParentName = studentDto.ParentName;
			studentToUpdate.NationalCode = studentDto.NationalCode;
			studentToUpdate.Phone = studentDto.Phone;
			studentToUpdate.Gender = studentDto.Gender;
			studentToUpdate.imagePath = studentDto.imagePath;
		}

		return _smsContext.SaveChanges() > 0;
	}
	
	public int createCourseStudent(CourseStudent courseStudent)
	{
		_smsContext.CourseStudents.Add(courseStudent);
		return _smsContext.SaveChanges();
	}


	public int RemoveCourseStudentRelations(CourseStudent courseStudent)
	{
		_smsContext.Attach(courseStudent);
		_smsContext.Remove(courseStudent);
		return _smsContext.SaveChanges();
	}
}