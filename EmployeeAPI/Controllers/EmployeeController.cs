﻿using EmployeeAPI.Interface;
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
            var filePath = Path.Combine("Storage", employeeViewModel.Photo.FileName);
            using Stream fileStream = new FileStream(filePath, FileMode.Create);
            employeeViewModel.Photo.CopyTo(fileStream);
            _logger.LogInformation($"Add {employeeViewModel}");
            var employee = new Employee(employeeViewModel.FirstName, employeeViewModel.LastName, employeeViewModel.Birthdate.Value, employeeViewModel.Sex.Value, employeeViewModel.Job, filePath);
            _employeeRepository.Add(employee);
            return CreatedAtAction(nameof(GetId), new { id = employee.Id }, employee);
        }

        [HttpPost]
        public IActionResult AddList(List<Employee> employeeList)
        {
            _logger.LogInformation($"AddList {employeeList}");
            return Ok(_employeeRepository.AddList(employeeList));
        }

        [HttpPost]
        public IActionResult DownloadPhoto(int id)
        {
            _logger.LogInformation($"DownloadPhoto {id}");
            var employee = _employeeRepository.GetById(id);
            var dataBytes = System.IO.File.ReadAllBytes(employee.Photo);
            return File(dataBytes, "image/png");
        }

        [HttpPut]
        public IActionResult Update([FromForm] int id,[FromForm] EmployeeViewModel employeeViewModel)
        {
            var employee = _employeeRepository.Update(id, employeeViewModel);
            return employee != null ? Ok(employee) : NotFound();
        }

        [HttpGet]
        public IActionResult GetId(int id)
        {
            _logger.LogInformation($"GetId {id}");
            var employee = _employeeRepository.GetById(id);
            return employee == null ? NotFound() : Ok(employee);
        }

        [HttpGet]
        public IActionResult Get()
        {
            _logger.LogInformation("Get");
            var employees = _employeeRepository.Get();
            return Ok(employees);
        }

        [HttpGet]
        public IActionResult GetBySex(char sex)
        {
            var employees = _employeeRepository.GetBySex(sex);
            if (employees != null && employees.Count > 0)
            {
                _logger.LogInformation($"GetBySex {sex}");
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
            _logger.LogInformation($"GetByJob {job}");
            var employees = _employeeRepository.GetByJob(job);
            return Ok(employees);
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            _logger.LogInformation($"Delete {id}");
            _employeeRepository.Remove(id);
            return Ok();
        }
    }
}
