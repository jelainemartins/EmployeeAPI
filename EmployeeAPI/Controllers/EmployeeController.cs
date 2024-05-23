using EmployeeAPI.Interface;
using EmployeeAPI.Models;
using EmployeeAPI.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeRepository _employeeRepository;

        public EmployeeController(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository ?? throw new ArgumentNullException();
        }

        [HttpPost]
        public IActionResult Add(EmployeeViewModel employeeViewModel)
        {
            var employee = new Employee(employeeViewModel.FirstName, employeeViewModel.LastName, employeeViewModel.Birthdate, employeeViewModel.Sex, null);
            _employeeRepository.Add(employee);
            return CreatedAtAction(nameof(GetId), new { id = employee.Id }, employee);
        }

        [HttpPost]
        public IActionResult AddList(List<Employee> employeeList)
        {
            _employeeRepository.AddList(employeeList);
            return Ok(employeeList);
        }

        [HttpGet]
        public IActionResult GetId(int id)
        {
            var employee = _employeeRepository.GetById(id);
            return employee == null ? NotFound() : Ok(employee);
        }

        [HttpGet]
        public IActionResult Get()
        {
            var employees = _employeeRepository.Get();
            return Ok(employees);
        }

        [HttpGet]
        public IActionResult GetBySex(char sex)
        {
            var employees = _employeeRepository.GetBySex(sex);
            return Ok(employees);
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            _employeeRepository.Remove(id);
            return Ok();
        }
    }
}
