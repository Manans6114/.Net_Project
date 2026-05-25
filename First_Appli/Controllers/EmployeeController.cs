using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using First_Appli.Common.Model;

namespace First_Appli.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private static List<Employee> employees = new List<Employee>
        {
            new Employee { Id = 1, Name = "Manan", Department = "IT" },
            new Employee { Id = 2, Name = "Rahul", Department = "HR" },
            new Employee { Id = 3, Name = "Aman", Department = "Finance" },
            new Employee { Id = 4, Name = "Priya", Department = "HR" },
            new Employee { Id = 5, Name = "Rohit", Department = "Development" },
            new Employee { Id = 6, Name = "Sneha", Department = "Testing" },
            new Employee { Id = 7, Name = "Karan", Department = "Support" },
            new Employee { Id = 8, Name = "Neha", Department = "Marketing" },
            new Employee { Id = 9, Name = "Arjun", Department = "Sales" },
            new Employee { Id = 10, Name = "Pooja", Department = "Admin" },
            new Employee { Id = 11, Name = "Vikram", Department = "Security" },
            new Employee { Id = 12, Name = "Anjali", Department = "Design" },
            new Employee { Id = 13, Name = "Deepak", Department = "Operations" },
            new Employee { Id = 14, Name = "Simran", Department = "Management" },
            new Employee { Id = 15, Name = "Akash", Department = "Cloud" }
        };



        // GET ALL EMPLOYEES
        [HttpGet]
        public IActionResult GetAllEmployee()
        {
            return Ok(employees);
        }



        // GET EMPLOYEE BY ID
        [HttpGet("{id}")]
        public IActionResult GetEmployeeById(int id)
        {
            var employee = employees.FirstOrDefault(e => e.Id == id);

            if (employee == null)
            {
                return NotFound("Employee not found");
            }

            return Ok(employee);
        }



        // ADD EMPLOYEE
        [HttpPost]
        public IActionResult AddEmployee(Employee employee)
        {
            employees.Add(employee);

            return Ok("Employee added successfully");
        }



        // UPDATE EMPLOYEE
        [HttpPut("{id}")]
        public IActionResult UpdateEmployee(int id, Employee updatedEmployee)
        {
            var employee = employees.FirstOrDefault(e => e.Id == id);

            if (employee == null)
            {
                return NotFound("Employee not found");
            }

            employee.Name = updatedEmployee.Name;
            employee.Department = updatedEmployee.Department;

            return Ok("Employee updated successfully");
        }



        // DELETE EMPLOYEE
        [HttpDelete("{id}")]
        public IActionResult DeleteEmployee(int id)
        {
            var employee = employees.FirstOrDefault(e => e.Id == id);

            if (employee == null)
            {
                return NotFound("Employee not found");
            }

            employees.Remove(employee);

            return Ok("Employee deleted successfully");
        }
    }
}