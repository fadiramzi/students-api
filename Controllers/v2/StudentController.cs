using StudentsManagerMW.Interfaces;
using StudentsManagerMW.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Asp.Versioning;

namespace StudentsManagerMW.Controllers.v2
{
    [ApiController]
    [ApiVersion("2.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class StudentController : ControllerBase
    {
        // This is injected service to access the student list and manipulate it
        // Done by the concept of DI (Dependency Injection)
        private readonly IStudentService _studentService;

        public StudentController(IStudentService studentService)
        {
            _studentService = studentService;
        }

       

        [HttpGet("{id}")]
        public IActionResult GetStudentById(int id)
        {
            // Logic to retrieve a student by id (not implemented)

            // Check if student with the provided ID exists in the list
            var student = _studentService.GetStudentById(id);
            if (student != null)
            {
                // Return the retrieved student if found
                return Ok(student);
            }
            else
            {
                // Return status code 404 (Not Found) if student with the provided ID is not found
                return NotFound($"Student with ID {id} not found");
            }
        }



       
    }
}
