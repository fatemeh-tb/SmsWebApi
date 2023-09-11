using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Http;

namespace DataDomain.External;

public class FileViewModel
{
	public int Id { get; set; }
	public Student Student { get; set; }
	
	[NotMapped]
	public IFormFile? Image { get; set; }
}