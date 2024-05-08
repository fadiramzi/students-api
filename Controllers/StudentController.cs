using StudentsManagerMW.Interfaces;
using StudentsManagerMW.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Asp.Versioning;
using log4net;

namespace StudentsManagerMW.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class StudentController : ControllerBase
    {
        // This is injected service to access the student list and manipulate it
        // Done by the concept of DI (Dependency Injection)
        private readonly ILog _logger;

        private readonly IStudentService _studentService;

        public StudentController(IStudentService studentService,
            ILog logger)
        {
            _studentService = studentService;
            _logger = logger;
        }

        [HttpGet]

        public async Task<IActionResult> GetAllStudents(int page = 1, int pageSize = 10, string name = null, string sortBy = "Id", string sortOrder = "Desc")
        {
            // Return all students stored in the lists
            _logger.Info($"This is an informational log. get all students, params: {page}, {pageSize}, {name}");

            try
            {
                var studentList = await _studentService.GetAllStudents(page, pageSize, sortBy, sortOrder, name); _logger.Info($"This is an informational log. get all students, params: {page}, {pageSize}, {name}");
                _logger.Info($"Students list returned successfully");

                return Ok(studentList);
            }
            catch (Exception ex)
            {
                _logger.Error("This is an error log.");
                _logger.Error($"Error is: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }

        }



        //[HttpPost]
        //public IActionResult CreateStudent(StudentInput student)
        //{
        //    var st = new Student { 
        //        Name  = student.Name,
        //        DepartmentId = student.DepartmentId,
        //    };

        //    var createdStudent = _studentService.CreateStudent(st);

        //    // Return status code 200 (OK) along with the created student as part of the response
        //    return Ok(createdStudent);
        //}


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


        [HttpPut("{id}")]
        public IActionResult UpdateStudent(int id, Student updatedStudent)
        {
            // Logic to update an existing student (not implemented)
            
            // Check if student with the provided ID exists in the list
            var existingStudent = _studentService.GetStudentById(id);
            if (existingStudent != null)
            {
                
                var updatedtudent = _studentService.UpdateStudent(id, updatedStudent);
                return Ok(updatedtudent);
            }
            else
            {
                return NotFound($"Student with ID {id} not found");
            }
        }

        //[HttpDelete("{id}")]
        //[Authorize(Roles = "Admin")]
        //public IActionResult DeleteStudent(int id)
        //{
        //    // Logic to delete an existing student (not implemented)
        //    // Check if student with the provided ID exists in the list
           
        //    var existingStudent = _studentService.GetStudentById(id);
        //    // 
        //    if (existingStudent != null)
        //    {
        //        // Remove

        //        var isDeleted = _studentService.DeleteStudent(id);
        //        if (!isDeleted)
        //        {
        //            // Return status code 500 (Internal Server Error) if deletion fails
        //            return StatusCode(StatusCodes.Status500InternalServerError, "Failed to delete student");
        //        }
        //        // Return status code 200 (OK) to indicate successful deletion
        //        return Ok($"Deleted student with ID: {id}");
        //    }
        //    else
        //    {
        //        // Return status code 404 (Not Found) if student with the provided ID is not found
        //        return StatusCode(StatusCodes.Status404NotFound, $"Student with ID {id} not found");
        //    }
        //}
    }
}
