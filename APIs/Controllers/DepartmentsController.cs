using APIs.Models;
using APIs.Repositories;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Net;

namespace APIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("AllowOrigin")]
    public class DepartmentsController : ControllerBase
    {
        private readonly DepartmentRepository _departmentRepository;
        public DepartmentsController(DepartmentRepository departmentRepository)
        {
            _departmentRepository = departmentRepository;
        }

        [HttpGet]
        public IActionResult GetAllDepartments()
        {
            //try
            //{
                var departments = _departmentRepository.GetAllDepartments();
                if (departments == null || departments.Count() == 0)
                {
                    return NotFound(new { status = "error", message = "No departments found", code = "404" });
                }
                return Ok(new { status = "success", data = departments, code = "200" });
            //}
            //catch (Exception ex)
            //{
            //    // Log the exception for debugging purposes
            //    Console.WriteLine($"An error occurred while retrieving departments: {ex}");

            //    return StatusCode(500, new { status = "error", message = "Internal server error", code = "500" });
            //}
        }

        [HttpGet("{deptId}")]
        public IActionResult GetDepartmentById(string deptId)
        {
            var department = _departmentRepository.GetDepartmentById(deptId);

            if (department == null)
            {
                return NotFound(); // Department not found
            }

            return Ok(department);
        }

        [HttpPost]
        public IActionResult AddDepartment([FromBody]Department department)
        {
            int result = _departmentRepository.AddDepartment(department);

            if (result > 0)
            {
                return Ok(result); // Department added successfully
            }

            return BadRequest(); // Failed to add department
        }

        [HttpPut("{deptId}")]
        public IActionResult UpdateDepartment([FromBody] Department department)
        {
            int result = _departmentRepository.UpdateDepartment(department);

            if (result > 0)
            {
                return Ok(); // Department updated successfully
            }

            return NotFound(); // Department not found or failed to update
        }

        [HttpDelete("{deptId}")]
        public IActionResult DeleteDepartment(string deptId)
        {
            int result = _departmentRepository.DeleteDepartment(deptId);

            if (result > 0)
            {
                return Ok(); // Department deleted successfully
            }

            return NotFound(); // Department not found or failed to delete
        }

        //[HttpGet]
        //public IActionResult GetAllDepartments()
        //{
        //    try 
        //    {

        //        var departments = _departmentRepository.GetAllDepartments();
        //        if (departments.Count() != 0)
        //        {
        //            return Ok();
        //        }
        //        return NoContent();
        //    }
        //    catch (Exception ex)
        //    {
        //        // Log the exception for debugging purposes
        //        _logger.LogError($"Error retrieving departments: {ex}");

        //        // Return a 500 Internal Server Error with a generic error message
        //        return StatusCode(500, new { Error = "An unexpected error occurred while processing the request." });
        //    };
        //}

    }
}
