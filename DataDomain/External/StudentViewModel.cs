using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Http;

namespace DataDomain.External;

public class StudentViewModel
{
	public string? FName { get; set; }
	public string? LName { get; set; }
	public string? Phone { get; set; }
	public string? NationalCode { get; set; }
	public string? ParentName { get; set; }
	public string? Gender { get; set; }
	[NotMapped]
	public IFormFile? Photo { get; set; }
}