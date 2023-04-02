using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using EmployeeDCT1204.Models;
using EmployeeServices;

namespace EmployeeDCT1204.Controllers.Api
{

    [ApiController]
    [Route("api/[controller]")]
    public class EmployeeController : ControllerBase{
        private IEmployeeService _employeeService;
        public EmployeeController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        private readonly List<EmployeeIndexViewModel> employees = new List<EmployeeIndexViewModel>{
            new EmployeeIndexViewModel { Id = 1, EmployeeNo = "E001", FullName = "John Doe", Gender = "Male", DateJoined = new DateTime(2022, 01, 01), Designation = "Software Engineer", City = "New York" },
            new EmployeeIndexViewModel { Id = 2, EmployeeNo = "E002", FullName = "Jane Smith", Gender = "Female", DateJoined = new DateTime(2022, 02, 01), Designation = "Project Manager", City = "Los Angeles" },
            new EmployeeIndexViewModel { Id = 3, EmployeeNo = "E003", FullName = "Bob Johnson", Gender = "Male", DateJoined = new DateTime(2022, 03, 01), Designation = "UI/UX Designer", City = "Chicago" }
        };

        [HttpGet]
        public IActionResult GetAllEmployees()
        {
            var model = _employeeService.GetAll().Select(employee => new EmployeeIndexViewModel
            {
                Id = employee.ID,
                EmployeeNo = employee.EmployeeNo,
                FullName = employee.FullName,
                Gender = employee.Gender,
                ImageUrl = employee.ImageUrl,
                DateJoined = employee.DateJoined,
                Designation = employee.Designation,
                City = employee.City
            }).ToList();
            return Ok(model);
        }

        [HttpGet("{id}")]
        public IActionResult GetEmployeeById(int id)
        {
            var employee = employees.Find(e => e.Id == id);

            if (employee == null)
            {
                return NotFound();
            }

            return Ok(employee);
        }

        [HttpPost]
        public IActionResult AddEmployee([FromBody] EmployeeIndexViewModel employee)
        {
            if (employee == null)
            {
                return BadRequest();
            }

            // assign an id to the new employee
            employee.Id = employees.Count + 1;

            employees.Add(employee);

            return CreatedAtAction(nameof(GetEmployeeById), new { id = employee.Id }, employee);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteEmployee(int id)
        {
            var employee = employees.Find(e => e.Id == id);

            if (employee == null)
            {
                return NotFound();
            }

            employees.Remove(employee);

            return NoContent();
        }

        [HttpPut("{id}")]
        public IActionResult UpdateEmployee(int id, [FromBody] EmployeeIndexViewModel employee)
        {
            if (employee == null || id != employee.Id)
            {
                return BadRequest();
            }

            var existingEmployee = employees.Find(e => e.Id == id);

            if (existingEmployee == null)
            {
                return NotFound();
            }

            existingEmployee.EmployeeNo = employee.EmployeeNo;
            existingEmployee.FullName = employee.FullName;
            existingEmployee.Gender = employee.Gender;
            existingEmployee.ImageUrl = employee.ImageUrl;
            existingEmployee.DateJoined = employee.DateJoined;
            existingEmployee.Designation = employee.Designation;
            existingEmployee.City = employee.City;

            return NoContent();
        }
    }
    }

