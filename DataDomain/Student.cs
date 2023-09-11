﻿using Microsoft.AspNetCore.Http;

namespace DataDomain;

public class Student
{
	public int Id { get; set; }
	public string FName { get; set; }
	public string LName { get; set; }
	public string Phone { get; set; }
	public string NationalCode { get; set; }
	public string ParentName { get; set; }
	public string Gender { get; set; }
	public string ImageName { get; set; }
	public virtual ICollection<CourseStudent> CourseStudent { get; set; }
}