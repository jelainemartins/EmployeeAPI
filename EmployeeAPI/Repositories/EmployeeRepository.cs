using EmployeeAPI.Context;
using EmployeeAPI.Interface;
using EmployeeAPI.Models;

namespace EmployeeAPI.Repositories
{
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

        public Employee GetById(int id)
        {
            return _context.Employees.Find(id);
        }

        public List<Employee> Get()
        {
            return _context.Employees.ToList();
            //return _context.Database.SqlQuery<Employee>($"SELECT * FROM Employees").ToList();
        }

        public List<Employee> GetBySex(char sex)
        {
            return _context.Employees.Where(employee => employee.Sex == sex).ToList();
        }

        public List<Employee> AddList(List<Employee> employeeList)
        {
            _context.Employees.AddRange(employeeList);
            _context.SaveChanges();
            return Get();
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

        public List<Employee> GetByJob(string job)
        {
            return _context.Employees.Where(employee => employee.Job == job).ToList();
        }
    }
}
