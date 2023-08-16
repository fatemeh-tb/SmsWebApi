using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataDomain;
using DataDomain.External;
using DataService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace StudentManagementSystemWebApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CourseController : ControllerBase
    {
        private readonly CourseDataService _courseDataService;

        public CourseController()
        {
            _courseDataService = new CourseDataService();
        }

        [HttpPost]
        public int AddCourse(CourseDto courseDto)
        {
            return _courseDataService.AddCourse(courseDto);
        }

        [HttpGet("/courses")]
        public IEnumerable<Course> GetAllCourses()
        {
            return _courseDataService.GetCourses();
        }
    }
}
