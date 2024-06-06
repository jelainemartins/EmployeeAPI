using EmployeeAPI.Models;
using EmployeeAPI.ViewModel;

namespace EmployeeAPI.Interface
{
    //A interface da acesso ao [REPOSITORY];
    public interface IEmployeeRepository
    {
        void Add(Employee employee);
        Employee GetById(int id);
        List<Employee> Get();
        List<Employee> AddList(List<Employee> employeeList);
        void Remove(int id);
        List<Employee> GetBySex(char sex);
        List<Employee> GetByJob(string job);
        Employee Update(int id, EmployeeViewModel employeeViewModel);
    }
}
