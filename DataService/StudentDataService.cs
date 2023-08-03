using DataAccess;
using DataDomain;
using DataDomain.External;

namespace DataService;

public class StudentDataService
{
	private readonly ISmsRepository _smsRepository;

	public StudentDataService()
	{
		_smsRepository = new SmsRepository();
	}

	public int AddStudent(StudentDto studentDto)
	{
		var student = new Student();
		student.FName = studentDto.FName;
		student.LName = studentDto.LName;
		student.Phone = studentDto.Phone;
		student.ParentName = studentDto.ParentName;
		student.NationalCode = studentDto.NationalCode;
		student.Gender = studentDto.Gender;
		return _smsRepository.CreateStudent(student);
	}

	public IEnumerable<Student> GetStudents()
	{
		return _smsRepository.GetAllStudents();
	}
	
}