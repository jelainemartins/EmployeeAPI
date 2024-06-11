using EmployeeAPI.Interface;
using EmployeeAPI.Models;
using EmployeeAPI.ViewModel;
using Microsoft.AspNetCore.Mvc;
//CONTROLLER; Entrada de dados;
//Responsável pelos Endpoints;
//Posso iniciar aqui e depois ir para o Repository/Interface; Ctrl + .;
namespace EmployeeAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly ILogger<EmployeeController> _logger;

        public EmployeeController(IEmployeeRepository employeeRepository, ILogger<EmployeeController> logger)
        {
            _employeeRepository = employeeRepository ?? throw new ArgumentNullException(nameof(employeeRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        [HttpPost]
        public IActionResult Add([FromForm] EmployeeViewModel employeeViewModel)
        {
            _logger.LogInformation(nameof(Add));
            var filePath = string.Empty;
            if (employeeViewModel.Photo != null)
            {
                filePath = Path.Combine("Storage", employeeViewModel.Photo.FileName);
                using Stream fileStream = new FileStream(filePath, FileMode.Create);
                employeeViewModel.Photo.CopyTo(fileStream);
            }
            var employee = new Employee(employeeViewModel.FirstName, employeeViewModel.LastName, employeeViewModel.Birthdate.Value, employeeViewModel.Sex.Value, employeeViewModel.Job, string.IsNullOrEmpty(filePath) ? null : filePath);
            _employeeRepository.Add(employee);
            return CreatedAtAction(nameof(GetId), new { id = employee.Id }, employee);
        }

        [HttpPost]
        public IActionResult AddList(List<EmployeeViewModel> employeeList)
        {
            _logger.LogInformation(nameof(AddList));
            var listEmployee = new List<Employee>();
            foreach (var employeeViewModel in employeeList)
            {
                var employee = new Employee(employeeViewModel.FirstName, employeeViewModel.LastName, employeeViewModel.Birthdate.Value, employeeViewModel.Sex.Value, employeeViewModel.Job, null);
                listEmployee.Add(employee);
            }
            _employeeRepository.AddList(listEmployee);
            return Ok(listEmployee);
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            _logger.LogInformation(nameof(Delete));
            _employeeRepository.Remove(id);
            return Ok();
        }

        [HttpPost]
        public IActionResult DownloadPhoto(int id)
        {
            _logger.LogInformation(nameof(DownloadPhoto));
            var employee = _employeeRepository.GetById(id);
            var dataBytes = System.IO.File.ReadAllBytes(employee.Photo);
            return File(dataBytes, "image/png");
        }

        [HttpGet]
        public IActionResult Get()
        {
            _logger.LogInformation(nameof(Get));
            var employees = _employeeRepository.Get();
            return Ok(employees);
        }

        [HttpGet]
        public IActionResult GetPage(int pageNumber, int pageQuantity)
        {
            _logger.LogInformation(nameof(GetPage));
            var employees = _employeeRepository.GetPage(pageNumber, pageQuantity);
            return Ok(employees);
        }

        [HttpGet]
        public IActionResult GetId(int id)
        {
            _logger.LogInformation(nameof(GetId));
            var employee = _employeeRepository.GetById(id);
            return employee == null ? NotFound() : Ok(employee);
        }

        [HttpGet]
        public IActionResult GetBySex(char sex)
        {
            var employees = _employeeRepository.GetBySex(sex);
            if (employees != null && employees.Count > 0)
            {
                _logger.LogInformation(nameof(GetBySex));
                return Ok(employees);
            }
            else
            {
                _logger.Log(LogLevel.Error, "Ocorreu um erro");
                return NotFound();
            }
        }

        [HttpGet]
        public IActionResult GetByJob(string job)
        {
            _logger.LogInformation(nameof(GetByJob));
            var employees = _employeeRepository.GetByJob(job);
            return Ok(employees);
        }

        [HttpGet]
        public IActionResult GetByBirthdate(DateTime date)
        {
            _logger.LogInformation(nameof(GetByBirthdate));
            var birthdate = _employeeRepository.GetByBirthdate(date);
            return Ok(birthdate);
        }

        [HttpPut]
        public IActionResult Update([FromForm] int id, [FromForm] EmployeeViewModel employeeViewModel)
        {
            _logger.LogInformation(nameof(Update));
            var employee = _employeeRepository.Update(id, employeeViewModel);
            return employee != null ? Ok(employee) : NotFound();
        }
    }
}
