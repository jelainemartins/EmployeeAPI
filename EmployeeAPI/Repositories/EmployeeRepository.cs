using EmployeeAPI.Context;
using EmployeeAPI.Interface;
using EmployeeAPI.Models;
using EmployeeAPI.ViewModel;

namespace EmployeeAPI.Repositories
{
    //Usando a interface de herança [IEmployeeRepository];
    //Tudo que há na [IEmployeeRepository], será herdado obrigatoriamente pelo [REPOSITORY];
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly EmployeeContext _context;

        public EmployeeRepository(EmployeeContext context)
        {
            _context = context;
        }

        public void Add(Employee employee)
        {
            _context.Employees.Add(employee);
            _context.SaveChanges();
        }

        public List<Employee> AddList(List<Employee> employeeList)
        {
            _context.Employees.AddRange(employeeList);
            _context.SaveChanges();
            return Get();
        }

        public List<Employee> Get()
        {
            return _context.Employees.ToList();
            //return _context.Database.SqlQuery<Employee>($"SELECT * FROM Employees").ToList();
        }
        public List<Employee> GetPage(int pageNumber, int pageQuantity)
        {
            return _context.Employees.Skip((pageNumber - 1) * pageQuantity).Take(pageQuantity).ToList();
        }

        public Employee GetById(int id)
        {
            return _context.Employees.Find(id);
        }

        public List<Employee> GetBySex(char sex)
        {
            return _context.Employees.Where(employee => employee.Sex == sex).ToList();
        }
        public List<Employee> GetByJob(string job)
        {
            return _context.Employees.Where(employee => employee.Job == job).ToList();
        }

        public List<Employee> GetByBirthdate(DateTime date)
        {
            return _context.Employees.Where(employee => employee.Birthdate == date).ToList();
        }

        public Employee Update(int id, EmployeeViewModel employeeViewModel)
        {
            var employee = GetById(id);
            if (employee != null)
            {
                var filePath = string.Empty;
                if (employeeViewModel.Photo != null)
                {
                    filePath = Path.Combine("Storage", employeeViewModel.Photo.FileName);
                    if (filePath != employee.Photo)
                    {
                        File.Delete(employee.Photo);
                    }
                    using Stream fileStream = new FileStream(filePath, FileMode.Create);
                    employeeViewModel.Photo.CopyTo(fileStream);
                }
                var newEmployee = new Employee(id, employeeViewModel.FirstName ?? employee.FirstName, employeeViewModel.LastName ?? employee.LastName, employeeViewModel.Birthdate ?? employee.Birthdate, employeeViewModel.Sex ?? employee.Sex, employeeViewModel.Job ?? employee.Job, string.IsNullOrEmpty(filePath) ? employee.Photo : filePath);
                _context.ChangeTracker.Clear();
                _context.Employees.Update(newEmployee);
                _context.SaveChanges();
                return newEmployee;
            }
            throw new Exception($"{id} not found.");
        }

        public void Remove(int id)
        {
            var employee = _context.Employees.Find(id);
            if (employee != null)
            {
                _context.Remove(employee);
                _context.SaveChanges();
            }
        }
    }
}
