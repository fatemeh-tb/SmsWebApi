using DataAccess;
using DataDomain;
using DataDomain.External;
using Microsoft.AspNetCore.Hosting;


namespace DataService;

public class StudentDataService
{
	private readonly StudentRepository _studentRepository;
	private IHostingEnvironment  _environment;

	public StudentDataService(IHostingEnvironment  environment)
	{
		_studentRepository = new StudentRepository();
		_environment = environment;

	}

	public int AddCourseStudent(CourseStudentDto courseStudentDto)
	{
		var coursestudent = new CourseStudent();
		coursestudent.CoursesId = courseStudentDto.CoursesId;
		coursestudent.StudentsId = courseStudentDto.StudentsId;

		return _studentRepository.createCourseStudent(coursestudent);
	}

	public int  AddStudent(StudentDto model)
	{
		var student = new Student();
		
		student.FName = model.FName;
		student.LName = model.LName;
		student.Phone = model.Phone;
		student.ParentName = model.ParentName;
		student.NationalCode = model.NationalCode;
		student.Gender = model.Gender;
		student.imagePath = model.imagePath;
		return _studentRepository.CreateStudent(student);
	}

	public IEnumerable<Student> GetStudents()
	{
		return _studentRepository.GetAllStudents();
	}

	public Student GetStudentById(long id)
	{
		return _studentRepository.GetStudentById(id);
	}

	public int DeleteStudent(long id)
	{
		var student = _studentRepository.GetStudentById(id);
		return _studentRepository.RemoveStudent(student);
	}

	public bool UpdateStudent(StudentDto studentDto)
	{
		return _studentRepository.UpdateStudent(studentDto);
		
	}
}