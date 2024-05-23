using EmployeeAPI.Models;

namespace EmployeeAPI.Interface
{
    public interface IEmployeeRepository
    {
        void Add(Employee employee);
        Employee GetById(int id);
        List<Employee> Get();
        List<Employee> AddList(List<Employee> employeeList);
        void Remove(int id);
        List<Employee> GetBySex(char sex);
        List<Employee> GetByJob(string job);
    }
}
